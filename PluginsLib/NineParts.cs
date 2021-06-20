using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PluginInterface;
using System.Drawing.Imaging;

namespace PluginsLib
{
    class NineParts : IPlugin
    {
        public string Name
        {
            get
            {
                return "9 частей";
            }
        }

        public string Author
        {
            get
            {
                return "Me";
            }
        }
        Random rng = new Random();

        public Bitmap Transform(Bitmap app)
        {
            int dividerX = app.Width / 3, dividerY = app.Height / 3;
            Bitmap[] bitmapList = new Bitmap[9];
            int k = 0;
            for (int i = 0; i < app.Width - 1; i += dividerX)
            {
                for (int j = 0; j < app.Height - 1; j += dividerY)
                {
                    bitmapList[k] = app.Clone(new Rectangle(i, j, dividerX, dividerY), app.PixelFormat);
                    k++;
                }
            }
            int n = bitmapList.Length;
            while (n > 1)
            {
                n--;
                int o = rng.Next(n + 1);
                Bitmap value = bitmapList[o];
                bitmapList[o] = bitmapList[n];
                bitmapList[n] = value;
            }
            for (int i = 0; i < app.Width; i ++)
            {
                for (int j = 0; j < app.Height; j ++)
                {
                    switch (i / dividerX)
                    {
                        case 0: k = j / dividerY; if (k == 3) k = 2; break;
                        case 1: k = 3 + j / dividerY; if (k == 3) k = 5; break;
                        case 2: k = 6 + j / dividerY; if (k == 3) k = 8; break;
                    }
                    app.SetPixel(i, j, bitmapList[k].GetPixel(i % dividerX, j % dividerY));
                }
            }
            return app;
        }
    }
}
