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
        int range, damage, durability, AOE;
        String name;
        Rectangle weaponRect, sourceRect;
        KeyboardState keyState, oldKeyState;

        public WeaponObjects(String name, int range, int damage, int durability, int AOE, Texture2D spriteSheet, Vector2 pos) : base(spriteSheet, pos) {
            this.name = name;
            this.spriteSheet = spriteSheet;
            this.pos = pos;
            this.range = range;
            this.damage = damage;
            this.durability = durability;
            this.AOE = AOE;
            weaponRect = new Rectangle((int)pos.X, (int)pos.Y, 25, 25);
            sourceRect = new Rectangle(5, 144, 50, 50);
        }

        public override void Update(GameTime gameTime) {

        }
        public void Attack(float direction) {
            if (durability>0) {
                durability--;
            }
            
        }

        public bool NewWeapon(Vector2 pos) {
            if (this.pos == pos) {
                return true;
            }
            return false;
        }


        public override void Draw(SpriteBatch sb) {
            sb.Draw(spriteSheet, weaponRect, sourceRect, Color.White);
        }
    }
}
