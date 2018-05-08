using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceholderGame {
    class Bullet: GameObjects{
        int range, damage, durability, AOE, speed, timer, shotTime, weapon;
        bool isBulletDead;
        Texture2D spriteSheet;
        Vector2 pos, direction;
        Rectangle bulletRect, playerRect, sourceRect;
        MouseState mouseState, oldMouseStat;
        PlayerObjects playerO;
        WeaponObjects weaponO;
        Game1 game;
        List<Rectangle> wallRectList;


        public Bullet(String name, int range, int damage, int durability, int AOE, Vector2 direction, Texture2D shot, Texture2D spriteSheet, Vector2 pos, List<Rectangle> wallRectList, int player, int weapon, Game1 game) 
            : base (spriteSheet, pos)  {
            this.range = range;
            this.damage = damage;
            this.durability = durability;
            this.AOE = AOE;
            this.weapon = weapon;
            this.direction = direction;
            this.pos = pos;
            this.spriteSheet = spriteSheet;
            this.game = game;
            this.wallRectList = wallRectList;
            isBulletDead = false;
            shotTime = 180;
            speed = 1;
            timer = 0;

            bulletRect = new Rectangle((int)pos.X, (int)pos.Y, 5, 5);
        }

        public override void Update(GameTime gameTime) {
            isBulletDead = game.HitPlayer(bulletRect, damage);
            if (timer <= range * shotTime) {
                pos += direction * 0.2f;
                timer++;
            }
            else {
                isBulletDead = true;
            }

            bulletRect.X = (int)pos.X;
            bulletRect.Y = (int)pos.Y;
            sourceRect = new Rectangle(30 * weapon, 120, 25, 25);

            foreach (Rectangle wallRect in wallRectList) {
                if (bulletRect.Intersects(wallRect)) {
                    isBulletDead = true;
                }
            }
            
        }
        public bool KillBullet() {
            return isBulletDead;
        }
        
        public override void Draw(SpriteBatch sb) {
            sb.Draw(spriteSheet, bulletRect, sourceRect, Color.White);
        }
    }
}
