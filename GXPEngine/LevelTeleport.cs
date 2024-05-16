using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class LevelTeleport : AnimationSprite
{
    bool doorIsOpen = false;
    TiledObject tiledObject;
    public Vec2 position;
    public LevelTeleport(TiledObject obj = null) : base("Assets/Lock_spritesheet.png",2 ,1)
    {
        tiledObject = obj;
        collider.isTrigger = true;

        float width = tiledObject.Width;
        float height = tiledObject.Height;
        position = new Vec2(tiledObject.X, tiledObject.Y);
    }

    void Update()
    {
        if (doorIsOpen == true)
        {
            currentFrame = 1;
        }
        else
        {
            currentFrame = 0;
        }
    }
    public void DoorIsOpen(bool _doorIsOpen)
    {
        doorIsOpen = _doorIsOpen;
    }
}

