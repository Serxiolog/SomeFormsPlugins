using FormForLab.BisnesLogic;
using Database.Implements;
using Microsoft.Extensions.DependencyInjection;

namespace FormForLab
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}