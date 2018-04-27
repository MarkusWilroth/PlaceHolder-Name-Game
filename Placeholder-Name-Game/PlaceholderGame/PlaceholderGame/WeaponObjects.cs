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
        int range, damage, durability, AOE, weapon;
        Vector2 shotPos, direction;
        String name;
        Rectangle weaponRect, sourceRect, shotRect;
        List<Vector2> shotsList;
        Texture2D shot;
        Bullet bullet;
        List<Bullet> bulletList;



        public WeaponObjects(String name, int durability, int range, int damage, int AOE, Texture2D spriteSheet, Vector2 pos, Texture2D shot, int weapon) : base(spriteSheet, pos) {
            this.name = name;
            this.range = range;
            this.damage = damage;
            this.AOE = AOE;
            this.durability = durability;
            this.spriteSheet = spriteSheet;
            this.pos = pos;
            this.weapon = weapon;
            bulletList = new List<Bullet>();
            
            weaponRect = new Rectangle((int)pos.X, (int)pos.Y, 25, 25);
            sourceRect = new Rectangle(30 * weapon, 65, 25, 25);
        }

        public override void Update(GameTime gameTime) {
                //bullet.Update(gameTime);
            
        }

        public void Attack(Vector2 direction, Vector2 shotPos, List<Rectangle> wallRectList, int player) {
            if (durability > 0) { //Fixa i hudden så att man kan se hur många skott det finns kvar
                durability--;
                this.shotPos = shotPos;
                this.direction = direction;
                Console.WriteLine("durability: " + durability);
                bullet = new Bullet(name, range, damage, durability, AOE, direction, shot, spriteSheet, pos, wallRectList, player, weapon); //allt som är rött + direction
                bulletList.Add(bullet);
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
