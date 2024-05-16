using System;
using System.Drawing.Imaging;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using GXPEngine;
using GXPEngine.Core;

public class Ball : EasyDraw
{
    Bullet bullet;
    Cube cube;
    public static bool drawDebugLine = false;
    public static float bounciness = 0.4f;
    public static Vec2 acceleration = new Vec2(0, 0);
    public Arrow _velocityIndicator;
    public float Mass
    {
        get
        {
            return 4 * radius * radius * _density;
        }
    }

    public Vec2 velocity;
    public Vec2 position;
    public Vec2 _oldPosition;

    public readonly int radius;
    public readonly bool moving;

    Vec2 gravity;
    float _density = 1;
    public float diff;

    public bool movingNow = false;
    public bool isPlayer = false;

    public float jump = 1;
    MyGame mygame;

    bool firstTime = true;

    public Ball(int pRadius, Vec2 pPosition, Vec2 pVelocity = new Vec2(),float density = 1, Vec2 pGravity = new Vec2(), bool moving = true, bool pIsPlayer = false) : base(pRadius * 2 + 1, pRadius * 2 + 1)
    {
        _density = density;
        radius = pRadius;
        gravity = pGravity;
        position = pPosition;
        velocity = pVelocity;
        this.moving = moving;
        isPlayer = pIsPlayer;
        mygame = (MyGame)game;
        position = pPosition;
        UpdateScreenPosition();
        SetOrigin(radius, radius);

        Draw(230, 200, 0);
        _velocityIndicator = new Arrow(position, new Vec2(0, 0), 10);
        AddChild(_velocityIndicator);
        Cube[] cube = FindObjectsOfType<Cube>();
    }

    void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        Ellipse(radius, radius, 2 * radius, 2 * radius);
    }

    void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
    }

    public void Step()
    {
        if (this is Bullet)
        {
            bullet = FindObjectOfType<Bullet>();
            position += velocity * bullet._speed;
            _oldPosition = position;
        }
        else
        {
            velocity += acceleration;
            _oldPosition = position;
            position += velocity;
        }

        CollisionInfo firstCollision = FindEarliestCollision();
        if (firstCollision != null)
        {
            ResolveCollision(firstCollision);
            if (Mathf.Abs(firstCollision.timeOfImpact) < 0.001f && firstTime == true)
            {
                firstTime = false;
                position += velocity;

                firstCollision = FindEarliestCollision();
                if (firstCollision != null)
                {
                    ResolveCollision(firstCollision);
                }
            }
        }

        UpdateScreenPosition();
    }
    float TimeOfImpact(Ball p, Ball q)
    {
        Vec2 u = p._oldPosition - q.position;
        float A = Mathf.Pow(p.velocity.Length(), 2);
        float B = u.Dot(p.velocity) * 2;
        float C = Mathf.Pow(u.Length(), 2) - Mathf.Pow(p.radius + q.radius, 2);

        if (C < 0)
        {
            if (B < 0)
            {
                return 0f;
            }
            else
            {
                return 10f;
            }

        }
        if (Mathf.Abs(A) < 0.000001f)
        {
            return 10f;
        }
        float D = (B * B) - (4 * A * C);
        if (D < 0)
        {
            return 10f;
        }
        float TOI = (-B - Mathf.Sqrt(D)) / (2 * A);
        if (0 <= TOI && TOI <= 1)
        {
            return TOI;
        }
        return 10f;
    }

    Vec2 PointOfImpact(float t)
    {
        return _oldPosition + velocity * t;
    }

    CollisionInfo FindEarliestCollision()
    {
        MyGame myGame = (MyGame)game;
        // Check other movers:		
        CollisionInfo earliest = null;
        firstTime = true;
        for (int i = 0; i < myGame.GetNumberOfMovers(); i++)
        {
            Ball mover = myGame.GetMover(i);
            if (mover != this)
            {
                float TOI = TimeOfImpact(this, mover);
                if (earliest == null || TOI < earliest.timeOfImpact)
                {
                    Vec2 relativePosition = PointOfImpact(TOI) - mover.position;
                    if (0 <= TOI && TOI < 1)
                    {
                        earliest = new CollisionInfo(relativePosition, mover, TOI); 
                    }
                }
            }
        }
        // TODO: Check Line segments using myGame.GetLine();
        for (int i = 0; i < myGame.GetNumberOfLines(); i++)
        {
            LineSegment _lineSegment = myGame.GetLine(i);
            if (Input.GetKey(Key.LEFT))
            {
               // Console.WriteLine("Checking line {0}", i);
                if (i == 0)
                {
                    Console.WriteLine("x of the line:" + _lineSegment.x.ToString());
                    Console.WriteLine("start x of the line:"+_lineSegment.start.x);
                    Console.WriteLine("end x of the line:"+ _lineSegment.end.x);
                }
            }
            Vec2 lineVector = _lineSegment.end - _lineSegment.start;
            Vec2 difference = _oldPosition - _lineSegment.start;
            Vec2 lineNormal = lineVector.Normal();
            float ballDistance = difference.Dot(lineNormal);

            float a = ballDistance - radius;
            float b = -velocity.Dot(lineNormal);
            float t;
            if (Input.GetKey(Key.LEFT_SHIFT))
            {
                Console.WriteLine("a={0} b={1} TOI={2}", a, b, a / b);
            }
            if (b <= 0)
            {
                continue; // go to next iteration of loop!
            }
            if (a >= 0)
            {
                t = a / b;
            }
            else if (a >= -radius)
            {
                t = 0;
            }
            else continue;

            if (t <= 1)
            {
                Vec2 diff = PointOfImpact(t) - _lineSegment.start;
                float distance = diff.Dot(lineVector.Normalized());
                if (0 <= distance && distance <= lineVector.Length())
                {
                    if (Input.GetKey(Key.LEFT_SHIFT))
                    {
                        Console.WriteLine("Found a collision");
                    }
                    if (earliest == null || t < earliest.timeOfImpact)
                    {
                        earliest = new CollisionInfo(lineNormal, _lineSegment, t); // Is this truly the *earliest*??
                    }
                }
            }
        }
        return earliest;

    }

    void ResolveCollision(CollisionInfo col)
    {
        if (col.other is Ball)
        {
            Ball otherBall = (Ball)col.other;
            if (col.other is Rock && this is PlayerBall)
            {
                position = PointOfImpact(col.timeOfImpact);
                Vec2 COM = (Mass * velocity + otherBall.Mass * otherBall.velocity) / (Mass + otherBall.Mass);
                velocity.Reflect(col.normal.Normalized(), COM);
            }
            else
            {
                position = PointOfImpact(col.timeOfImpact);
                velocity.Reflect(col.normal.Normalized(), new Vec2(0, 0), 0.01f);

            }
        }
        if (col.other is LineSegment)
        {
            LineSegment line = (LineSegment)col.other;
            position = PointOfImpact(col.timeOfImpact);
            velocity.Reflect(col.normal.Normalized(), new Vec2(0, 0), 0.5f);
            if (this is PlayerBall)
            {
                if (Input.GetMouseButtonDown(0) && diff < radius * 5)
                {
                    velocity.x = 0;
                }
            }
        }
        Bounce();
    }
    public virtual void Bounce()
    {

    }
    
}

