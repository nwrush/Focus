using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Focus.globals;


namespace Focus.entities {
    class Button:Entity {
        Texture2D baseTexture, hoverTexture, clickTexture;
        Rectangle buttonRect;
        bool hovered = false;
        bool clicked = false;
        public bool clickReleased = false;
        
        public Button(string baseName, Vector2 pos)
            : base(baseName) {
                position = pos;
                buttonRect = new Rectangle(285, 285, 285, 35);
        }

        protected virtual void LoadContent(string contentName)
        {
           baseTexture = GV.contentManager.Load<Texture2D>(contentName + "_base");
           hoverTexture = GV.contentManager.Load<Texture2D>(contentName + "_hover");
           clickTexture = GV.contentManager.Load<Texture2D>(contentName + "_click");
           texture = baseTexture;
        }

        public override void Update() {
            clicked = false;
            hovered = false;
            clickReleased = false;

            if ((Input.getPos().X >= buttonRect.X 
                || Input.getPos().X <= buttonRect.Right) 
                || (Input.getPos().Y >= buttonRect.Y 
                || Input.getPos().Y <= buttonRect.Bottom)) {
                    if (Input.isLeftMouseDown()) {
                        clicked = true;
                    }
                    else if (Input.isLeftMouseReleased()) {
                        clickReleased = true;
                    }
                    else {
                        hovered = true;
                    }
            }

            if (clicked) {
                texture = clickTexture;
            }
            else if (hovered) {
                texture = hoverTexture;
            }
            else {
                texture = baseTexture;
            }

            
        }
    }
}
