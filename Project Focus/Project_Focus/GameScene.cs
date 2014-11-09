using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Focus.entities;
using Focus.layers;

namespace Focus {
    class GameScene {
        List<TileLayer> layers;
        public int currentLayer = 0;

        private uint[,] testLevel = { { 0, 1 }, { 2, 3 } };

        public GameScene() {
            layers = new List<TileLayer>();
            layers.Add(TileLayer.FromArray(testLevel, "sketch"));
        }

        public void setFocusedLayer(int layer) {
            currentLayer = layer;
        }

        public int getFocusedLayer()
        {
            return currentLayer;
        }

        public void Update(GameTime gt)
        {
            foreach (Layer l in this.layers)
            {
                l.Update();
            }
        }

        public void Draw(SpriteBatch sb)
        {
            layers[currentLayer].Draw(sb);
        }
    }
}
