using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceholderGame {
    class GroundObjects : GameObjects {
        Texture2D spireSheet;
        Rectangle sourceRect, groundRect;        

        public GroundObjects(Texture2D spriteSheet, Vector2 groundPos) : base(spriteSheet, groundPos) {
            this.spireSheet = spriteSheet;
            sourceRect = new Rectangle(150, 30, 20, 20);
            groundRect = new Rectangle((int)groundPos.X, (int)groundPos.Y, 25, 25);
        }
        public override void Update(GameTime gameTime) {

        }

        public override void Draw(SpriteBatch sb) {
            //sb.Draw(spireSheet, groundRect, sourceRect, Color.White);
        }
    }
}
