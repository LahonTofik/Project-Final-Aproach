using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using GXPEngine;

public class Turret : Sprite
{
    Vec2 position;
    int bounces;
    Vec2 direction;
    float targetAngle;
    List<Ball> bullets;
    Vec2 bulletPos;
    Vec2 velocity;
    MyGame myGame;
    float timer = 2500;
    public Turret(Vec2 pPosition) : base("Assets/beehive_tile.png")
    {
        SetOrigin(width / 3, height / 2);
        position = pPosition;
        UpdateScreenPosition();
        bullets = new List<Ball>();
        myGame = (MyGame)game;

    }

    public void Update()
    {
        timer -= Time.deltaTime;
        for (int i = 0; i < myGame.GetNumberOfMovers(); i++)
        {
            Ball mover = myGame.GetMover(i);
            if (mover.isPlayer)
            {
                direction = mover.position - position;
                direction.Normalize();
                targetAngle = direction.GetAngleDegrees();
            }
        }
        if (timer <= 0)
        {
            velocity = new Vec2(2, 0);
            velocity.RotateDegrees(targetAngle);
            myGame.AddMover(new Bullet(3, position + velocity.Normalized(), targetAngle, velocity, 1, new Vec2(0, 0.5f)));
            Console.WriteLine("ball made");
            timer = 2500;
        }
        UpdateScreenPosition();
    }
    void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
    }
}
