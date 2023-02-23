using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Runtime.Remoting.Channels;

namespace Pelotas
{
    public class Nieve
    {
        int index;
        Size space;
        public Color c;
        // Variables de posición
        public float x;
        public float y;

        // Variables de velocidad
        private float vx;
        private float vy;

        // Variable de radio
        public float radio;

        // Tiempo de vida
        public int lifespan;
        //public Random rndm = new Random();

        // Constructor
        public Nieve(Random rand,Size size, int index)
        {
            lifespan    = rand.Next(200, 255);
            this.radio  = rand.Next(5, 10);
            this.x      = rand.Next((int)radio, size.Width - (int)radio);
            this.y      = radio + 20;         
            c           = Color.FromArgb(lifespan, rand.Next(250, 255), rand.Next(235, 255), rand.Next(240, 255));

            // Velocidades iniciales
            this.vx = 0;
            this.vy = rand.Next(1,6);

            this.index = index;
            space = size;
        }

        // Método para actualizar la posición de la pelota en función de su velocidad
        public void Update(float deltaTime, List<Nieve> balls, Random rand)
        {
            int lifespanIncrease = (int)(this.y / 150); // Cambiar el factor de aumento según sea necesario
            lifespan -= lifespanIncrease;

            if (lifespan <= 0)
            {
                vy = rand.Next(1, 6);
                lifespan = 255;
                y = radio + 20;
            }

            if ((x - radio) <= 0 || (x + radio) >= space.Width)
            {
                if (x - radio <= 0)
                    x = radio + 3;
                else
                    x = space.Width - radio-3;
                    
                //vx *= -.01f;
                vy *= .5f;
            }
            
            if ((y - radio) <= 0 || (y + radio) >= space.Height)
            {
                if (y - radio<=  0)
                    y = radio + 3;
                else
                    y = space.Height - radio-3;

                
                //vx *=  .01f;
                vy *= -.5f;
            }

            //this.x += this.vx + .55f; // * deltaTime;
            this.y += this.vy + 4; // * deltaTime;
        }
    }

}
