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
        Vector2 movement, direction, destination;
        Vector2[] playerPos;
        List<Vector2> playerList;
        Rectangle playerRect, playerHitBox;
        List<Rectangle> playerRectList;
        int newDestX, newDestY, player;
        bool hitWall, isMoving;
        float rotation, speed;
        SpriteEffects playerFx;
        

        public PlayerObjects (Texture2D picPlayer, String[] printObjects, int players, Game1 game) : base (picPlayer, printObjects, game) {
            this.picPlayer = picPlayer;

            playerPos = new Vector2[players];

            playerList = new List<Vector2>();
            playerRectList = new List<Rectangle>();
            playerList = new List<Vector2>(GetPos('s', game.currentLevel));
            speed = 75;
            playerFx = SpriteEffects.None;

            player = 0;

            foreach (Vector2 pos in playerList) {
                if (player >= players) {
                    break;
                }
                playerRect = new Rectangle((int)pos.X, (int)pos.Y, 50, 50);
                playerPos[player] = new Vector2(pos.X, pos.Y);
                playerRectList.Add(playerRect);
                player++;

            }
            playerHitBox = playerRect;

        }

        public override void Update(Game game, GameTime gameTime) {
            if (!isMoving) {
                if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                    ChangeDirection(new Vector2(-1, 0));
                    rotation = MathHelper.ToRadians(90);

                } else if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                    ChangeDirection(new Vector2(0, -1));
                    rotation = MathHelper.ToRadians(-180);

                } else if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                    ChangeDirection(new Vector2(1, 0));
                    rotation = MathHelper.ToRadians(-90);

                } else if (Keyboard.GetState().IsKeyDown(Keys.Down)) {
                    ChangeDirection(new Vector2(0, 1));
                    rotation = MathHelper.ToRadians(0);

                }

            } else {
                //frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                playerPos += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Vector2.Distance(playerPos, destination) < 1) {
                    playerPos = destination;
                    playerHitBox = new Rectangle((int)playerPos.X, (int)playerPos.Y, 50, 50);
                    isMoving = false;
                }
            }


        }
        private void ChangeDirection(Vector2 dir) {
            direction = dir;
            Vector2 newDestination = playerPos + (direction * 50);
            
            newDestX = (int)newDestination.X;
            newDestY = (int)newDestination.Y;

            hitWall = HitWall(new Rectangle(newDestX, newDestY, 50, 50));
            Console.WriteLine("is hit walL: " + hitWall);
            
            if (!hitWall) {
                destination = newDestination;
                isMoving = true;
            }
            if (hitWall) {
                isMoving = false;
            }

        }

        public override void Draw(SpriteBatch sb) {
            foreach (Rectangle playerRect in playerRectList) {
                sb.Draw(picPlayer, new Vector2(playerPos.X +25, playerPos.Y +25), null, Color.White, rotation, new Vector2(25,25), 1, playerFx, 1);
            }
        }
    }
}
