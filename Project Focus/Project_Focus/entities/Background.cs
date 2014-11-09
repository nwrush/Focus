using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Focus.globals;


namespace Focus.entities {
    class Background:Entity {
        public Background(string name)
            : base(name) {
                position = new Vector2();
        }
    }
}
