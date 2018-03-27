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
        Rectangle wallRect;
        float scale, rotation;
        SpriteEffects fx;


        public WallObjects(Texture2D TileWall, Vector2 wallPos) : base(TileWall, wallPos) {
            this.TileWall = TileWall;
            this.wallPos = wallPos;
            wallRect = new Rectangle((int)wallPos.X, (int)wallPos.Y, 25, 25);
            scale = 0.5f;
            rotation = MathHelper.ToRadians(0);

            fx = SpriteEffects.None;
        }
        public override void Update(GameTime gameTime) {

        }


        public override void Draw(SpriteBatch sb) {
            sb.Draw(TileWall, wallRect, Color.White);
        }
    }
}
