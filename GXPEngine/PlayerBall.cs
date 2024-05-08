using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerBall : Ball
{
    Vec2 mouseStart;
    Vec2 mouseEnd;
    Vec2 mouseVel;
    Vec2 mousePos;
    float diff;
    bool pressed = false;
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
        mousePos = new Vec2(Input.mouseX, Input.mouseY);
        diff = (mousePos - position).Length();
        if (Input.GetMouseButtonDown(0) && diff < radius)
        {
            mouseStart = new Vec2(Input.mouseX, Input.mouseY);
            pressed = true;
        }
        if (Input.GetMouseButtonUp(0) && pressed)
        {
            mouseEnd = new Vec2(Input.mouseX, Input.mouseY);
            mouseVel = (mouseEnd - mouseStart);
            velocity += mouseVel / 100;
            pressed = false;
        }
        Slowing();
    }
    void Slowing()
    {
        if (!movingNow)
        {
            velocity.x = velocity.x / 1.1f;
            if (Mathf.Abs(velocity.x) < 0.01f)
            {
                velocity.x = 0;
            }
        }
    }
}
