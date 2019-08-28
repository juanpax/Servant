using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
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
        static string pattern = "TEST";
        static string currentString = "";
        static string template = "THIS is a ** ? test TEXT";

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

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int key = Marshal.ReadInt32(lParam);
                currentString += ((Keys)key).ToString();

                if (pattern == currentString)
                {
                    UnhookWindowsHookEx(_hookID);
                    //Missing functionality: Find the template based on the match

                    keybd_event((byte)key, 0, 0, IntPtr.Zero);

                    foreach (int letter in template)
                    {
                        keybd_event((byte)letter, 0, 0, IntPtr.Zero);
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
 * No esta validando si el patron contiene caracteres especiales 
 * Si el patron contiene minusculas 
 * Si el template tiene minusculas
 * 
 */