using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Focus.globals;

namespace Focus.layers
{
    class TileLayer : Layer
    {
        static TileLayer()
        {
            translationDictionary = new Dictionary<Tuple<int, int, int>, uint>();

            translationDictionary.Add(new Tuple<int, int, int>(0, 0, 0), (uint)Tile.Wall);
            translationDictionary.Add(new Tuple<int, int, int>(127, 127, 127), (uint)Tile.Obstacle);
            translationDictionary.Add(new Tuple<int, int, int>(163, 73, 164), (uint)GameObject.Player);
            translationDictionary.Add(new Tuple<int, int, int>(255, 242, 0), (uint)GameObject.Key);
            translationDictionary.Add(new Tuple<int, int, int>(255, 0, 0), (uint)GameObject.Boss);
        }
        public bool focused = false;
        public bool dbgShowTiles = true;

        public List<FancyRect> blocks;

        public const int TILESIZE = 16;

        private Texture2D texture;
        private Texture2D blank;

        private uint[,] tiles;

        private TileLayer(string contentName)
        {
            LoadContent(contentName);
            blocks = new List<FancyRect>();
        }

        public static TileLayer FromTemplateImage(string templateName, string textureName ="")
        {
            ContentManager cm = globals.GV.contentManager;
            Texture2D img = cm.Load<Texture2D>(templateName);
            TileLayer result = new TileLayer(textureName);

            //Todo: load tiles in from image
            result.tiles = result.GenerateMapFromImage(templateName);
            return result;
        }

        private static Dictionary<Tuple<int, int, int>, uint> translationDictionary;
        private uint[,] GenerateMapFromImage(string template)
        {
            Texture2D img = GV.contentManager.Load<Texture2D>(template);

            Color[] colorData = new Color[img.Height * img.Width];
            img.GetData<Color>(colorData);

            uint[,] levelArray = new uint[img.Width, img.Height];
            //convert 1D to 2D
            Color c;
            Tuple<int, int, int> color;

            int cnt = 0;
            for (int j = 0; j < img.Height; ++j)
            {
                for (int i = 0; i < img.Width; ++i)
                {
                    c = colorData[cnt];
                    color = new Tuple<int, int, int>(c.R, c.G, c.B);
                    if (translationDictionary.ContainsKey(color))
                    {
                        uint thing = translationDictionary[color];
                        if (thing == 0 || thing == 1 || thing == 2)
                        {
                            levelArray[i, j] = translationDictionary[color];
                            blocks.Add(new FancyRect(i * TILESIZE, j * TILESIZE, TILESIZE, TILESIZE));
                        }
                        else
                        {
                            // Todo: place entities
                        }
                    }
                    else
                    {
                        levelArray[i, j] = (uint)Tile.Floor;
                    }
                    ++cnt;
                }
            }

            return levelArray;
        }

        protected virtual void LoadContent(string contentName)
        {
            Microsoft.Xna.Framework.Content.ContentManager cm = globals.GV.contentManager;
            if (contentName != "") { this.texture = cm.Load<Texture2D>(contentName); }
            blank = cm.Load<Texture2D>("white1x1");
        }

        public uint getTileAt(int x, int y)
        {
            if (x < 0 || y < 0 || x >= tileWidth || y >= tileHeight) { return uint.MaxValue; }
            return tiles[x, y];
        }

        public bool isSolid(int x, int y)
        {
            if (x < 0 || y < 0 || x >= tileWidth || y >= tileHeight) { return true; }
            uint g = getTileAt(x, y);
            return g == (uint)Tile.Obstacle || g == (uint)Tile.Wall;
        }

        public Color getTilecolor(uint g)
        {
            switch (g)
            {
                case (uint)Tile.Floor:
                    return Color.Transparent;
                case (uint)Tile.Wall:
                    return Color.Red;
                case (uint)Tile.Obstacle:
                    return Color.Blue;
                default:
                    return Color.Magenta;
            }
        }

        public Color makeTransparent(Color c, byte a)
        {
            if (c.A != 0)
            {
                c.A = a;
            }
            return c;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawTiles(spriteBatch);
            base.Draw(spriteBatch);
        }

        public virtual void DrawTiles(SpriteBatch spriteBatch)
        {
            if (this.texture != null)
            {
                spriteBatch.Draw(this.texture, new Rectangle(0, 0, width, height), Color.White);
            }
            if (dbgShowTiles)
            {
                Rectangle tileBnds = new Rectangle(0, 0, TILESIZE, TILESIZE);
                for (int x = 0; x < tileWidth; ++x)
                {
                    for (int y = 0; y < tileHeight; ++y)
                    {
                        tileBnds.X = x * TILESIZE;
                        tileBnds.Y = y * TILESIZE;

                        spriteBatch.Draw(blank, tileBnds, makeTransparent(getTilecolor(tiles[x, y]), 128));
                    }
                }
            }
        }

        public int tileWidth
        {
            get { return tiles.GetLength(0); }
        }
        public int tileHeight
        {
            get { return tiles.GetLength(1); }
        }

        public override int width
        {
            get { return tileWidth * TILESIZE; }
        }
        public override int height
        {
            get { return tileHeight * TILESIZE; }
        }
    }
    public enum GameObject : uint
    {
        Player = 3,
        Key = 4,
        Boss = 5
    };
    public enum Tile : uint
    {
        Floor = 0,
        Wall = 1,
        Obstacle = 2
    }
}
