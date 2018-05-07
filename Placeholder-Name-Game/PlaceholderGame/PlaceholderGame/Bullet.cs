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
        int range, damage, durability, AOE, speed;
        Texture2D shot, spriteSheet;
        Vector2 pos, direction;
        Rectangle bulletRect, playerRect;
        MouseState mouseState, oldMouseStat;
        PlayerObjects playerO;
        WeaponObjects weaponO;

        public Bullet(String name, int range, int damage, int durability, int AOE, Vector2 direction, Texture2D shot, Texture2D spriteSheet, Vector2 pos, List<Rectangle> wallRectList, int player, int weapon) 
            : base (spriteSheet, pos)  {
            this.range = range;
            this.damage = damage;
            this.durability = durability;
            this.AOE = AOE;
            this.shot = shot;
            this.direction = direction;
            this.pos = pos;
            this.spriteSheet = spriteSheet;
            speed = 100;

            bulletRect = new Rectangle((int)pos.X, (int)pos.Y, 5, 5);
        }

        public override void Update(GameTime gameTime) {
            pos += direction * speed;

            bulletRect.X = (int)pos.X;
            bulletRect.Y = (int)pos.Y;
        }

        public bool HitPlayer(Vector2 playerPos) {
            playerRect = new Rectangle((int)playerPos.X, (int)playerPos.Y, 25, 25);
            if (bulletRect.Intersects(playerRect)) {
                playerO.GetHit(damage);
                return true;
            }
            return false;
        }

        
        public override void Draw(SpriteBatch sb) {
            sb.Draw(shot, bulletRect, Color.White);
        }
    }
}
