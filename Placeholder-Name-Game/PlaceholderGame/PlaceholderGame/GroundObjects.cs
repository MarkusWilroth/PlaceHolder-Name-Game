﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceholderGame {
    class GroundObjects : GameObjects {
        List<Vector2> groundList, wallList;
        Texture2D ground, TileWall;
        float scale, rotation;
        SpriteEffects fx;
        

        public GroundObjects(Texture2D ground, Vector2 groundPos, Game1 game) : base(ground, groundPos, game) {
            this.ground = ground;
            scale = 0.5f;
            rotation = MathHelper.ToRadians(0);
            fx = SpriteEffects.None;
        }
        public override void Update(GameTime gameTime) {

        }


        public override void Draw(SpriteBatch sb) {
            sb.Draw(ground, pos, Color.White);
        }
    }
}
