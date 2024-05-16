using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class PlayerBall : Ball
{
    public bool pressed = false;
    Door door;
    MyGame myGame;
    Peng peng;
    Code code;
    List<CodePiece> piece;
    AddMoves move;
    NextLev lev;
    DoorSwitch doorSwitch;
    LevelTeleport levelTeleport;
    Vec2 mouseStart;
    Vec2 mouseEnd;
    Vec2 mouseVel;
    Vec2 mousePos;
    float dist;
    float speed = 3;
    float maxSpeed = 6;
    float jumpPow = 15;
    bool leverOpen = false;

    private Sound leverSwitched = new Sound("Assets/sfx_pulling_lever.wav");
    private Sound PengoJump = new Sound("Assets/Pengolin_Jump.wav");

    public PlayerBall(int pRadius, Vec2 pPosition, Vec2 pVelocity = default, float density = 1, Vec2 pGravity = default, bool moving = true, bool pIsPlayer = false) : base(pRadius, pPosition, pVelocity, density, pGravity, moving, pIsPlayer)
    {
        myGame = (MyGame)game;
        piece = new List<CodePiece>();
        peng = new Peng("Assets/rolling.png", 3, 2);
        peng.SetOrigin(this.width + 350, this.height + 450);
        piece = myGame.FindObjectsOfType<CodePiece>().ToList();
        AddChild(peng);
        peng.scale = 0.1f;
    }
    void Update()
    {
        MovePlayer();
        if (myGame.currentLevel == 3)
        CheckPaper();
        if(myGame.currentLevel == 2)
        {
            if (move == null)
            {
                move = myGame.FindObjectOfType<AddMoves>();
            }
            if (position.x > move.position.x
            && position.x < (move.position.x + move.width)
            && position.y > (move.position.y - move.height)
            && position.y < move.position.y)
            {
                myGame.moves += 50;
                move.Bye();
            }
            if (lev == null)
            {
                lev = myGame.FindObjectOfType<NextLev>();
            }
            if (position.x > lev.position.x
            && position.x < (lev.position.x + lev.width)
            && position.y > (lev.position.y - lev.height)
            && position.y < lev.position.y)
            {
                myGame.nextLevel = true;
            }
        }
        if (myGame.currentLevel == 1)
        {
            SwitchCollision();
            LevelTeleport();
        }

    }

    void SwitchCollision()
    {
        if (doorSwitch == null)
            doorSwitch = myGame.FindObjectOfType<DoorSwitch>();
        if (position.x > doorSwitch.position.x
            && position.x < (doorSwitch.position.x + doorSwitch.width)
            && position.y > (doorSwitch.position.y - doorSwitch.height)
            && position.y < doorSwitch.position.y)
        {
            leverSwitched.Play();
            leverOpen = true;
            doorSwitch.SwitchIsOpen(leverOpen);
            levelTeleport.DoorIsOpen(leverOpen);
        }
    }

    void LevelTeleport()
    {
        if (levelTeleport == null)
            levelTeleport = myGame.FindObjectOfType<LevelTeleport>();
        if (leverOpen == true && position.x > levelTeleport.position.x
            && position.x < (levelTeleport.position.x + levelTeleport.width)
            && position.y > (levelTeleport.position.y - levelTeleport.height)
            && position.y < levelTeleport.position.y)
        {
            myGame.SetCurrentLevel(myGame.currentLevel++);
        }
    }

    void CheckPaper()
    {
        if (code == null)
            code = myGame.FindObjectOfType<Code>();
        /*dist = (code.position - position).Length();*/
        if (position.x > code.position.x
            && position.x < (code.position.x + code.width)
            && position.y > (code.position.y - code.height)
            && position.y < code.position.y)
        {
            code.paper = true;
            velocity.x = 0;
        }
        else code.paper = false;
        foreach (CodePiece piece in piece)
        {
            if (position.x > piece.position.x - radius * 10
                && position.x < (piece.position.x + code.width + radius * 10)
                && position.y > (piece.position.y - code.height - radius * 10)
                && position.y < piece.position.y + radius * 10)
            {
                piece.paper = true;
            }
            else
            {
                piece.paper = false;
            }
        }
        if (door == null)
        {
            door = myGame.FindObjectOfType<Door>();
        }
        if (position.x > door.position.x
            && position.x < (door.position.x + door.width)
            && position.y > (door.position.y - door.height)
            && position.y < door.position.y)
        {
            position.x   = door.position.x - 50f;
        }
        }
    void MovePlayer()
    {
        mousePos = new Vec2(Input.mouseX, Input.mouseY);
        diff = (mousePos - position).Length();
        if (Input.GetMouseButtonDown(0) && diff < radius && myGame.moves >0)
        {
            mouseStart = new Vec2(Input.mouseX, Input.mouseY);
            pressed = true;

        }
        if (Input.GetMouseButtonUp(0) && pressed)
        {
            PengoJump.Play();
            mouseEnd = new Vec2(Input.mouseX, Input.mouseY);
            mouseVel = (mouseEnd - mouseStart);
            mouseVel.y *= 2;
            velocity -= mouseVel / 20;
            myGame.moves--;
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
        if (velocity.x > 0)
        {
            peng.SetOrigin(this.width + 340, this.height + 427);
            peng.Mirror(false, false);
            peng.SetCycle(1, 5);
            if (peng.currentFrame != 5)
                peng.Animate(0.3f);
            if (peng.currentFrame == 5)
                peng.rotation += velocity.x;
        }
        if (velocity.x < 0)
        {
            peng.SetOrigin(this.width + 220, this.height + 425);
            peng.Mirror(true, false);
            peng.SetCycle(1, 5);
            if (peng.currentFrame != 5)
                peng.Animate(0.3f);
            if (peng.currentFrame == 5)
                peng.rotation += velocity.x;
        }
        if (velocity.x == 0)
        {
            peng.rotation = 0;
            peng.SetCycle(0, 1);
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
