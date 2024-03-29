﻿using System;
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

        public Color BackgroundColor = Color.Black;

        public Layer()
        {
            entities = new List<Entity>();
        }

        public virtual void UpdateEntities()
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

        public virtual void Update()
        {
            UpdateEntities();

            
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

        public virtual void setWidth()
        {

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

        public virtual int width
        {
            get { return RenderTarget != null ? RenderTarget.Width : -1; }
        }
        public virtual int height
        {
            get { return RenderTarget != null ? RenderTarget.Height : -1; }
        }
    }

}
