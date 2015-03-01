using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MiniPlayerClassic
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //bool firstrun;
            //System.Threading.Mutex run = new System.Threading.Mutex(true, "MiniPlayerClassic", out firstrun);
            //if(firstrun)
            //{
            //    run.ReleaseMutex();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainFrom(args));
            //}
            //else
            //{
            //    Application.Exit();
            //}
        }
    }
}
