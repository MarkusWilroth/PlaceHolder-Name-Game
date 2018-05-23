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
        Rectangle sourceBloodRect, groundRect;
        bool isBlood;        

        public GroundObjects(Texture2D spriteSheet, Vector2 groundPos) : base(spriteSheet, groundPos) {
            this.spireSheet = spriteSheet;
            sourceBloodRect = new Rectangle(150, 30, 20, 20);
            groundRect = new Rectangle((int)groundPos.X, (int)groundPos.Y, 25, 25);
        }
        public override void Update(GameTime gameTime) {

        }

        public void GetBlood(Rectangle playerRectPos) {
            if(playerRectPos.Intersects(groundRect)) {
                isBlood = true;
            }
        }

        public override void Draw(SpriteBatch sb) {
            if(isBlood) {
                sb.Draw(spireSheet, groundRect, sourceBloodRect, Color.White);
            }
            
        }
    }
}
