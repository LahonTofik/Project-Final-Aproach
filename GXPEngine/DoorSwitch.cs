﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class DoorSwitch : AnimationSprite
{
    bool switchIsOpen = false;
    public Vec2 position;
    TiledObject tiledObject;
    public DoorSwitch(TiledObject obj = null) : base("Assets/switch_spritesheet.png", 2, 1)
    {
        tiledObject = obj;
        collider.isTrigger = true;

        float width = tiledObject.Width;
        float height = tiledObject.Height;
        position = new Vec2(tiledObject.X, tiledObject.Y);
    }
    void Update()
    {
        if (switchIsOpen == true)
        {
            currentFrame = 0;
        }
        else
        {
            currentFrame = 1;
        }
    }
    public void SwitchIsOpen(bool _switchIsOpen)
    {
        switchIsOpen = _switchIsOpen;
    }
}
