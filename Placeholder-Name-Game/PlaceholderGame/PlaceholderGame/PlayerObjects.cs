﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Input;

namespace PlaceholderGame {
    class PlayerObjects : GameObjects {
        Texture2D picPlayer;
        Vector2 direction, playerPos, destination;
        Rectangle playerRect, playerHitBox, playerDest;
        List<Rectangle> wallRectList;
        int newDestX, newDestY;
        bool hitWall, isMoving;
        float speed, scale, rotation;
        SpriteEffects playerFx;
        KeyboardState keyState, oldKeyState;
        

        public PlayerObjects (Texture2D picPlayer, Vector2 playerPos, List<Rectangle> wallRectList) : base (picPlayer, playerPos) {
            this.picPlayer = picPlayer;
            this.playerPos = playerPos;
            this.wallRectList = wallRectList;

            scale = 0.5f;
            speed = 100;
            playerFx = SpriteEffects.None;
            playerRect = new Rectangle((int)playerPos.X, (int)playerPos.Y, 25, 25);
        }

        public override void Update(GameTime gameTime) {
            keyState = Keyboard.GetState();
            if (!isMoving) {
                if (keyState.IsKeyDown(Keys.Left)) {
                    ChangeDirection(new Vector2(-1, 0));
                    rotation = MathHelper.ToRadians(90);

                } else if (keyState.IsKeyDown(Keys.Up)) {
                    ChangeDirection(new Vector2(0, -1));
                    rotation = MathHelper.ToRadians(-180);

                } else if (keyState.IsKeyDown(Keys.Right)) {
                    ChangeDirection(new Vector2(1, 0));
                    rotation = MathHelper.ToRadians(-90);

                } else if (keyState.IsKeyDown(Keys.Down)) {
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
        }
        private void ChangeDirection(Vector2 dir) {
            direction = dir;
            Vector2 newDestination = playerPos + (direction * 25);
            
            newDestX = (int)newDestination.X;
            newDestY = (int)newDestination.Y;
            playerDest = new Rectangle(newDestX, newDestY, 25, 25);

            foreach (Rectangle wallRect in wallRectList) {
                hitWall = HitWall(playerDest, wallRect);
                if (hitWall) {
                    break;
                }
            }
            
            Console.WriteLine("is hit walL: " + hitWall);
            
            if (!hitWall) {
                destination = newDestination;
                isMoving = true;
            }
            if (hitWall) {
                isMoving = false;
            }
        }

        public override void Draw(SpriteBatch sb) { //Alla rotarerar med spelare.. kan lösas med att ha en sorts array på rotation, är det värt koden?
            sb.Draw(spriteSheet, new Vector2(playerPos.X + 12, playerPos.Y + 12), null, Color.White, rotation, new Vector2(25, 25), scale, playerFx, 1);
        }
    }
}
