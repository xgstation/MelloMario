using System;

namespace MelloMario
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new Game1().Run();
        }
    }
#endif
}
