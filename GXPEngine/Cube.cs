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
    public List<LineSegment> lines;
    public Cube(string imageFile, int cols, int rows, TiledObject obj = null) : base(imageFile, cols, rows)
    {
        lines = new List<LineSegment>();
        alpha = 0;
        tiledObject = obj;
        myGame = (MyGame)game;
        CreateCollisions();
    }
    void CreateCollisions()
    {
       // Console.WriteLine(tiledObject.X);
        myGame.AddLine(new Vec2(tiledObject.X, tiledObject.Y - tiledObject.Height-5), new Vec2(tiledObject.X + tiledObject.Width, tiledObject.Y - tiledObject.Height-5), true, true, true);//left top to right top
        myGame.AddLine(new Vec2(tiledObject.X, tiledObject.Y - tiledObject.Height-5), new Vec2(tiledObject.X, tiledObject.Y), true, true, true);//left top to left bot
        myGame.AddLine(new Vec2(tiledObject.X, tiledObject.Y), new Vec2(tiledObject.X + tiledObject.Width, tiledObject.Y), true, true, true);//left bpt to right bort
        myGame.AddLine(new Vec2(tiledObject.X + tiledObject.Width, tiledObject.Y - tiledObject.Height-5), new Vec2(tiledObject.X + tiledObject.Width, tiledObject.Y), true, true, true);//right bot to right top
    }
    /*public int GetNumberOfLines()
    {
        return lines.Count;
    }

    public LineSegment GetLine(int index)
    {
        if (index >= 0 && index < lines.Count)
        {
            return lines[index];
        }
        return null;
    }
    public void AddLine(Vec2 start, Vec2 end, bool specialCol = false, bool twosided = false, bool addcaps = false)
    {
        LineSegment line = new LineSegment(start, end, specialCol ? 0xffff00ff : 0xff00ff00, 4);
        AddChild(line);
        lines.Add(line);
        if (twosided) AddLine(end, start, true, false); // :-)
    }*/

    void Update()
    {
    }
}