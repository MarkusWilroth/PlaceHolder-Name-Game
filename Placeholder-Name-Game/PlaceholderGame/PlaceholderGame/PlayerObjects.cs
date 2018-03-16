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
        Vector2 movement, direction;
        Vector2[] playerPos, destination, newDestination;
        List<Vector2> playerList;
        Rectangle playerRect, playerHitBox;
        List<Rectangle> playerRectList;
        int newDestX, newDestY, player, players, deBugPlayer;
        bool hitWall, isMoving;
        float speed;
        float[] rotation;
        SpriteEffects playerFx;
        KeyboardState keyState, oldKeyState;
        

        public PlayerObjects (Texture2D picPlayer, String[] printObjects, Game1 game) : base (picPlayer, printObjects, game) {
            this.picPlayer = picPlayer;
            this.players = game.players;

            playerPos = new Vector2[players];
            destination = new Vector2[players];
            newDestination = new Vector2[players];
            rotation = new float[players];




            playerList = new List<Vector2>();
            playerRectList = new List<Rectangle>();
            playerList = new List<Vector2>(GetPos('s', game.currentLevel));
            speed = 75;
            playerFx = SpriteEffects.None;
            
            

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
            player = 0;

        }

        public override void Update(Game game, GameTime gameTime) {
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
                    rotation[player] = MathHelper.ToRadians(90);

                } else if (keyState.IsKeyDown(Keys.Up)) {
                    ChangeDirection(new Vector2(0, -1));
                    rotation[player] = MathHelper.ToRadians(-180);

                } else if (keyState.IsKeyDown(Keys.Right)) {
                    ChangeDirection(new Vector2(1, 0));
                    rotation[player] = MathHelper.ToRadians(-90);

                } else if (keyState.IsKeyDown(Keys.Down)) {
                    ChangeDirection(new Vector2(0, 1));
                    rotation[player] = MathHelper.ToRadians(0);

                }

            } else {
                //frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

                playerPos[player] += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Vector2.Distance(playerPos[player], destination[player]) < 1) {
                    playerPos[player] = destination[player];
                    playerHitBox = new Rectangle((int)playerPos[player].X, (int)playerPos[player].Y, 50, 50);
                    isMoving = false;
                }

            }
            oldKeyState = keyState;





        }
        private void ChangeDirection(Vector2 dir) {
            direction = dir;
            Vector2 newDestination = playerPos[player] + (direction * 50);
            
            newDestX = (int)newDestination.X;
            newDestY = (int)newDestination.Y;

            hitWall = HitWall(new Rectangle(newDestX, newDestY, 50, 50));
            Console.WriteLine("is hit walL: " + hitWall);
            
            if (!hitWall) {
                destination[player] = newDestination;
                isMoving = true;
            }
            if (hitWall) {
                isMoving = false;
            }

        }

        public override void Draw(SpriteBatch sb) { //Alla rotarerar med spelare.. kan lösas med att ha en sorts array på rotation, är det värt koden?
        
            for (int i = 0; i < players; i++) {
                sb.Draw(picPlayer, new Vector2(playerPos[i].X + 25, playerPos[i].Y + 25), null, Color.White, rotation[i], new Vector2(25, 25), 1, playerFx, 1);
            }


        }
    }
}
