using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Focus.entities;
using Focus.layers;

namespace Focus {
    class GameScene {
        List<TileLayer> layers;
        public int currentLayer = 0;
        public static int ScalingConstant = 1;

        public int level = 1;

        private Player player;
        private Effect testEffect;
        private RenderTarget2D ppBuffer;

        public GameScene(int level ) {
            this.level = level;
            layers = new List<TileLayer>();
            player = new Player(new Vector2(50, 50), new Vector2(.5f));

            layers.Add(TileLayer.FromTemplateImage( level + "a"));
            //layers[0].BackgroundColor = Color.Green;
            layers[0].add(player);

            layers.Add(TileLayer.FromTemplateImage(level + "b"));
            //layers[1].BackgroundColor = Color.Goldenrod;
            layers[1].add(player);


            layers.Add(TileLayer.FromTemplateImage(level + "c"));
            //layers[2].BackgroundColor = Color.SkyBlue;
            layers[2].add(player);

            layers.Add(TileLayer.FromTemplateImage(level + "d"));
            //layers[3].BackgroundColor = Color.Red;
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
                        l.width > 0 ? l.width : device.PresentationParameters.BackBufferWidth,
                        l.height > 0 ? l.height : device.PresentationParameters.BackBufferHeight
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

        private void UpdateLayers(GameTime gt)
        {
            foreach (Layer l in this.layers)
            {
                l.Update();
            }
        }

        public void Update(GameTime gt)
        {
            UpdateLayers(gt);

            if (Input.isKeyPressed(Keys.I))
            {
                currentLayer = 0;
            }
            if (Input.isKeyPressed(Keys.O))
            {
                currentLayer = 1;
            }
            if (Input.isKeyPressed(Keys.K))
            {
                currentLayer = 2;
            }
            if (Input.isKeyPressed(Keys.L))
            {
                currentLayer = 3;
            }

            PlayerCollision(layers[currentLayer]);
        }

        private void PlayerCollision(Layer layer)
        {
            if (!(layer is TileLayer)) { return; }
            TileLayer tlayer = (TileLayer)layer;

            FancyRect result = new FancyRect(player.Position.X, player.Position.Y, player.Size.Width, player.Size.Height);
            foreach (FancyRect b in tlayer.blocks) {
                result = FancyRect.CollideRect(result, b);
            }
            player.Position = new Vector2(result.x, result.y);

            if (player.Position.Y > tlayer.height)
            {
                globals.GV.Current.LoadLevelDown(level);
            }
            if (player.Position.Y < 0)
            {
                globals.GV.Current.LoadLevelUp(level);
            }
            if (player.Position.X < 0)
            {
                globals.GV.Current.LoadLevelLeft(level);
            }
            if (player.Position.X > tlayer.width)
            {
                globals.GV.Current.LoadLevelRight(level);
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
                if (l.RenderTarget == null) { continue; }

                int width = (device.PresentationParameters.BackBufferWidth / 2);
                int height = (device.PresentationParameters.BackBufferHeight / 2);
                
                device.SetRenderTarget(ppBuffer);

                testEffect.Parameters["Time"].SetValue((float)gt.TotalGameTime.TotalMilliseconds / 1000.0f);

                if (i != currentLayer)
                    sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, testEffect);
                else
                    sb.Begin();

                sb.Draw(
                    l.RenderTarget,
                    new Rectangle(
                        (i % 2) * width + (width - l.width) / 2,
                        (i / 2) * height + (height - l.height) / 2,
                        l.width,
                        l.height
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
