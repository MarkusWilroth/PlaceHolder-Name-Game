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
        Texture2D picPlayer;
        Vector2 movement, direction, playerPos, destination, newDestination;
        Rectangle playerRect, playerHitBox;
        int newDestX, newDestY, player, players, deBugPlayer;
        bool hitWall, isMoving;
        float speed, scale, rotation;
        SpriteEffects playerFx;
        KeyboardState keyState, oldKeyState;
        

        public PlayerObjects (Texture2D picPlayer, Vector2 playerPos) : base (picPlayer, playerPos) {
            this.picPlayer = picPlayer;
            this.playerPos = playerPos;
            //this.players = game.players;
            scale = 0.5f;
            speed = 100;
            playerFx = SpriteEffects.None;
            playerRect = new Rectangle((int)playerPos.X, (int)playerPos.Y, 25, 25);
        }

        public override void Update(GameTime gameTime) {
            keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.End) && !(oldKeyState.IsKeyDown(Keys.End))) {
                player++;
                if (player >= players) {
                    player = 0;
                }
                Console.WriteLine("Players turn: " + player);
                
            }
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

            hitWall = HitWall(new Rectangle(newDestX, newDestY, 25, 25));
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
            sb.Draw(picPlayer, new Vector2(playerPos.X + 35, playerPos.Y + 10), null, Color.White, rotation, new Vector2(25, 25), scale, playerFx, 1);
        }
    }
}
