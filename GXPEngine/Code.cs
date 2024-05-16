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

public class Code : AnimationSprite
{
    List<char> code;
    public string numb;
    public bool paper;
    TiledObject tiledObject;
    public Vec2 position;
    public bool shown = false;
    float timer = 300f;
    PlayerBall player;
    Sprite papers;
    MyGame mygame;
    EasyDraw coding;
    Font rowdies;
    public Code(string fileName, int cols, int rows, TiledObject obj = null) : base(fileName, cols, rows)
    {
        mygame = (MyGame)game;
        papers = new Sprite("Assets/papers.png");
        rowdies = Utils.LoadFont("Assets/Rowdies-Regular.ttf", 40);
        code = new List<char>();
        tiledObject = obj;

        float width = tiledObject.Width;
        float height = tiledObject.Height;
        position = new Vec2(tiledObject.X, tiledObject.Y);
    }
    void Update()
    {
        if (paper)
        {
            if (!shown)
            {
                papers.x = mygame.width / 2 - papers.width / 2;
                papers.y = mygame.height / 2 - papers.height / 2f;
                mygame.AddChild(papers);
                coding = new EasyDraw(600, 100, false);
                coding.TextFont(rowdies);
                coding.TextAlign(CenterMode.Min, CenterMode.Max);
                coding.Fill(Color.Black);
                coding.SetXY(papers.width/2-coding.width/10,papers.height/2-coding.height/2);
                papers.AddChild(coding);
                shown = true;
            }
            Inputs();
            SetCode();
            if (Input.GetMouseButtonDown(0))
            {
                if (player == null)
                {
                    player = mygame.FindObjectOfType<PlayerBall>();
                }
                papers.Remove();
                code.Clear();
                coding.ClearTransparent();
                shown = false;
                player.position.x = position.x - 10;
            }
        }
    }
    void SetCode()
    {
        coding.Text(numb);
    }
    void Inputs()
    {
        if (Input.GetKeyUp(Key.ZERO))
        {
            code.Add('0');
        }
        if (Input.GetKeyUp(Key.ONE))
        {
            code.Add('1');
        }
        if (Input.GetKeyUp(Key.TWO))
        {
            code.Add('2');
        }
        if (Input.GetKeyUp(Key.THREE))
        {
            code.Add('3');
        }
        if (Input.GetKeyUp(Key.FOUR))
        {
            code.Add('4');
        }
        if (Input.GetKeyUp(Key.FIVE))
        {
            code.Add('5');
        }
        if (Input.GetKeyUp(Key.SIX))
        {
            code.Add('6');
        }
        if (Input.GetKeyUp(Key.SEVEN))
        {
            code.Add('7');
        }
        if (Input.GetKeyUp(Key.EIGHT))
        {
            code.Add('8');
        }
        if (Input.GetKeyUp(Key.NINE))
        {
            code.Add('9');
        }
        Conv();
    }
    void Conv()
    {
        timer--;
        if (timer < 0 || code.Count >3)
        {
            code.Clear();
            timer = 300;
            coding.ClearTransparent();
        }
        numb = new string(code.ToArray());
        if (numb == "371")
        {
            Console.WriteLine("fr");
        }
    }

}
