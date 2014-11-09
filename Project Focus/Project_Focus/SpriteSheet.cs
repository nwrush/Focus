using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Focus {
    struct Animation {
        public string name;
        public int[] frames;
        public int frameRate;
    }

    public class SpriteSheet {
        private Texture2D texture;
        private Rectangle frameRect;
        private Animation currentAnim;
        private int frame, width, height, frameRate, animCount, iterator, tileWidth, tileHeight;
        private bool looping;
        private Dictionary<string,Animation> animations;

        public SpriteSheet(Texture2D source, int frameWidth, int frameHeight) {
            texture = source;
            frameRect = new Rectangle(0, 0, frameWidth, frameHeight);
            frame = 0;
            width = texture.Width;
            height = texture.Height;
            tileWidth = texture.Width / frameWidth;
            tileHeight = texture.Height / frameHeight;
            frameRate = 0;
            animations = new Dictionary<string, Animation>();
            Animation a = new Animation();
            a.frames = new int[1];
            a.frames[0] = 0;
            a.frameRate = 1;
            a.name = "idle";
            iterator = 0;
            animCount = 0;
            animations.Add("idle", a);
            Play("idle", true);
        }

        public void Add(string name, int[] frames, int rate) {
            Animation a = new Animation();
            a.name = name;
            a.frames = frames;
            a.frameRate = rate;
            animations.Add(name,a);
        }

        public void Play(string name, bool loop = false) {
            if (!animations.ContainsKey(name)) {
                Play("idle");
            }
            else if(currentAnim.name == name){
            }
            else {
                currentAnim = animations[name];
                looping = loop;
                animCount = 0;
                frameRate = currentAnim.frameRate;
                iterator = 0;
            }
        }

        public void Update() {
            iterator++;
            iterator %= frameRate;
            if (iterator == 0) {
                animCount++;
                if (animCount >= currentAnim.frames.Length) {
                    if (looping) {
                        animCount = 0;
                    }
                    else {
                        Play("idle", true);
                    }
                }
            }
            frame = currentAnim.frames[animCount];
            this.frameRect.X = (frame % tileWidth) * frameRect.Width; 
            this.frameRect.Y = (frame / tileWidth) * frameRect.Height;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position) {
            spriteBatch.Draw(texture, position, frameRect, Color.White);
        }

        public void DrawScaled(SpriteBatch spriteBatch, Rectangle rect) {
            spriteBatch.Draw(texture, rect, frameRect, Color.White);
        }

    }
}
