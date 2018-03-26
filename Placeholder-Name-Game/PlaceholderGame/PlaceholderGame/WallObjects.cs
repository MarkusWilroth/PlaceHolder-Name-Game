using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceholderGame {
    class WallObjects : GameObjects {
        Texture2D TileWall;
        Vector2 wallPos;
        float scale, rotation;
        SpriteEffects fx;


        public WallObjects(Texture2D TileWall, Vector2 wallPos) : base(TileWall, wallPos) {
            this.TileWall = TileWall;
            this.wallPos = wallPos;
            scale = 0.5f;
            rotation = MathHelper.ToRadians(0);

            fx = SpriteEffects.None;
        }
        public override void Update(GameTime gameTime) {

        }


        public override void Draw(SpriteBatch sb) {
            sb.Draw(TileWall, wallPos, null, Color.White, rotation, new Vector2(0, 0), scale, fx, 1);
        }
    }
}
