using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Bullet : Ball
{

    Sprite sprite;
    int bounces = 2;
    float timer = 5000;
    MyGame myGame;
    public Bullet(int pRadius, Vec2 pPosition, float pRotation, Vec2 pVelocity = default, float density = 1, Vec2 pGravity = default, bool moving = true, bool pIsPlayer = false) : base(pRadius, pPosition, pVelocity, density, pGravity, moving, pIsPlayer)
    {
        myGame = (MyGame)game;
        sprite = new Sprite("Assets/bee_tile.png");
        sprite.SetOrigin(width / 2, height / 2);
        AddChild(sprite);
        sprite.rotation = pRotation;
    }
    void Update()
    {
        sprite.rotation = velocity.GetAngleDegrees();

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