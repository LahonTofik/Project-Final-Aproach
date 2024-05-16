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
    PlayerBall player;
    readonly TiledLoader loader;
    float prevX;
    public Level(string filename)
    {
        myGame = (MyGame)game;
        loader = new TiledLoader(filename);
        CreateLevel();
        player = myGame.FindObjectOfType<PlayerBall>();
        //game.AddChild(myGame.colliderHolder);
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
        loader.LoadTileLayers(); // platforms and walls (everything that is collidable)
        loader.LoadObjectGroups();
        

    }
    /*void HandleScroll()
    {
        if (player == null)
        {
            Console.WriteLine("ok");
            return;
        }
        int boundarySizex = 500;
        if (player.x + x < boundarySizex)
        {
            x = boundarySizex - player.x;
        }
        if (player.x + x > game.width - boundarySizex)
        {
            x = game.width - boundarySizex - player.x;
        }
        if (prevX != x)
        {
            Console.WriteLine("ok");
            float diff = x - prevX;
            foreach (LineSegment line in myGame._lines)
            {
                line.start.x += diff;
                line.end.x += diff;
            }

        }
        prevX = x;
    }*/
    void GameBoundary()
    {
        if (player == null) return;
        if (x > 0) x = 0;
        if (-x >= game.width*2) x = (game.width*-2);
        if (-y >= game.height) y = (game.height) * -1;
        if (y > 0) y = 0;
    }


    void Update()
    {
        /*HandleScroll();*/
        GameBoundary();
    }
}
