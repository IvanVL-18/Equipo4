using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Equipo4
{
    public class Images
    {
        public readonly static ImageSource Emty = LoadImage("Emty.png");
        public readonly static ImageSource Body = LoadImage("Body.png");
        public readonly static ImageSource Head = LoadImage("Head.png");
        public readonly static ImageSource Food = LoadImage("Food.png");
        public readonly static ImageSource DeadBody = LoadImage("DeadBody.png");
        public readonly static ImageSource DeadHead = LoadImage("DeadHead.png");

        private static ImageSource LoadImage(String fileName)
        {
            return new BitmapImage(new Uri($"Assts/{fileName}", UriKind.Relative));
        }

    }
}
