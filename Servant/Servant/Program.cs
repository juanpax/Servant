using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Servant
{
    static class Program
    {
        // Key codes
        private const uint WH_KEYBOARD_LL = 13;
        private const uint WM_KEYUP = 0x0101;
        private const uint KEYEVENTF_KEYUP = 0x0002;
        private const uint KEYEVENTF_UNICODE = 0x0004;

        // Variables to catch the key events
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        // Variables to paste the blurb text in the text box
        private static uint _lastVKCode = 0;
        private static uint _lastScanCode = 0;
        private static byte[] _lastKeyState = new byte[255];
        private static bool _lastIsDead = false;

        private static string currentString = "";
        private static BlurbListView BlurbListView;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (GetProcessCount("Servant") == 1)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                BlurbListView = new BlurbListView();
                BlurbListView.roundedButtonPlayPause.Click += new EventHandler(roundedButtonPlayPause_Click);
                BlurbListView.FormClosed += new FormClosedEventHandler(BlurbListView_FormClosed);

                _hookID = SetHook(_proc);
                Application.Run(BlurbListView);
            }
        }

        /// <summary>
        /// Method to validate if Servant is already running or not. In case the application is already running so it will not opened twice
        /// </summary>
        public static int GetProcessCount(string name)
        {
            int count = 0; 

            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName == name)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Event when the rounded button is clicked
        /// </summary>
        private static void roundedButtonPlayPause_Click(object sender, EventArgs e)
        {
            string tag = BlurbListView.roundedButtonPlayPause.Tag.ToString();

            if (tag == "Play")
            {
                BlurbListView.roundedButtonPlayPause.BackgroundImage = Properties.Resources.pause;
                BlurbListView.labelServantState.Text = "Servant has been started!";
                BlurbListView.roundedButtonPlayPause.Tag = "Pause";

                _hookID = SetHook(_proc);
            }
            else
            {
                BlurbListView.roundedButtonPlayPause.BackgroundImage = Properties.Resources.start;
                BlurbListView.labelServantState.Text = "Servant has been stopped!";
                BlurbListView.roundedButtonPlayPause.Tag = "Play";

                UnhookWindowsHookEx(_hookID);
            }
        }

        /// <summary>
        /// Event when the EventListView is closed
        /// </summary>
        private static void BlurbListView_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
        }

        /// <summary>
        /// Delegate to manage Low level keyboard proc
        /// </summary>
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Method to bind the hook event
        /// </summary>
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        /// <summary>
        /// This is method is going to be triggered when the user press any key
        /// </summary>
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
            {
                IntPtr currentWindow = GetForegroundWindow();
                string key = VKCodeToString(currentWindow, (uint)Marshal.ReadInt32(lParam));

                if (key == "\b" && currentString.Length > 0)
                {
                    currentString = currentString.Remove(currentString.Length - 1);
                }
                else
                {
                    currentString += key;
                    bool foundPatternMatch = false;

                    foreach (string[] blurb in BlurbListView.BLURBLIST)
                    {
                        if (blurb[1] == currentString)
                        {
                            UnhookWindowsHookEx(_hookID);

                            DeletePattern(blurb[1]);
                            WriteText(currentWindow, blurb[2], blurb[3]);
                            currentString = "";

                            _hookID = SetHook(_proc);
                            break;
                        }
                        else if (blurb[1].StartsWith(currentString))
                        {
                            foundPatternMatch = true;
                            break;
                        }
                    }

                    if (!foundPatternMatch)
                    {
                        currentString = ""; // el error cuando se esta  eliminando y se falla una letra esta aqui, porque basicamente como el patron deja de tener un startswith entonces se limpia el string
                    }
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        /// <summary>
        /// Method to delete the blurb identifier from the current writting window
        /// </summary>
        private static void DeletePattern(string pattern)
        {
            foreach (char lettern in pattern)
            {
                keybd_event((byte)'\b', 0, 0, IntPtr.Zero);
                keybd_event((byte)'\b', 0, KEYEVENTF_KEYUP, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Method to get the current key pressed as string format
        /// </summary>
        public static string VKCodeToString(IntPtr currentHWnd, uint vkCode)
        {
            StringBuilder sbString = new StringBuilder(5);
            byte[] bKeyState = new byte[255];
            bool bKeyStateStatus;
            bool isDead = false;
            uint currentProcessID;
            uint currentWindowThreadID = GetWindowThreadProcessId(currentHWnd, out currentProcessID);
            uint thisProgramThreadId = GetCurrentThreadId();

            if (AttachThreadInput(thisProgramThreadId, currentWindowThreadID, true))
            {
                bKeyStateStatus = GetKeyboardState(bKeyState);
                AttachThreadInput(thisProgramThreadId, currentWindowThreadID, false);
            }
            else
            {
                bKeyStateStatus = GetKeyboardState(bKeyState);
            }

            if (!bKeyStateStatus)
                return "";

            IntPtr hkl = GetKeyboardLayout(currentWindowThreadID);
            uint lScanCode = MapVirtualKeyEx(vkCode, 0, hkl);
            string ret = "";
            int relevantKeyCountInBuffer = ToUnicodeEx(vkCode, lScanCode, bKeyState, sbString, sbString.Capacity, (uint)0, hkl);

            switch (relevantKeyCountInBuffer)
            {
                // Dead keys (^,`...)
                case -1:
                    {
                        isDead = true;
                        ClearKeyboardBuffer(vkCode, lScanCode, hkl);
                        break;
                    }
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        ret = sbString[0].ToString();
                        break;
                    }
                case 2:
                default:
                    {
                        ret = sbString.ToString().Substring(0, 2);
                        break;
                    }
            }

            if (_lastVKCode != 0 && _lastIsDead)
            {
                var sbTemp = new StringBuilder(5);
                ToUnicodeEx(_lastVKCode, _lastScanCode, _lastKeyState, sbTemp, sbTemp.Capacity, (uint)0, hkl);
                _lastVKCode = 0;

                return ret;
            }

            _lastScanCode = lScanCode;
            _lastVKCode = vkCode;
            _lastIsDead = isDead;
            _lastKeyState = (byte[])bKeyState.Clone();

            return ret;
        }

        /// <summary>
        /// Method to clear the keyboard buffer
        /// </summary>
        private static void ClearKeyboardBuffer(uint vk, uint sc, IntPtr hkl)
        {
            StringBuilder sb = new StringBuilder(10);

            int rc;
            do
            {
                var lpKeyStateNull = new byte[255];
                rc = ToUnicodeEx(vk, sc, lpKeyStateNull, sb, sb.Capacity, 0, hkl);
            } while (rc < 0);
        }

        /// <summary>
        /// Method to send the blurb text to the current writting window
        /// </summary>
        private static void WriteText(IntPtr currentWindow, string format, string text)
        {
            TextDataFormat textFormat = TextDataFormat.Text;

            if (format == "Rich Text Format (RTF)")
            {
                if (CurrentWindowAppSupportRTF(currentWindow))
                {
                    textFormat = TextDataFormat.Rtf;
                }
                else
                {
                    text = ParseRTF2PlainText(text);
                }
            }

            Clipboard.SetText(text, textFormat);
            SendKeys.SendWait("^(v)");
        }

        /// <summary>
        /// This method is going to parse the rtf text to plain text format
        /// </summary>
        private static string ParseRTF2PlainText(string rtf)
        {
            RichTextBox rtBox = new RichTextBox();
            rtBox.Rtf = rtf;

            return rtBox.Text;
        }

        /// <summary>
        /// Method to validate if the current window will support RTF format or not
        /// </summary>
        private static bool CurrentWindowAppSupportRTF(IntPtr handle)
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                string windowName = Buff.ToString().Trim();

                return
                    (
                        windowName.EndsWith("Word") ||
                        windowName.EndsWith("PowerPoint") ||
                        windowName.EndsWith("Outlook") ||
                        windowName.EndsWith("Message (Plain Text)") || // Outlook
                        windowName.EndsWith("Message (HTML)") || // Outlook
                        windowName.EndsWith("Message (Rich Text)") // Outlook
                    );
            }
            return false;
        }

        // DDL methods to get the pressed key
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(uint idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, IntPtr dwExtraInfo);

        [DllImport("User32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("User32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();

        [DllImport("user32.dll")]
        private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("user32.dll")]
        private static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetKeyboardLayout(uint dwLayout);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKeyEx(uint uCode, uint uMapType, IntPtr dwhkl);

        [DllImport("user32.dll")]
        private static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);

        // DLL to validate the Window 
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
    }
}