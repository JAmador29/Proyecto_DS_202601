using System;
using System.Windows.Forms;

namespace Proyecto_G4
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Corrige el escalado DPI para que el diseño se vea igual en ejecución
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Agrega esta línea para DPI alto (Windows 10/11)
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.Run(new Form1());
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}