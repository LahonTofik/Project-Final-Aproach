using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GXPEngine;

public class HUD : GameObject
{
    EasyDraw movesLeft;
    Font rowdies;
    public HUD()
    {
        rowdies = Utils.LoadFont("Assets/Rowdies-Regular.ttf", 40);
        movesLeft = new EasyDraw(300, 60, false);
        movesLeft.TextFont(rowdies);
        movesLeft.TextAlign(CenterMode.Min, CenterMode.Center);
        movesLeft.Fill(0, 0, 0);
        movesLeft.Text("Moves: 35");
        movesLeft.SetXY(10, 10);
        AddChild(movesLeft);


    }
    public void SetMoves(int moveCount)
    {
        movesLeft.Text(String.Format("Moves: " + moveCount), true);
    }
    void Update()
    {
    }
}

