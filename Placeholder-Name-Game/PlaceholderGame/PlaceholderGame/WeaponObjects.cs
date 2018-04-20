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
        Bullet bullet;



        public WeaponObjects(String name, int durability, int range, int damage, int AOE, Texture2D spriteSheet, Vector2 pos, Texture2D shot, int weapon) : base(spriteSheet, pos) {
            this.name = name;
            this.range = range;
            this.damage = damage;
            this.AOE = AOE;
            this.durability = durability;
            this.spriteSheet = spriteSheet;
            this.pos = pos;
            
            weaponRect = new Rectangle((int)pos.X, (int)pos.Y, 25, 25);
            sourceRect = new Rectangle(30 * weapon, 65, 25, 25);
        }

        public override void Update(GameTime gameTime) {
            shotPos += direction * 25;
            shotRect = new Rectangle((int)shotPos.X, (int)shotPos.Y, 5, 5);
        }

        public void Attack(Vector2 direction, Vector2 shotPos, List<Rectangle> wallRectList, int player) {
            if (durability > 0) { //Fixa i hudden så att man kan se hur många skott det finns kvar
                durability--;
                this.shotPos = shotPos;
                this.direction = direction;
                Console.WriteLine("durability: " + durability);
                bullet = new Bullet(range, damage, durability, AOE, direction, shot, pos, spriteSheet, shotPos, wallRectList, player); //allt som är rött + direction
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
