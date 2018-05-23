using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PlaceholderGame {

    class Hud{
        int players;
        int[] HP, ammo;

        String[] HPText, ammoText;
        Rectangle[] sourceActiveWeapon1, sourceActiveWeapon2, activeWeapon1Pos, activeWeapon2Pos, sourcePlayer, playerPicPos, redBox, HPPos;
        Texture2D hudTex, spriteSheet;

        public Hud(Texture2D hudTex, Texture2D spriteSheet, int players){
            this.spriteSheet = spriteSheet;
            this.players = players;
            this.hudTex = hudTex;

            CreateVariables();

            for (int i = 0; i < players; i++) {
                if (i < 2) {
                    sourcePlayer[i] = new Rectangle(0 + 105*i, 150, 100, 100);
                    playerPicPos[i] = new Rectangle(80 + 1340 * i, 30, 120, 120);
                    activeWeapon1Pos[i] = new Rectangle(78 + 1340 * i, 194, 50, 50);
                    activeWeapon2Pos[i] = new Rectangle(159 + 1340 * i, 194, 50, 50);
                    HPPos[i] = new Rectangle(300 + 1065 * i, 60, 50, 50);
                }
                else {
                    sourcePlayer[i] = new Rectangle(0 + 108 * i, 150, 100, 100);
                    playerPicPos[i] = new Rectangle(90 + 1340 * (i-2), 512, 120, 120);
                    activeWeapon1Pos[i] = new Rectangle(78 + 1340 * (i-2), 673, 50, 50);
                    activeWeapon2Pos[i] = new Rectangle(159 + 1340 * (i-2), 673, 50, 50);
                    HPPos[i] = new Rectangle(290 + 1070 * (i - 2), 545, 50, 50);
                }
                HPText[i] = "";
                ammoText[i] = "";
                sourceActiveWeapon1[i] = new Rectangle(10000, 10000, 10, 10);
                sourceActiveWeapon2[i] = new Rectangle(10000, 10000, 10, 10);
                HP[i] = 0;
            } 
        }

        private void CreateVariables() {
            ammo = new int[players];
            HP = new int[players];
            redBox = new Rectangle[players];
            HPPos = new Rectangle[players];
            playerPicPos = new Rectangle[players];
            sourcePlayer = new Rectangle[players];
            sourceActiveWeapon1 = new Rectangle[players];
            sourceActiveWeapon2 = new Rectangle[players];
            activeWeapon1Pos = new Rectangle[players];
            activeWeapon2Pos = new Rectangle[players];
            HPText = new String[players];
            ammoText = new String[players];
        }

        public void Update(Game1 game){
            for (int i = 0; i < players; i++) {
                HP[i] = game.GetHP(i);
                ammo[i] = game.GetAmmo(i);
                sourceActiveWeapon1[i] = game.GetWeapon(i, 0);
                sourceActiveWeapon2[i] = game.GetWeapon(i, 1);
                HPText[i] = ""+HP[i];
                ammoText[i] = "" + ammo[i];
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont) {
            spriteBatch.Draw(hudTex, new Vector2(0, 0), Color.White);
            for (int i = 0; i < players; i++) {
                spriteBatch.DrawString(spriteFont, HPText[i], new Vector2(HPPos[i].X, HPPos[i].Y), Color.White);
                spriteBatch.DrawString(spriteFont, ammoText[i], new Vector2(HPPos[i].X, HPPos[i].Y + 40), Color.White);
                spriteBatch.Draw(spriteSheet, activeWeapon1Pos[i], sourceActiveWeapon1[i], Color.White);
                spriteBatch.Draw(spriteSheet, activeWeapon2Pos[i], sourceActiveWeapon2[i], Color.White);
                spriteBatch.Draw(spriteSheet, redBox[i], new Rectangle(150, 30, 20, 20), Color.Red);

                if (HP[i] <= 0) {
                    spriteBatch.Draw(spriteSheet, playerPicPos[i], sourcePlayer[i], Color.Black);
                }
                else {
                    spriteBatch.Draw(spriteSheet, playerPicPos[i], sourcePlayer[i], Color.White);
                }
            }
        }
    }
}
