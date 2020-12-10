using System;
using System.Reflection;
using System.Windows.Forms;

namespace AV_Tool
{
    internal static class Program
    {
        public static Gui Gui;
        public static LoginForm LoginPrompt;

        [STAThread]
        private static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (s, assembly) =>
            {
                if (!assembly.Name.Contains("Newtonsoft.Json,"))
                {
                    return null;
                }

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AV_Tool.Resources.Newtonsoft.Json.dll"))
                {
                    if (stream == null)
                    {
                        return null;
                    }

                    var assemblyData = new byte[stream.Length];

                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Updater.CheckNewestVersion();
            Gui = new Gui();
            LoginPrompt = new LoginForm();

            Application.Run(Gui);
        }
    }
}