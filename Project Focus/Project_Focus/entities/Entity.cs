using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Focus.entities
{
    class Entity
    {
        protected Vector2 position;
        protected Vector2 speed;
        protected Texture2D texture;

        //draw the entity to the screen
        bool isDead;

        public Entity()
        {
            isDead = false;
        }
        public Entity(Vector2 pos, Vector2 speed)
            : this()
        {
            this.position = pos;
            this.speed = speed;
        }

        public virtual void LoadContent(string contentName)
        {
            
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        //Accessors
        public Boolean Dead
        {
            public get { return this.isDead; }
            public set { this.isDead = value; }
        }

    }
}
