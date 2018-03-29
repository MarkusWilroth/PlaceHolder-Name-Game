using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlaceholderGame {
    class WeaponObjects : GameObjects {
        int Range, Damage, Durability, AOE;
        String name;
        Rectangle weaponRect, sourceRect;
        KeyboardState keyState, oldKeyState;

        public WeaponObjects(String name, int Range, int Damage, int Durability, int AOE, Texture2D spriteSheet, Vector2 pos, Rectangle sourceRect) : base(spriteSheet, pos) {
            this.name = name;
            this.spriteSheet = spriteSheet;
            this.pos = pos;
            this.Range = Range;
            this.Damage = Damage;
            this.Durability = Durability;
            this.AOE = AOE;
            weaponRect = new Rectangle((int)pos.X, (int)pos.Y, 25, 25);
            sourceRect = new Rectangle();
        }

        public override void Update(GameTime gameTime) {

        }

        public override void Draw(SpriteBatch sb) {
            sb.Draw(spriteSheet, weaponRect, sourceRect, Color.White);
        }
    }
}
