using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Focus.globals;

namespace Focus.layers
{

    class TileLayer : Layer
    {
        static TileLayer()
        {
            translationDictionary = new Dictionary<Tuple<int, int, int>, GameObjects>();

            translationDictionary.Add(new Tuple<int, int, int>(0, 0, 0), GameObjects.Wall);
            translationDictionary.Add(new Tuple<int, int, int>(127, 127, 127), GameObjects.Obstacle);
            translationDictionary.Add(new Tuple<int, int, int>(163, 73, 164), GameObjects.Player);
            translationDictionary.Add(new Tuple<int, int, int>(255, 242, 0), GameObjects.Key);
            translationDictionary.Add(new Tuple<int, int, int>(255, 0, 0), GameObjects.Boss);
        }
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
            GenerateMapFromImage(templateName);
            return result;
        }

        private static Dictionary<Tuple<int, int, int>, GameObjects> translationDictionary;
        [Obsolete("Doesn't work. I blame MS")]
        private static void GenerateMapFromImage(string template)
        {
            Texture2D img = GV.contentManager.Load<Texture2D>(template);
            Color[] colorData = new Color[img.Height * img.Width];
            img.GetData<Color>(colorData);

            GameObjects[,] levelArray = new GameObjects[img.Width, img.Height];
            //convert 1D to 2D
            Color c;
            Tuple<int, int, int> color;

            int cnt = 0;
            for (int j = 0; j < img.Height; ++j)
            {
                for (int i = 0; i < img.Width; ++i)
                {
                    c = colorData[i];
                    color = new Tuple<int, int, int>(c.R, c.G, c.B);
                    if (translationDictionary.ContainsKey(color))
                    {
                        levelArray[i, j] = translationDictionary[color];
                    }
                    else
                    {
                        levelArray[i, j] = GameObjects.Floor;
                    }
                    ++cnt;
                }
            }
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

        public uint getTileAt(int x, int y)
        {
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
    public enum GameObjects
    {
        Floor,
        Wall,
        Obstacle,
        Player,
        Key,
        Boss
    };
}
