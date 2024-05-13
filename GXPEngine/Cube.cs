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
    Vec2 startcheck;
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
        AddLine(new Vec2(tiledObject.X, tiledObject.Y - tiledObject.Height), new Vec2(tiledObject.X + tiledObject.Width, tiledObject.Y - tiledObject.Height), true, true, true);//left top to right top
        AddLine(new Vec2(tiledObject.X, tiledObject.Y - tiledObject.Height), new Vec2(tiledObject.X, tiledObject.Y), true, true, true);//left top to left bot
        AddLine(new Vec2(tiledObject.X, tiledObject.Y), new Vec2(tiledObject.X + tiledObject.Width, tiledObject.Y), true, true, true);//left bpt to right bort
        AddLine(new Vec2(tiledObject.X + tiledObject.Width, tiledObject.Y - tiledObject.Height), new Vec2(tiledObject.X + tiledObject.Width, tiledObject.Y), true, true, true);//right bot to right top
    }
    public void AddLine(Vec2 start, Vec2 end, bool specialCol = false, bool twosided = false, bool addcaps = false)
    {
        LineSegment line = new LineSegment(start, end, specialCol ? 0xffff00ff : 0xff00ff00, 4);
        myGame.AddChild(line);
        myGame._lines.Add(line);
        if (twosided) AddLine(end, start, true, false); // :-)
        startcheck = line.start;
    }
    void Update()
    {
        Console.WriteLine(startcheck);
    }
}