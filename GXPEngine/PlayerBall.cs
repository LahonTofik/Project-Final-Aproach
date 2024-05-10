using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerBall : Ball
{
    float speed = 3;
    float maxSpeed = 6;
    float jumpPow = 15;
    public PlayerBall(int pRadius, Vec2 pPosition, Vec2 pVelocity = default, Vec2 pGravity = default, bool moving = true, bool pIsPlayer = false) : base(pRadius, pPosition, pVelocity, pGravity, moving, pIsPlayer)
    {
    }
    void Update()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        if (Input.GetKey(Key.A))
        {
            if (velocity.x >= -maxSpeed)
            {
                velocity.x -= speed;
            }
            if (velocity.x <= -maxSpeed)
            {
                velocity.x = -maxSpeed;
            }
            movingNow = true;
        }
        else if (Input.GetKey(Key.D)) ;
        else
        {
            movingNow = false;
        }
        if (Input.GetKey(Key.D))
        {
            if (velocity.x <= maxSpeed)
            {
                velocity.x += speed;
            }
            if (velocity.x >= maxSpeed)
            {
                velocity.x = maxSpeed;
            }
            movingNow = true;
        }
        else if (Input.GetKey(Key.A)) ;
        else movingNow = false;
        if (Input.GetKeyDown(Key.SPACE) && jump > 0)
        {
            velocity.y -= jumpPow;
            jump -= 1;
        }
    }
}
