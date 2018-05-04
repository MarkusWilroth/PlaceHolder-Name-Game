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
        Vector2 direction, playerPos, destination, dir, mousePos;
        Rectangle playerRect, playerHitBox, playerDest, sourceRect;
        List<Rectangle> wallRectList;
        WeaponObjects[] weaponSlot;
        Bullet bullet;
        Hud hud;

        int newDestX, newDestY, player, activeWeapon, equipedWeapons, HP;
        bool hitWall, isMoving;
        float speed, scale, rotation;

        SpriteEffects playerFx;
        KeyboardState keyState, oldKeyState;
        MouseState mouseState, oldMouseState;
        

        public PlayerObjects (Texture2D spriteSheet, Vector2 playerPos, List<Rectangle> wallRectList, int player) : base (spriteSheet, playerPos) {
            this.spriteSheet = spriteSheet;
            this.playerPos = playerPos;
            this.wallRectList = wallRectList;
            this.player = player;
            HP = 100;
            activeWeapon = 0;
            equipedWeapons = 0;

            sourceRect = new Rectangle((30 * player), 3, 25,25);
            weaponSlot = new WeaponObjects[2];

            scale = 1;
            speed = 100;
            playerFx = SpriteEffects.None;
            playerRect = new Rectangle((int)playerPos.X, (int)playerPos.Y, 25, 25);

            //hud = new Hud(spriteSheet, playerPos, wallRectList, player);
        }

        public override void Update(GameTime gameTime) {
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released) {
                if (!(weaponSlot[activeWeapon] == null)) {
                    mousePos = new Vector2(mouseState.X, mouseState.Y);
                    weaponSlot[activeWeapon].Attack(mousePos, playerPos, wallRectList, player);
                }
            }
            SwitchWeapon();
            //Console.WriteLine("Namn på vapen: "+weaponSlot[activeWeapon].name

            if (!isMoving) {
                if (keyState.IsKeyDown(Keys.A)) {
                    ChangeDirection(new Vector2(-1, 0));
                    rotation = MathHelper.ToRadians(90);

                } else if (keyState.IsKeyDown(Keys.W)) {
                    ChangeDirection(new Vector2(0, -1));
                    rotation = MathHelper.ToRadians(-180);

                } else if (keyState.IsKeyDown(Keys.D)) {
                    ChangeDirection(new Vector2(1, 0));
                    rotation = MathHelper.ToRadians(-90);

                } else if (keyState.IsKeyDown(Keys.S)) {
                    ChangeDirection(new Vector2(0, 1));
                    rotation = MathHelper.ToRadians(0);
                }
            } else {
                playerPos += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Vector2.Distance(playerPos, destination) < 1) {
                    playerPos = destination;
                    playerHitBox = new Rectangle((int)playerPos.X, (int)playerPos.Y, 25, 25);
                    isMoving = false;

                }

            }
            oldKeyState = keyState;
            oldMouseState = mouseState;
        }

        private void SwitchWeapon() {
            if (keyState.IsKeyDown(Keys.F1)) {
                activeWeapon = 0;
            }
            if (keyState.IsKeyDown(Keys.F2)) {
                activeWeapon = 1;
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
                }
                else {
                    hitWall = false;
                }
            }
            
            //Console.WriteLine("is hit walL: " + hitWall);
            
            if (!hitWall) {
                destination = newDestination;
                isMoving = true;
            }
            if (hitWall) {
                isMoving = false;
            }
        }
        public void GetHit(int damage) {
            HP -= damage;
        }
        public Rectangle GetRect() {
            return playerRect;
        }

        public override void Draw(SpriteBatch sb) { //Alla rotarerar med spelare.. kan lösas med att ha en sorts array på rotation, är det värt koden?
            sb.Draw(spriteSheet, new Vector2(playerPos.X + 12, playerPos.Y + 12), sourceRect, Color.White, rotation, new Vector2(12.5f, 12.5f), scale, playerFx, 1);
            //hud.Draw(sb);
        }

        public Vector2 SendPos() {
            return playerPos;
        }

        public void EquipedWeapon(WeaponObjects weaponStats) {
                weaponSlot[activeWeapon] = weaponStats;
        }
    }
}
