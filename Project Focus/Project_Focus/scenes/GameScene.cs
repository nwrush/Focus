using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Focus.entities;


namespace Focus.scenes {
    class GameScene:Scene {
        Tile[, ,] layers;

        ///change this from static const later
        const int LAYERCOUNT = 1;
        int levelWidth = 10;
        int levelHeight = 10;
        int currentLayer = 0;

        


        public GameScene() {
            layers = new Tile[LAYERCOUNT, levelWidth, levelHeight];
        }

        private void setLayer(int layer) {
            setLayerFocus(currentLayer, false);
            currentLayer = layer;
            setLayerFocus(currentLayer, true);
        }

        private void setLayerFocus(int layer, bool focus) {
            for (int i = 0; i < levelWidth; i++) {
                for (int j = 0; j < levelHeight; j++) {
                    layers[layer, i, j].focused = focus;
                }
            }
        }
    }
}
