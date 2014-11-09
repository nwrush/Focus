using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Focus.globals;


namespace Focus.entities {
    class Button:Entity {
        Texture2D hoverTexture, clickTexture;
        public Button(string baseName)
            : base(baseName) {
        }

        protected virtual void LoadContent(string contentName)
        {
           texture = GV.contentManager.Load<Texture2D>(contentName);
           hoverTexture = GV.contentManager.Load<Texture2D>(contentName + "_hover");
           clickTexture = GV.contentManager.Load<Texture2D>(contentName + "_click");
        }
    }
}
