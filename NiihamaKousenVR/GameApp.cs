using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NiihamaKousenVR.Worlds;

namespace NiihamaKousenVR
{
    public class GameApp
    {
        public static OpeningWorld OpeningWorld { get; private set; }

        public static MainWorld MainWorld { get; private set; }

        public static void Initialize()
        {
            OpeningWorld = new OpeningWorld();
        }

        public static async Task LoadClassesAsync()
        {
            MainWorld = new MainWorld();
        }
    }
}
