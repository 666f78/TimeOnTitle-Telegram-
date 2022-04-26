using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace TimeOnTitle
{
    internal class ChangeWindowTitle
    {
        #region Import function from user32.dll

        [DllImport("user32.dll")]
        private static extern int SetWindowText(IntPtr hWnd, string windowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        #endregion Import function from user32.dll

        public void Start()
        {
            Task task = Task.Run(() => MemoryManagement.FlushMemory());
            HideWindows.HideConsole();
            ChangeTitleWindow();
        }

        private void ChangeTitleWindow()
        {
            Process process = GetProcesOrStart();
            SpinWait.SpinUntil(() => process.MainWindowHandle != IntPtr.Zero);
            int MessageCount = GetMessageCount(GetWindowTitle(process));
            while (true)
            {
                int checkTitle = GetMessageCount(GetWindowTitle(process));
                SetWindowText(process.MainWindowHandle, $"Telegram ({(checkTitle == MessageCount ? MessageCount : checkTitle)}) | {DateTime.Now.ToString("HH:mm:ss")}");
                Thread.Sleep(500);
            }
        }

        private string GetWindowTitle(Process process)
        {
            int length = GetWindowTextLength(process.MainWindowHandle);
            StringBuilder sb = new StringBuilder(length + 1);
            GetWindowText(process.MainWindowHandle, sb, sb.Capacity);
            return sb.ToString();
        }

        private int GetMessageCount(string str)
        {
            try
            {
                Regex pattern = new Regex(@"\((?<value>.*?)\)");
                MatchCollection matches = pattern.Matches(str);
                return Convert.ToInt32(matches[0].Groups["value"].Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private Process GetProcesOrStart()
        {
            Process[] process = Process.GetProcessesByName("Telegram");
            if (process.Length == 0)
            {
                return Process.Start(@"pathToTelegram");
            }
            else
            {
                return process[0];
            }
        }
    }
}