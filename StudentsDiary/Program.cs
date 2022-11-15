using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsDiary
{
    internal static class Program
    {

        public static string FilePath = Path.Combine(Environment.CurrentDirectory, "students.txt");
        public static List<string> ComboBoxItems = new List<string>(){
        "Klasa 0",
        "Klasa 1",
        "Klasa 2",
        "Klasa 3",
        "Klasa 4",
        "Klasa 5",
        "Klasa 6",
        "Klasa 7",
        "Klasa 8", };

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
