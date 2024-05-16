using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Bullet : Ball
{

    Sprite sprite;
    int radius;
    PlayerBall player;
    Vec2 distance;
    Vec2 mousePos;
    public float _speed = 3;
    float _acel = 0.1f;
    int bounces = 1;
    float timer = 5000;
    MyGame myGame;
    public Bullet(int pRadius, Vec2 pPosition, float pRotation, Vec2 pVelocity = default, float density = 1, Vec2 pGravity = default, bool moving = true, bool pIsPlayer = false) : base(pRadius, pPosition, pVelocity, density, pGravity, moving, pIsPlayer)
    {
        radius = pRadius;
        myGame = (MyGame)game;
        sprite = new Sprite("Assets/bee_tile.png");
        sprite.SetOrigin(width / 2, height / 2);
        AddChild(sprite);
        sprite.rotation = pRotation;
    }
    void Update()
    {
        if (player == null)
        {
            player = game.FindObjectOfType<PlayerBall>();
        }

        distance = (player.position - position);
        if (distance.Length() > radius)
        {
            velocity = velocity * 0.95f + distance.Normalized() * 0.05f;
            _speed += _acel;
        }
        timer -= Time.deltaTime;
        if (timer <= 0 || bounces <= 0)
        {
            myGame.RemoveMover(this);
            sprite.Remove();
            this.Remove();
        }
        sprite.rotation = velocity.GetAngleDegrees();
    }

    public override void Bounce()
    {
        bounces--;
    }
}