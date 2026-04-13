namespace Apprendre
{
    internal static class Program
    {
        /// <summary>
        ///  Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Pour personnaliser la configuration de l'application, comme les paramètres DPI élevés
            // ou la police par défaut, consultez https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Apprendre());
        }
    }
}       