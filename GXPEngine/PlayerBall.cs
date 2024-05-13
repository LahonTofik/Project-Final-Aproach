using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class PlayerBall : Ball
{
    Vec2 mouseStart;
    Vec2 mouseEnd;
    Vec2 mouseVel;
    Vec2 mousePos;
    bool pressed = false;
    float speed = 3;
    float maxSpeed = 6;
    float jumpPow = 15;
    public PlayerBall(int pRadius, Vec2 pPosition, Vec2 pVelocity = default,float density = 1, Vec2 pGravity = default, bool moving = true, bool pIsPlayer = false) : base(pRadius, pPosition, pVelocity,density, pGravity, moving, pIsPlayer)
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
            mouseVel.y *= 2;
            velocity -= mouseVel / 20;
            pressed = false;
            _velocityIndicator.startPoint = new Vec2(0, 0);
            _velocityIndicator.vector = new Vec2(0, 0);
        }
        if (pressed)
        {
            _velocityIndicator.color = 0xffffffff;
        }
        else
        {
            _velocityIndicator.color = 0;
        }
        ShowDebugInfo();
        Slowing();
    }
    void Slowing()
    {
        velocity.x = velocity.x / 1.01f;
        if (Mathf.Abs(velocity.x) < 0.01f)
        {
            velocity.x = 0;
        }
    }
    void ShowDebugInfo()
    {
        if (drawDebugLine)
        {
            ((MyGame)game).DrawLine(_oldPosition, position);
        }
        _velocityIndicator.startPoint = position;
        _velocityIndicator.vector = (mousePos - position) * -1 / 20;
    }
}
