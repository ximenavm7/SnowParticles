using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Pelotas.Properties;

namespace Pelotas
{
    public partial class Particulas : Form
    {
        static List<Nieve> balls;
        static Bitmap bmp = Resources.fondo;
        static Graphics g;
        static Random rand = new Random();
        static float deltaTime;
        static List<Nube> nubes;

        public Particulas()
        {
            InitializeComponent();
        }

        private void Init()
        {
            if (PCT_CANVAS.Width == 0)
                return;
            nubes        = new List<Nube>();
            balls       = new List<Nieve>();
            bmp         = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
            g           = Graphics.FromImage(bmp);
            deltaTime   = 1;
            PCT_CANVAS.Image = bmp;

            // Emisor
            nubes.Add(new Nube((PCT_CANVAS.Width / 2) - 50, 5, 160, 160, Color.LightGray, 0));
            nubes.Add(new Nube(200, 5, 160, 160, Color.LightBlue, 1));
            nubes.Add(new Nube((PCT_CANVAS.Width - 300), 5, 160, 160, Color.LightBlue, 1));


            for (int b = 0; b < 200; b++)
            {
                balls.Add(new Nieve(rand, PCT_CANVAS.Size, b));
            }
        }

        private void Pelotas_Load(object sender, EventArgs e)
        {
            Init();
            this.BackgroundImage = Resources.fondo;
        }

        private void Pelotas_SizeChanged(object sender, EventArgs e)
        {
            Init();
        }

        private void TIMER_Tick(object sender, EventArgs e)
        {
            g.DrawImage(this.BackgroundImage, 0, 0, this.Width, this.Height);
            Parallel.For(0, balls.Count, b =>//ACTUALIZAMOS EN PARALELO
            {
                Nieve P;
                balls[b].Update(deltaTime, balls, rand);
                if (balls[b].lifespan <= 0)
                {
                    balls[b].y = balls[b].radio + 4;
                }
                P = balls[b];               
            });

            Nieve p;

            for (int b = 0; b < balls.Count; b++)//PINTAMOS EN SECUENCIA
            {
                p = balls[b];
                //g.FillEllipse(new SolidBrush(p.c), p.x - p.radio, p.y - p.radio, p.radio * 2, p.radio * 2);

                // Crear un nuevo color con la opacidad proporcional al tiempo de vida
                Color newColor = Color.FromArgb(p.lifespan, p.c.R, p.c.G, p.c.B);

                // Dibujar la pelota con el nuevo color
                Brush brush = new SolidBrush(newColor);
                g.FillEllipse(brush, p.x - p.radio, p.y - p.radio, 2 * p.radio, 2 * p.radio);
                brush.Dispose();
            }
            for (int b = 0; b < 3; b++)
            {
                nubes[b].Dibujar(g);
            }
            

            PCT_CANVAS.Invalidate();
            deltaTime += .1f;
        }
    }
}
