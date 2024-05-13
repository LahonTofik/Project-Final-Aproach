using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Cube : AnimationSprite
{
    MyGame myGame;
    TiledObject tiledObject;
    public Cube(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        alpha = 0;
        tiledObject = obj;
        myGame = (MyGame)game;
        CreateCollisions();
    }
    void CreateCollisions()
    {
        CollisionLine[] lines = FindObjectsOfType<CollisionLine>();
        foreach (var line in lines)
        {
            CollisionLine cLine = line as CollisionLine;
            var corners = cLine.GetExtents();
        }
        Console.WriteLine(tiledObject.X);
        myGame.AddLine(new Vec2(tiledObject.X, tiledObject.Y - tiledObject.Height), new Vec2(tiledObject.X+ tiledObject.Width, tiledObject.Y - tiledObject.Height), true, true, true);//left top to right top
        myGame.AddLine(new Vec2(tiledObject.X, tiledObject.Y - tiledObject.Height), new Vec2(tiledObject.X, tiledObject.Y), true, true, true);//left top to left bot
        myGame.AddLine(new Vec2(tiledObject.X, tiledObject.Y), new Vec2(tiledObject.X + tiledObject.Width, tiledObject.Y), true, true, true);//left bpt to right bort
        myGame.AddLine(new Vec2(tiledObject.X + tiledObject.Width, tiledObject.Y - tiledObject.Height), new Vec2(tiledObject.X + tiledObject.Width, tiledObject.Y), true, true, true);//right bot to right top
    }
}