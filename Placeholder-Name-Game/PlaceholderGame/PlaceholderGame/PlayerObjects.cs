using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Input;

namespace PlaceholderGame {
    class PlayerObjects : GameObjects {
        Vector2 direction, shotDir, distance, playerPos, destination, mousePos;
        Rectangle playerHitBox, playerDest, sourceRect, sourceWeaponRect, sourceWeapon, nullRect;
        List<Rectangle> wallRectList;
        WeaponObjects[] weaponSlot;
        KeyboardState keyState, oldKeyState;
        MouseState mouseState, oldMouseState;

        int newDestX, newDestY, player, activeWeapon, HP, weapon, count, ammo, damage, range;
        bool hitWall, isMoving, isDone, haveShot;
        float speed, scale, rotation;

        SpriteEffects playerFx;

        public PlayerObjects (Texture2D spriteSheet, Vector2 playerPos, List<Rectangle> wallRectList, int player) : base (spriteSheet, playerPos) {
            this.spriteSheet = spriteSheet;
            this.playerPos = playerPos;
            this.wallRectList = wallRectList;
            this.player = player;
            HP = 20;
            isDone = false;
            haveShot = false;
            activeWeapon = 0;
            
            sourceRect = new Rectangle((30 * player), 3, 25,25);
            weaponSlot = new WeaponObjects[2];
            nullRect = new Rectangle(10000, 10000, 10, 10);

            scale = 1;            
            speed = 100;
            playerFx = SpriteEffects.None;
            playerHitBox = new Rectangle((int)playerPos.X, (int)playerPos.Y, 25, 25);
        }

        public override void Update(GameTime gameTime) {
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            mousePos = new Vector2(mouseState.X, mouseState.Y);
            distance.X = mousePos.X - playerPos.X;
            distance.Y = mousePos.Y - playerPos.Y;

            rotation = (float)Math.Atan2(distance.Y, distance.X) + (float)Math.PI / 2;
            if (!(weaponSlot[activeWeapon] == null)) {
                ammo = weaponSlot[activeWeapon].SendAmmo();
                damage = weaponSlot[activeWeapon].SendDamage();
                range = weaponSlot[activeWeapon].SendRange();
                if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released && haveShot == false && ammo > 0) {
                    shotDir = new Vector2((float)Math.Cos(MathHelper.ToRadians(90) - rotation), -(float)Math.Sin(MathHelper.ToRadians(90) - rotation));
                    weaponSlot[activeWeapon].Attack(shotDir, playerPos, wallRectList, player);
                    count = 5;
                    haveShot = true;
                    isDone = false;
                }
            }
            SwitchWeapon();

            if (!isDone) {   
                if (!isMoving) {
                    if (keyState.IsKeyDown(Keys.A)) {
                        ChangeDirection(new Vector2(-1, 0));

                    } else if (keyState.IsKeyDown(Keys.W)) {
                        ChangeDirection(new Vector2(0, -1));

                    } else if (keyState.IsKeyDown(Keys.D)) {
                        ChangeDirection(new Vector2(1, 0));

                    } else if (keyState.IsKeyDown(Keys.S)) {
                        ChangeDirection(new Vector2(0, 1));
                    }
                } else {
                    playerPos += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (Vector2.Distance(playerPos, destination) < 1) {
                        playerPos = destination;
                        count++;
                        playerHitBox = new Rectangle((int)playerPos.X, (int)playerPos.Y, 25, 25);
                        isMoving = false;
                    }
                }
            }

            oldKeyState = keyState;
            oldMouseState = mouseState;
        }

        #region Give and Send Values
        public int GetHP() {
            return HP;
        }
        public Rectangle GetSendWeapon(int activeWeapon) {
            if (!(weaponSlot[activeWeapon] == null)) {
                sourceWeapon = weaponSlot[activeWeapon].SendSourceRect();
                return sourceWeapon;
            }
            return nullRect;            
        }

        public int GetSendAmmo() {            
            return ammo;
        }

        public int GetSendDamage(int activeWeapon) {
            if (!(weaponSlot[activeWeapon] == null)) {
                damage = weaponSlot[activeWeapon].SendDamage();
                return damage;
            }
            return 0;
        }

        public int GetSendRange(int activeWeapon) {
            if (!(weaponSlot[activeWeapon] == null)) {
                range = weaponSlot[activeWeapon].SendRange();
                return range;
            }
            return 0;
        }

        public bool GetHit(int damage) {
            HP -= damage;
            if (HP <= 0) {
                return true;
            }
            return false;
        }

        public Rectangle GetRect() {
            return playerHitBox;
        }

        public Vector2 SendPos() {
            return playerPos;
        }
        #endregion

        public bool Counter() {
            if (count >= 10) {
                isDone = true;
                if (keyState.IsKeyDown(Keys.Enter)) {
                    isDone = false;
                    count = 0;
                    haveShot = false;
                    return true;
                }return false;                
            }return false;
        }

        private void SwitchWeapon() {
            if (keyState.IsKeyDown(Keys.F1)) {
                activeWeapon = 0;
                
            }
            if (keyState.IsKeyDown(Keys.F2)) {
                activeWeapon = 1;
            }

            if (!(weaponSlot[activeWeapon] == null)) {
                weapon = weaponSlot[activeWeapon].ReturnWeapon();
                sourceWeaponRect = new Rectangle(30 * weapon, 90, 25, 25);
            }
        }

        private void ChangeDirection(Vector2 direction) {
            this.direction = direction;
            Vector2 newDestination = playerPos + (this.direction * 25);
            
            newDestX = (int)newDestination.X;
            newDestY = (int)newDestination.Y;
            playerDest = new Rectangle(newDestX, newDestY, 25, 25);

            foreach (Rectangle wallRect in wallRectList) {
                if (wallRect.Intersects(playerDest)) {
                    hitWall = true;
                    break;
                } else {
                    hitWall = false;
                }
            }

            if (!hitWall) {
                destination = newDestination;
                isMoving = true;
            }
            if (hitWall) {
                isMoving = false;
            }
        }        

        public void EquipedWeapon(WeaponObjects weaponStats) {
            weaponSlot[activeWeapon] = weaponStats;
            
        }

        public override void Draw(SpriteBatch sb) {
            sb.Draw(spriteSheet, new Vector2(playerPos.X + 12, playerPos.Y + 12), sourceRect, Color.White, rotation, new Vector2(12.5f, 12.5f), scale, playerFx, 1);
            sb.Draw(spriteSheet, new Vector2(playerPos.X + 12, playerPos.Y + 12), sourceWeaponRect, Color.White, rotation + 3.1415f, new Vector2(12.5f, 12.5f), scale, playerFx, 1);
        }
    }
}
