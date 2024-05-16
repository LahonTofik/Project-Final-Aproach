using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class CodePiece : AnimationSprite
{
    List<char> code;
    public string numb;
    public bool paper;
    TiledObject tiledObject;
    public Vec2 position;
    public bool showing = false;
    float timer = 300f;
    string piece;
    PlayerBall player;
    Sprite papers;
    MyGame mygame;
    EasyDraw coding;
    Font rowdies;
    public CodePiece(string fileName, int cols, int rows, TiledObject obj = null) : base(fileName, cols, rows)
    {
        mygame = (MyGame)game;
        papers = new Sprite("Assets/papers.png");
        rowdies = Utils.LoadFont("Assets/Rowdies-Regular.ttf", 40);
        code = new List<char>();
        tiledObject = obj;
        if (obj != null)
        {
            piece = obj.GetStringProperty("codePiece");
        }

        float width = tiledObject.Width;
        float height = tiledObject.Height;
        position = new Vec2(tiledObject.X, tiledObject.Y);
    }
    void Update()
    {
        if (paper)
        {
            if (!showing)
            {
                coding = new EasyDraw(600, 100, false);
                coding.TextFont(rowdies);
                coding.TextAlign(CenterMode.Min, CenterMode.Max);
                coding.Fill(Color.Black);
                coding.SetXY(width / 2 - coding.width / 10, height/2 - coding.height);
                coding.Text(piece);
                AddChild(coding);
                showing = true;
            }
        }
        else if(!paper && showing)
        {
            coding.ClearTransparent();
            showing = false;
        }
    }
}
