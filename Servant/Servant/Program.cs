using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Servant
{
    static class Program
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        // Testing variables
        static string pattern = "..TEST ";
        static string currentString = "";
        static string template = "THIS is A TEXT";

        [STAThread]
        static void Main()
        {
            _hookID = SetHook(_proc);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        //lParam => Unmanaged memory 
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // lParam could be a memory position 
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                currentString += VKCodeToString((uint)Marshal.ReadInt32(lParam)); //https://stackoverflow.com/questions/10391025/receive-os-level-key-press-events-in-c-sharp-application
                Console.WriteLine(currentString);

                if (pattern == currentString)
                {
                    UnhookWindowsHookEx(_hookID);
                    //Missing functionality: Find the template based on the match

                    foreach (char letter in template)
                    {
                        Console.WriteLine(letter);
                        keybd_event((byte)letter, 0, 0, IntPtr.Zero); //https://stackoverflow.com/questions/31370634/how-to-sendkeybd-event-unicode-keys-with-c-sharp
                    }

                    currentString = "";
                    _hookID = SetHook(_proc);
                }
                else if (!pattern.StartsWith(currentString))
                {
                    currentString = "";
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        //...................................Testing..........................................

        private static uint _lastVKCode = 0;
        private static uint _lastScanCode = 0;
        private static byte[] _lastKeyState = new byte[255];
        private static bool _lastIsDead = false;

        public static string VKCodeToString(uint vkCode)
        {
            // ToUnicodeEx needs StringBuilder, it populates that during execution.
            var sbString = new System.Text.StringBuilder(5);

            var bKeyState = new byte[255];
            bool bKeyStateStatus;
            bool isDead = false;

            // Gets the current windows window handle, threadID, processID
            IntPtr currentHWnd = GetForegroundWindow();
            uint currentProcessID;
            uint currentWindowThreadID = GetWindowThreadProcessId(currentHWnd, out currentProcessID);

            // This programs Thread ID
            uint thisProgramThreadId = GetCurrentThreadId();

            // Attach to active thread so we can get that keyboard state
            if (AttachThreadInput(thisProgramThreadId, currentWindowThreadID, true))
            {
                // Current state of the modifiers in keyboard
                bKeyStateStatus = GetKeyboardState(bKeyState);

                // Detach
                AttachThreadInput(thisProgramThreadId, currentWindowThreadID, false);
            }
            else
            {
                // Could not attach, perhaps it is this process?
                bKeyStateStatus = GetKeyboardState(bKeyState);
            }

            // On failure we return empty string.
            if (!bKeyStateStatus)
                return "";

            // Gets the layout of keyboard
            IntPtr hkl = GetKeyboardLayout(currentWindowThreadID);

            // Maps the virtual keycode
            uint lScanCode = MapVirtualKeyEx(vkCode, 0, hkl);

            

            // Converts the VKCode to unicode
            int relevantKeyCountInBuffer = ToUnicodeEx(vkCode, lScanCode, bKeyState, sbString, sbString.Capacity, (uint)0, hkl);

            string ret = "";

            switch (relevantKeyCountInBuffer)
            {
                // Dead keys (^,`...)
                case -1:
                    isDead = true;

                    // We must clear the buffer because ToUnicodeEx messed it up, see below.
                    ClearKeyboardBuffer(vkCode, lScanCode, hkl);
                    break;

                case 0:
                    break;

                // Single character in buffer
                case 1:
                    ret = sbString[0].ToString();
                    break;

                // Two or more (only two of them is relevant)
                case 2:
                default:
                    ret = sbString.ToString().Substring(0, 2);
                    break;
            }

            if (_lastVKCode != 0 && _lastIsDead)
            {
                var sbTemp = new System.Text.StringBuilder(5);
                ToUnicodeEx(_lastVKCode, _lastScanCode, _lastKeyState, sbTemp, sbTemp.Capacity, (uint)0, hkl);
                _lastVKCode = 0;

                return ret;
            }

            // Save these
            _lastScanCode = lScanCode;
            _lastVKCode = vkCode;
            _lastIsDead = isDead;
            _lastKeyState = (byte[])bKeyState.Clone();

            return ret;
        }

        private static void ClearKeyboardBuffer(uint vk, uint sc, IntPtr hkl)
        {
            var sb = new System.Text.StringBuilder(10);

            int rc;
            do
            {
                var lpKeyStateNull = new Byte[255];
                rc = ToUnicodeEx(vk, sc, lpKeyStateNull, sb, sb.Capacity, 0, hkl);
            } while (rc < 0);
        }


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


        //..............




        // Get OS key events to get the key clicked, this is basically like a keylogger 
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, IntPtr dwExtraInfo);

    }

}


/*
 * Bugs:
 * 
 * Si el template contiene caracteres especiales no los esta pegando 
 * No esta validando si el patron contiene caracteres especiales => ahora si funciona con caracteres especiales, pero no caracteres especiales de combinaciones de teclas
 * Si el patron contiene minusculas 
 * Si el template tiene minusculas
 * 
 * Hay un problema con el parse de numeros a chars, no siempre esta viniendo el numero que corresponde a la letra o simbolo especial
 * 
 *  http://yorktown.cbe.wwu.edu/sandvig/shared/asciiCodes.aspx
 *  https://docs.microsoft.com/en-us/dotnet/api/system.intptr?view=netframework-4.8 => No se que esta haciendo
 *  https://stackoverflow.com/questions/11829072/how-do-i-read-a-uint-from-a-pointer-with-marshalling
 *  
 *  https://stackoverflow.com/questions/20201350/paste-text-to-an-active-word-window-using-c-sharp
 *  Parece que esta es la solucion: 
 *  https://www.youtube.com/watch?v=f_ZBFJMlhYQ
 *  
 *  Esto fue lo que sirvio para parsear bien la tecla al valor 
 *  
 */
