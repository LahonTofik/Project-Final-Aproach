using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Spawner : AnimationSprite
{
    MyGame myGame;
    bool player;
    bool turret;
    bool start = true;
    TiledObject tiledObject;
    Vec2 startcheck;
    public List<LineSegment> lines;
    public Spawner(String fileName, int cols, int rows, TiledObject obj = null) : base("Assets/floor_tile_1.png", cols, rows)
    {
        alpha = 0;
        tiledObject = obj;
        myGame = (MyGame)game;
        if (obj != null)
        {
            player = obj.GetBoolProperty("isPlayer");
            turret = obj.GetBoolProperty("isTurret");
        }
    }
    void Update()
    {
        if (start && player)
        {
            myGame.AddPlayer(new PlayerBall(10, new Vec2(x, y), new Vec2(), 1, new Vec2(0, 0.75f), true, true));
            start = false;
        }
        if (start && turret)
        {
            myGame.AddChild(new Turret(new Vec2(x, y)));
            start = false;
        }
    }
}