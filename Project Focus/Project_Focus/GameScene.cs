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
            layers[0].BackgroundColor = Color.Green;

            layers.Add(TileLayer.FromArray(testLevel, "white1x1"));
            layers[1].BackgroundColor = Color.Goldenrod;
            layers[1].add(new Player(Vector2.Zero, new Vector2(1f)));
        }

        public void CreateRenderTargets(GraphicsDevice device)
        {
            foreach (Layer l in layers)
            {
                l.RenderTarget = 
                    new RenderTarget2D(
                        device, 
                        device.PresentationParameters.BackBufferWidth,
                        device.PresentationParameters.BackBufferHeight
                    );
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

            for (int i = 0; i < n; ++i)
            {
                Layer l = layers[i];
                int width = (device.PresentationParameters.BackBufferWidth / n) * i;
                
                device.SetRenderTarget(l.RenderTarget);
                device.Clear(l.BackgroundColor);
                sb.Begin();
                l.Draw(sb);
                sb.End();
                device.SetRenderTarget(null);
                sb.Begin();
                sb.Draw(
                    l.RenderTarget,
                    new Rectangle(
                        width * i,
                        0,
                        width, 
                        device.PresentationParameters.BackBufferHeight/2
                    ),
                    Color.White
                );
                sb.End();
            }
        }
    }
}
