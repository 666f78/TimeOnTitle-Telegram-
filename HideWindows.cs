using System;
using System.Runtime.InteropServices;

namespace TimeOnTitle
{
    internal class HideWindows
    {
        #region Import function from user32.dll

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        #endregion Import function from user32.dll

        public static void HideConsole()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, 0);
        }
    }
}