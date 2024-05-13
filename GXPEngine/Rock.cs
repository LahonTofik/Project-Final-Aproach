using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rock : Ball
{
    int bounces = 2;
    float timer = 5000;
    MyGame myGame;
    public Rock(int pRadius, Vec2 pPosition, float pRotation, Vec2 pVelocity = default, float density = 1, Vec2 pGravity = default, bool moving = true, bool pIsPlayer = false) : base(pRadius, pPosition, pVelocity,density, pGravity, moving, pIsPlayer)
    {
        myGame = (MyGame)game;
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 || bounces <= 0)
        {
            myGame.RemoveMover(this);
            this.Destroy();
        }
    }
    public override void Bounce()
    {
        bounces--;
    }
}
