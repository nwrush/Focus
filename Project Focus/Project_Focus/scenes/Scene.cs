using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Focus.entities;

namespace Focus.scenes {

    class Scene {
        List<Entity> entities;



        public Scene() {
            entities = new List<Entity>();
        }

        public virtual void Update() {
            if (entities.Count != 0) {
                foreach (Entity e in entities) {
                    e.Update();

                    //check for e.kill == true
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            if (entities.Count != 0) {
                foreach (Entity e in entities) {
                    e.Draw(spriteBatch);
                }
            }
        }

        /// <summary>
        /// Add an entity to the scene
        /// </summary>
        protected virtual void add(Entity e) {
            entities.Add(e);
        }

        protected virtual void remove(Entity e) {
            entities.Remove(e);
        }
    }
}
