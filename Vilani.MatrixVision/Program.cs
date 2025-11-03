using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;
using Vilani.Licenses;
using Vilani.MatrixVision.Core;
using Vilani.MatrixVision.Forms;

namespace Vilani.MatrixVision
{
    static class Program
    {

        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;



            SerialKeyGenrator serialKeyGenrator = new SerialKeyGenrator();
            if (!serialKeyGenrator.IsSerialKeyValid())
            {
                Application.Run(new MainContainer());
               // Application.Run(form);
            }
            else
            {
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["AuthenticationEnabled"]))
                    Application.Run(new LoginForm());
                else
                    Application.Run(new MainContainer());
            }


        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                logger.Fatal(e.Exception);

            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
        }



    }
}
