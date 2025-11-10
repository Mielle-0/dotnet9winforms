using it13Project.Forms;

namespace it13Project
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();


            Application.Run(new Form1());

            // CurrentUser.UserId = 2;
            // CurrentUser.Name = "admin";
            // CurrentUser.Role = "System Administrator";
            // Application.Run(new MainForm());

        }
    }
}