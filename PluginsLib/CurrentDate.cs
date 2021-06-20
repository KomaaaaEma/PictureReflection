using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PluginInterface;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace PluginsLib
{
    class CurrentDate : IPlugin
    {
        public string Name
        {
            get
            {
                return "Текущая дата";
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
            RectangleF rectF = new RectangleF(new PointF(800, 750), new SizeF(400, 50));
            Graphics g = Graphics.FromImage(app);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.DrawString($"{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}", new Font("Times New Roman", 40), Brushes.Black, rectF);
            g.Flush();
            return app;
        }
    }
}
