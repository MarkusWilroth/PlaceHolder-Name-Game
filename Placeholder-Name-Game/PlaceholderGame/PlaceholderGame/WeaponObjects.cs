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
        Vector2 shotPos, direction;
        String name;
        Rectangle weaponRect, sourceRect, shotRect;
        List<Vector2> shotsList;
        Texture2D shot;


        public WeaponObjects(String name, int range, int damage, int durability, int AOE, Texture2D spriteSheet, Vector2 pos, Texture2D shot) : base(spriteSheet, pos) {
            this.name = name;
            this.shot = shot;
            this.spriteSheet = spriteSheet;
            this.pos = pos;
            this.range = range;
            this.damage = damage;
            this.durability = durability; 
            this.AOE = AOE; 
            weaponRect = new Rectangle((int)pos.X, (int)pos.Y, 25, 25);
            sourceRect = new Rectangle(0, 65, 25, 25);
        }

        public override void Update(GameTime gameTime) {
            shotPos += direction;
            shotRect = new Rectangle((int)shotPos.X, (int)shotPos.Y, 5, 5);
        }

        public void Attack(Vector2 direction, Vector2 shotPos) {
            if (durability > 0) { //Fixa i hudden så att man kan se hur många skott det finns kvar
                durability--;
                this.shotPos = shotPos;
                this.direction = direction;
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
            sb.Draw(shot, shotRect, Color.White);            
        }
    }
}
