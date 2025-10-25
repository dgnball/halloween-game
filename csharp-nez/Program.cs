using System;

namespace HalloweenGame
{
    /// <summary>
    /// The main entry point for the Halloween Candy Dash game.
    /// </summary>
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new HalloweenGameMain())
            {
                game.Run();
            }
        }
    }
}
