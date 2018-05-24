using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlaceholderGame {
    class WeaponObjects : GameObjects {
        int range, damage, ammo, AOE, weapon, speed;
        public bool isFired;

        Vector2 shotPos, direction;
        String name;
        Rectangle weaponRect, sourceRect, bulletRect;
        Texture2D shot;
        Game1 game;
        List<Bullet> bulletList;        

        public WeaponObjects(String name, int ammo, int range, int damage, int AOE, Texture2D spriteSheet, Vector2 pos, Texture2D shot, int weapon, Game1 game) : base(spriteSheet, pos) {
            this.name = name;
            this.range = range;
            this.damage = damage;
            this.AOE = AOE;
            this.ammo = ammo;
            this.spriteSheet = spriteSheet;
            this.pos = pos;
            this.weapon = weapon;
            this.shot = shot;
            this.game = game;
            speed = 100;
            isFired = false;
            bulletList = new List<Bullet>();

            weaponRect = new Rectangle((int)pos.X, (int)pos.Y, 25, 25);
            sourceRect = new Rectangle(30 * weapon, 65, 25, 25);
        }

        #region Give Values

        public int SendAmmo() {
            return ammo;
        }

        public int SendDamage() {
            return damage;
        }
        public int SendRange() {
            return range;
        }

        public Rectangle SendSourceRect() {
            return sourceRect;
        }

        public int ReturnWeapon() {
            return weapon;
        }

        public List<Bullet> GetBulletList() {
            return bulletList;
        }

        #endregion

        public override void Update(GameTime gameTime) {
            pos += direction * speed;
            bulletRect.X = (int)pos.X;
            bulletRect.Y = (int)pos.Y;
        }

        public void Attack(Vector2 direction, Vector2 shotPos, List<Rectangle> wallRectList, int player) {
            if (ammo > 0) {
                ammo--;
                bulletRect = new Rectangle((int)shotPos.X, (int)shotPos.Y, 5, 5);
                this.shotPos = shotPos;
                this.direction = direction;

                game.CreateBullet(name, range, damage, ammo, AOE, direction, shot, spriteSheet, shotPos, wallRectList, player, weapon);
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
            sb.Draw(shot, bulletRect, Color.White);
        }
    }
}
