using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;
using GXPEngine;


public class Level : GameObject
{
    readonly TiledLoader loader;
    public Level(string filename)
    {
        loader = new TiledLoader(filename);
        CreateLevel();
    }
        void CreateLevel(bool IncludeImageLayer = true)
    {
        loader.autoInstance = true;
        loader.rootObject = this;

        loader.addColliders = false;
        loader.LoadImageLayers();

        loader.LoadTileLayers(0); // background (skybox/ scenery)
        loader.LoadTileLayers(1); // background and props
        loader.addColliders = true;
        loader.LoadTileLayers(2); // platforms and walls (everything that is collidable)
        loader.LoadObjectGroups();

    }
}
