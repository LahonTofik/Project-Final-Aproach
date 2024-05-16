using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
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
    float timer= 300f;
    Sprite papers;
    MyGame mygame;
    public Code(string fileName, int cols, int rows, TiledObject obj = null) : base(fileName, cols, rows)
    {
        mygame = (MyGame)game;
        papers = new Sprite("Assets/papers.png");
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
                shown = true;
            }
            Inputs();
        }
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
        if(timer < 0) {
            code.Clear();
            timer = 300;
        }
        numb = new string(code.ToArray());
        if (numb == "371")
        {
            Console.WriteLine("fr");
        }
    }

}
