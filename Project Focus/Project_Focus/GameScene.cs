﻿using System;
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
        public static int ScalingConstant = 1;

        private uint[,] testLevel = { { 0, 1 }, { 2, 3 } };
        private Player player;
        private Effect testEffect;
        private RenderTarget2D ppBuffer;

        public GameScene() {
            layers = new List<TileLayer>();
            player = new Player(Vector2.Zero, new Vector2(.5f));

            layers.Add(TileLayer.FromTemplateImage("7c","sketch"));
            layers.Add(TileLayer.FromArray(testLevel, "sketch"));
            layers[0].BackgroundColor = Color.Green;
            layers[0].add(player);

            layers.Add(TileLayer.FromArray(testLevel, "white1x1"));
            layers[1].BackgroundColor = Color.Goldenrod;
            layers[1].add(player);


            layers.Add(TileLayer.FromArray(testLevel, "sketch"));
            layers[2].BackgroundColor = Color.SkyBlue;
            layers[2].add(player);

            layers.Add(TileLayer.FromArray(testLevel, "white1x1"));
            layers[3].BackgroundColor = Color.Red;
            layers[3].add(player);

            testEffect = globals.GV.contentManager.Load<Effect>("ripple");
        }

        public void CreateRenderTargets(GraphicsDevice device)
        {
            foreach (Layer l in layers)
            {
                l.RenderTarget = 
                    new RenderTarget2D(
                        device, 
                        device.PresentationParameters.BackBufferWidth / (2 * ScalingConstant),
                        device.PresentationParameters.BackBufferHeight / (2 * ScalingConstant)
                    );
            }
            ppBuffer = new RenderTarget2D(device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight);
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

        public void Draw(GameTime gt, GraphicsDevice device, SpriteBatch sb)
        {
            int n = layers.Count;

            foreach (Layer l in layers)
            {
                device.SetRenderTarget(l.RenderTarget);
                device.Clear(l.BackgroundColor);
                sb.Begin();
                l.Draw(sb);
                sb.End();
                device.SetRenderTarget(null);
            }

            for (int i = 0; i < n; ++i )
            {
                Layer l = layers[i];

                int width = (device.PresentationParameters.BackBufferWidth / 2);
                int height = (device.PresentationParameters.BackBufferHeight / 2);

                device.SetRenderTarget(ppBuffer);
                testEffect.Parameters["Time"].SetValue((float)gt.TotalGameTime.TotalMilliseconds / 1000.0f);
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, testEffect);
                sb.Draw(
                    l.RenderTarget,
                    new Rectangle(
                        (i % 2) * width,
                        (i / 2) * height,
                        width,
                        height
                    ),
                    Color.White
                );
                sb.End();
            }

            device.SetRenderTarget(null);

            sb.Begin();
            sb.Draw(ppBuffer, device.PresentationParameters.Bounds, Color.White);
            sb.End();
        }
    }
}
