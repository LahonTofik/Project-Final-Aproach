using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;
using GXPEngine;
using GXPEngine.Core;
using GXPEngine.Managers;
using GXPEngine.OpenGL;


public class Level : GameObject
{
    MyGame myGame;
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

        loader.LoadTileLayers(); // background (skybox/ scenery)
        loader.LoadTileLayers(1); // background and props
        loader.addColliders = true;
        loader.LoadTileLayers(2); // platforms and walls (everything that is collidable)
        loader.LoadObjectGroups();

    }
    void CreateCollisions()
    {
        CollisionLine[] lines = FindObjectOfType<CollisionLine>();
        foreach (var line in lines)
        {
            CollisionLine cLine = line as CollisionLine;
            var corners = cLine.GetExtents();

            for (int i = 0; 0 < corners; i++)
            {
                if (i != corners)
                {
                    // top line
                    myGame.AddChild(new LineSegment(new Vec2(corners[i].x, corners[i].y), new Vec2(corners[i + 1].x, corners[i + 1].y)));
                }
                else
                {
                    MyGame.AddChild(new LineSegment(new Vec2(corners[i].x, corners[i].y), new Vec2(corners[0].x, corners[0].y)));
                }
            }
        }

    }
}
