using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceholderGame {
    class WallObjects : GameObjects {        
        Rectangle sourceRect;


        public WallObjects(Texture2D spriteSheet, Vector2 wallPos) : base(spriteSheet, wallPos) {
            this.spriteSheet = spriteSheet;
            sourceRect = new Rectangle(30, 30, 25, 25);
            wallRect = new Rectangle((int)wallPos.X, (int)wallPos.Y, 25, 25);
        }
        public override void Update(GameTime gameTime) {

        }

        public override void Draw(SpriteBatch sb) {
            sb.Draw(spriteSheet, wallRect, sourceRect, Color.White);
        }
    }
}
