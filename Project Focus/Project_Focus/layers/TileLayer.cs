using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Focus.layers {

    class TileLayer : Layer
    {
        public bool focused = false;
        public bool dbgShowTiles = true;

        public const int TILESIZE = 18;

        private Texture2D texture;
        private Texture2D blank;

        private uint[,] tiles;

        private TileLayer(int width, int height, string contentName)
        {
            tiles = new uint[width, height];
            LoadContent(contentName);
        }

        public static TileLayer FromTemplateImage(string templateName, string textureName)
        {
            Microsoft.Xna.Framework.Content.ContentManager cm = globals.GV.contentManager;
            Texture2D img = cm.Load<Texture2D>(templateName);
            TileLayer result = new TileLayer(img.Width, img.Height, textureName);

            //Todo: load tiles in from image

            return result;
        }

        public static TileLayer FromArray(uint[,] array, string textureName)
        {
            TileLayer result = new TileLayer(array.GetLength(0), array.GetLength(1), textureName);
            result.tiles = array;
            return result;
        }

        protected virtual void LoadContent(string contentName)
        {
            Microsoft.Xna.Framework.Content.ContentManager cm = globals.GV.contentManager;
            this.texture = cm.Load<Texture2D>(contentName);
            blank = cm.Load<Texture2D>("white1x1");
        }

        public uint getTileAt(int x, int y) {
            return tiles[x, y];
        }

        public Color getTilecolor(uint tileId)
        {
            switch (tileId)
            {
                case 0:
                    return Color.Transparent;
                case 1:
                    return Color.Red;
                case 2:
                    return Color.Blue;
                default:
                    return Color.Magenta;
            }
        }
        
        public Color makeTransparent(Color c, byte a)
        {
            c.A &= a;
            return c;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawTiles(spriteBatch);
            base.Draw(spriteBatch);
        }

        public virtual void DrawTiles(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, new Rectangle(0, 0, width * TILESIZE, height * TILESIZE), Color.White);
            if (dbgShowTiles)
            {
                Rectangle tileBnds = new Rectangle(0, 0, TILESIZE, TILESIZE);
                for (int x = 0; x < width; ++x)
                {
                    for (int y = 0; y < height; ++y)
                    {
                        tileBnds.X = x * TILESIZE;
                        tileBnds.Y = y * TILESIZE;

                        spriteBatch.Draw(blank, tileBnds, makeTransparent(getTilecolor(tiles[x, y]), 128));
                    }
                }
            }
        }

        public int width
        {
            get { return tiles.GetLength(0); }
        }
        public int height
        {
            get { return tiles.GetLength(1); }
        }
    }
}
