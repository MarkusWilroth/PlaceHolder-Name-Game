using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Graphics
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlaceholderGame {

    class Hud{
        int players;
        int[] HP;
        Rectangle[] sourceHP, sourceActiveWeapon1, sourceActiveWeapon2, sourceAmmo, sourcePlayer, playerPicPos, redBox, HPPos;
        Texture2D hudTex, spriteSheet;
        Vector2[] hudPos;
        PlayerObjects[] playerO;

        float bgWidth;
        float bgHeight;

        public Hud(Texture2D hudTex, Texture2D spriteSheet, int players){
            this.HP = HP;
            this.spriteSheet = spriteSheet;
            this.players = players;
            this.hudTex = hudTex;
            this.hudPos = hudPos;
            HP = new int[players];
            redBox = new Rectangle[players];
            HPPos = new Rectangle[players];
            playerPicPos = new Rectangle[players];
            sourcePlayer = new Rectangle[players];
            sourceAmmo = new Rectangle[players];
            sourceHP = new Rectangle[players];
            sourceActiveWeapon1 = new Rectangle[players];
            sourceActiveWeapon2 = new Rectangle[players];

            for (int i = 0; i < players; i++) {
                if (i < 2) {
                    sourcePlayer[i] = new Rectangle(0 + 105*i, 150, 100, 100);
                    playerPicPos[i] = new Rectangle(80 + 1315 * i, 30, 120, 120);
                    HPPos[i] = new Rectangle(290 + 1025 * i, 40, 50, 50);
                }
                else {
                    sourcePlayer[i] = new Rectangle(0 + 105 * i, 150, 100, 100);
                    playerPicPos[i] = new Rectangle(80 + 1315 * (i-2), 514, 120, 120);
                    HPPos[i] = new Rectangle(280 + 1035 * (i - 2), 520, 50, 50);
                }

            } 
        }

        //public void Initialize(float screenWidth, float screenHeight, Texture2D hudTex) {

        //    bgHeight = screenHeight;
        //    bgWidth = screenWidth * (screenHeight / hudTex.Height);
        //    this.hudTex = hudTex;

        //    for (int i = 0; i < hudPos.Length; i++)
        //        hudPos[i].X = i * bgWidth;
        //}

        public void Update(GameTime game){
            for (int i = 0; i < players; i++) {
                HP[i] = playerO[i].GetHP();
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(hudTex, new Vector2(0, 0), Color.White);
            for (int i = 0; i < players; i++) {
                spriteBatch.Draw(spriteSheet, playerPicPos[i], sourcePlayer[i], Color.White);
                spriteBatch.Draw(spriteSheet, redBox[i], new Rectangle(150, 30, 20, 20), Color.Red);
            }
        }
    }
}
