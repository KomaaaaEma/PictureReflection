using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PluginInterface;

namespace PluginsLib
{
    class black_and_white_transform : IPlugin
    {
        public string Name
        {
            get
            {
                return "ЧБ";
            }
        }

        public string Author
        {
            get
            {
                return "Me";
            }
        }

        public Bitmap Transform(Bitmap app)
        {
            for (int i = 0; i < app.Width; i++)
            {
                for (int j = 0; j < app.Height; j++)
                {
                    Color currentColor = app.GetPixel(i, j);
                    int avgColor = (currentColor.R + currentColor.G + currentColor.B) / 3;
                    app.SetPixel(i, j, Color.FromArgb(avgColor, avgColor, avgColor));
                }
            }
            return app;
        }
    }
}
