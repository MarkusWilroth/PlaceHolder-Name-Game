﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace PlaceholderGame {
    abstract class GameObjects {
        protected Vector2 pos;
        protected Texture2D spriteSheet;
        protected List<Vector2> posList, wallList;        
        protected String[] printLevel;

        protected char textLetter;
        protected int groundX, groundY;
        bool hitWall;

        protected Rectangle wallRect;
        GroundObjects groundO;
        PlayerObjects[] playerO;
        WeaponObjects weaponO;


        public GameObjects(Texture2D spriteSheet, Vector2 pos) {

        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch sb);

    }
}
