using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Focus.globals;

namespace Focus.entities
{
    class Player : Entity
    {
        public const int WIDTH = 18;
        public const int HEIGHT = 18;

        public Player(Vector2 position, Vector2 speed)
            : base(position, speed, "player")
        { }

        public override void Update()
        {
            //y-axis movement
            if (Input.isKeyDown(Keys.W))
            {
                this.position.Y -= this.speed.Y;
            }
            else if (Input.isKeyDown(Keys.S))
            {
                this.position.Y += this.speed.Y;
            }

            //x-axis
            if (Input.isKeyDown(Keys.A))
            {
                this.position.X -= this.speed.X;
            }
            else if (Input.isKeyDown(Keys.D))
            {
                this.position.X += this.speed.X;
            }
        }

        /*public override Rectangle Size
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, WIDTH, HEIGHT);
            }
        }*/
    }
}
