using System;
using System.Threading;

namespace TimeOnTitle
{
    public class MemoryManagement
    {
        public static void FlushMemory()
        {
            while (true)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Thread.Sleep(30000);
            }
        }
    }
}