using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Focus.globals;

namespace Focus.entities
{
    class Entity
    {
        protected Vector2 position;
        protected Vector2 speed;
        protected Texture2D texture;

        //draw the entity to the screen
        bool isDead;

        public Entity(string contentName)
        {
            isDead = false;
            LoadContent(contentName);
        }
        public Entity(Vector2 pos, Vector2 speed, string contentName)
            : this(contentName)
        {
            this.position = pos;
            this.speed = speed;
        }

        protected virtual void LoadContent(string contentName)
        {
            Microsoft.Xna.Framework.Content.ContentManager cm = GV.contentManager;
            this.texture = cm.Load<Texture2D>(contentName);
        }

        public virtual void Update()
        {
            this.position += this.speed;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.position, null, Color.White);
        }

        //Accessors
        public Boolean Dead
        {
            get { return this.isDead; }
            set { this.isDead = value; }
        }

    }
}
