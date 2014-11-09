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

        public void CreateRenderTargets(GraphicsDevice device)
        {
            foreach (Layer l in layers)
            {
                l.RenderTarget = 
                    new RenderTarget2D(
                        device, 
                        device.PresentationParameters.BackBufferWidth,
                        device.PresentationParameters.BackBufferHeight);
            }
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

        public void Draw(GraphicsDevice device, SpriteBatch sb)
        {
            int n = layers.Count;

            foreach (Layer l in layers)
            {
                device.SetRenderTarget(l.RenderTarget);
                device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, l.BackgroundColor, 1.0f, 0);
                l.Draw(sb);
                device.SetRenderTarget(null);

                sb.Draw(l.RenderTarget, device.PresentationParameters.BackBufferWidth)
            }
        }
    }
}
