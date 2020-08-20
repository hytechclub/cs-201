using System;

namespace FlyingWizard2D
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new FlyingWizardGame())
                game.Run();
        }
    }
}
