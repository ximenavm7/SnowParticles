using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pelotas
{
    public class Nube
    {
        private int x, y, width, height, index;
        private Color color;

        public Nube(int x, int y, int width, int height, Color color, int index)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.color = color;
            this.index = index;
        }

        public void Dibujar(Graphics g)
        {
            // Dibuja la nube con una serie de elipses superpuestas
            Brush brush = new SolidBrush(color);
            g.FillEllipse(brush, x, y, width, height);
            g.FillEllipse(brush, x + 100, y - height/7, width, height);
            g.FillEllipse(brush, x + 200, y + height/7, width, height);
            g.FillEllipse(brush, x - 100, y - height/7, width, height);
            g.FillEllipse(brush, x - 200, y + height/7, width, height);
            brush.Dispose();
        }
    }
}
