﻿using GameCore;
using GameCore.Collision;
using System;

namespace PaperWork.GameEntities
{
    public class SolidBlock : Entity
    {
        public SolidBlock() : base(50, 50)
        {

            Textures.Add(new EntityTexture("block", 50, 50));
            Colliders.Add(new Collider(this, 48, 48, 1, 1));
        }
    }
}
