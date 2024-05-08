using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;


public class CollisionLine : AnimationSprite
{
    public CollisionLine(string fileName, int cols, int rows, TiledObject obj = null) : base(fileName, cols, rows)
    {
        alpha = 0.1f;
    }
}
