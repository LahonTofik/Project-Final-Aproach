using System;
using GXPEngine;	// For Mathf

public struct Vec2
{
    public float x;
    public float y;

    public Vec2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }

    public override string ToString()
    {
        return String.Format("({0},{1})", x, y);
    }

    public void SetXY(float pX, float pY)
    {
        x = pX;
        y = pY;
    }

    public float Length()
    {
        return Mathf.Sqrt(x * x + y * y);
    }

    public void Normalize()
    {
        float len = Length();
        if (len > 0)
        {
            x /= len;
            y /= len;
        }
    }

    public Vec2 Normalized()
    {
        Vec2 result = new Vec2(x, y);
        result.Normalize();
        return result;
    }
    public static float Deg2Rad(float deg)
    {
        return deg * Mathf.PI / 180;
    }
    public static float Rad2Deg(float rad)
    {
        return rad * 180 / Mathf.PI;
    }
    public static Vec2 GetUnitVectorRad(float rad)
    {
        return new Vec2(Mathf.Cos(rad), Mathf.Sin(rad));
    }
    public static Vec2 GetUnitVectorDeg(float deg)
    {
        return GetUnitVectorRad(Deg2Rad(deg));
    }
    public static Vec2 RandomUnitVector()
    {
        float randomAngleRad = Utils.Random(0, 2 * Mathf.PI);
        return GetUnitVectorRad(randomAngleRad);
    }
    public void SetAngleRadius(float rad)
    {
        Vec2 rotatedVec = GetUnitVectorRad(rad) * Length();
        x = rotatedVec.x;
        y = rotatedVec.y;
    }
    public void SetAngleDegrees(float deg)
    {
        SetAngleRadius(Deg2Rad(deg));
    }
    public float GetAngleRadians()
    {
        return Mathf.Atan2(y, x);
    }
    public float GetAngleDegrees()
    {
        return Rad2Deg(GetAngleRadians());
    }
    public void RotateRadians(float rad)
    {
        float cos = GetUnitVectorRad(rad).x;
        float sin = GetUnitVectorRad(rad).y;
        Vec2 rotatedVec = new Vec2((cos * x - sin * y), (sin * x + cos * this.y));
        x = rotatedVec.x;
        y = rotatedVec.y;
    }
    public void RotateDegrees(float deg)
    {
        RotateRadians(Deg2Rad(deg));
    }
    public void RotateAroundRadians(float rad, Vec2 p)
    {
        x -= p.x;
        y -= p.y;
        RotateRadians(rad);
        x += p.x;
        y += p.y;
    }
    public void RotateAroundDegrees(float deg, Vec2 p)
    {
        RotateAroundRadians(Deg2Rad(deg), p);
    }

    public float Dot(Vec2 other)
    {
        // TODO: insert dot product here
        return x * other.x + y * other.y;
    }

    public Vec2 Normal()
    {
        // TODO: return a unit normal
        return new Vec2(-y, x).Normalized();
    }
    public void Reflect(Vec2 pNormal, float pBounciness = 1)
    {
        Vec2 vel = new Vec2(x, y);
        vel = vel - (1 + pBounciness) * (vel.Dot(pNormal)) * pNormal;
        x = vel.x;
        y = vel.y;
    }

    public static Vec2 operator +(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x + right.x, left.y + right.y);
    }

    public static Vec2 operator -(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x - right.x, left.y - right.y);
    }
    public static Vec2 operator *(Vec2 v, float scalar)
    {
        return new Vec2(v.x * scalar, v.y * scalar);
    }

    public static Vec2 operator *(float scalar, Vec2 v)
    {
        return new Vec2(v.x * scalar, v.y * scalar);
    }

    public static Vec2 operator /(Vec2 v, float scalar)
    {
        return new Vec2(v.x / scalar, v.y / scalar);
    }
}



