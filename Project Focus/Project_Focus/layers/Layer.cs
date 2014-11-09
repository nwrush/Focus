using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Focus.entities;

namespace Focus.layers
{
    class Layer
    {
        List<Entity> entities;
        public RenderTarget2D RenderTarget;
        public Texture2D Buffer;

        public Color BackgroundColor = Color.Black;

        public Layer()
        {
            entities = new List<Entity>();
        }

        public virtual void Update()
        {
            if (entities.Count != 0)
            {
                List<int> entitiesToRemove = new List<int>();
                for (int i = 0; i < this.entities.Count; ++i)
                {
                    entities[i].Update();

                    if (entities[i].Dead)
                        entitiesToRemove.Add(i);
                }
                foreach (int i in entitiesToRemove)
                {
                    entities.RemoveAt(i);
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (entities.Count != 0)
            {
                foreach (Entity e in entities)
                {
                    e.Draw(spriteBatch);
                }
            }
        }

        /// <summary>
        /// Add an entity to the scene
        /// </summary>
        public virtual void add(Entity e)
        {
            entities.Add(e);
        }

        public virtual void remove(Entity e)
        {
            entities.Remove(e);
        }
    }
}
