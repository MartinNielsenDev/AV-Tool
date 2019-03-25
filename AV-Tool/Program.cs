using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AV_Tool
{
    static class Program
    {
        public static GUI gui;
        public static LoginForm loginPrompt;

        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler((s, assembly) =>
            {
                if (assembly.Name.Contains("Newtonsoft.Json,"))
                {
                    using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AV_Tool.Resources.Newtonsoft.Json.dll"))
                    {
                        byte[] assemblyData = new byte[stream.Length];

                        stream.Read(assemblyData, 0, assemblyData.Length);
                        return Assembly.Load(assemblyData);
                    }
                }
                return null;
            });
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Updater.CheckNewestVersion();
            gui = new GUI();
            loginPrompt = new LoginForm();

            Application.Run(gui);
        }
    }
}
