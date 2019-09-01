using System;
using System.Collections.Generic;
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
        private const uint WM_KEYDOWN = 0x0100;
        private const uint INPUT_KEYBOARD = 1;
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
        private static BlurbListView BlurbListView = new BlurbListView();

        [STAThread]
        static void Main()
        {
            _hookID = SetHook(_proc);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(BlurbListView);

            UnhookWindowsHookEx(_hookID);
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                currentString += VKCodeToString((uint)Marshal.ReadInt32(lParam));

                foreach (string[] blurb in BlurbListView.BLURBLIST)
                {
                    string pattern = blurb[1];

                    if (pattern == currentString)
                    {
                        UnhookWindowsHookEx(_hookID);

                        DeletePattern(pattern);
                        WriteText(blurb[2]);
                        currentString = "";

                        _hookID = SetHook(_proc);
                    }
                    else if (!pattern.StartsWith(currentString))
                    {
                        currentString = "";
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
            for (int i = 1; i < pattern.Length; i++)
            {
                keybd_event((byte)'\b', 0, 0, IntPtr.Zero);
                keybd_event((byte)'\b', 0, KEYEVENTF_KEYUP, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Method to get the current key pressed as string format
        /// </summary>
        public static string VKCodeToString(uint vkCode)
        {
            StringBuilder sbString = new StringBuilder(5);
            byte[] bKeyState = new byte[255];
            bool bKeyStateStatus;
            bool isDead = false;
            IntPtr currentHWnd = GetForegroundWindow();
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
                var sbTemp = new System.Text.StringBuilder(5);
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
        public static void WriteText(string text)
        {
            List<INPUT> inputs = new List<INPUT>();

            foreach (char letter in text)
            {
                foreach (bool keyUp in new bool[] { false, true })
                {
                    INPUT input = new INPUT
                    {
                        type = INPUT_KEYBOARD,
                        u = new InputUnion
                        {
                            ki = new KEYBDINPUT
                            {
                                wVk = 0,
                                wScan = letter,
                                dwFlags = KEYEVENTF_UNICODE | (keyUp ? KEYEVENTF_KEYUP : 0),
                                dwExtraInfo = GetMessageExtraInfo(),
                            }
                        }
                    };
                    inputs.Add(input);
                }
            }

            SendInput((uint)inputs.Count, inputs.ToArray(), Marshal.SizeOf(typeof(INPUT)));
        }

        struct INPUT
        {
            public uint type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct InputUnion
        {
            [FieldOffset(0)]
            public MOUSEINPUT mi;
            [FieldOffset(0)]
            public KEYBDINPUT ki;
            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
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

        [DllImport("user32.dll")]
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
        private static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);

        // DDLS methods to write text 
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        static extern IntPtr GetMessageExtraInfo();
    }
}