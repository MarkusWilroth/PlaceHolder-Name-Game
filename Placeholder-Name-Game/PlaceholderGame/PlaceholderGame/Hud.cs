using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlaceholderGame {

    class Hud : PlayerObjects{
        int HP;
        Texture2D hudTex;
        Vector2[] hudPos;

        float bgWidth;
        float bgHeight;

        public Hud(Texture2D spriteSheet, Vector2 playerPos, List<Rectangle> wallRectList, int player) : base(spriteSheet, playerPos, wallRectList, player){

        }

        //public void Initialize(float screenWidth, float screenHeight, Texture2D hudTex){

        //    bgHeight = screenHeight;
        //    bgWidth = screenWidth *(screenHeight/hudTex.Height);
        //    this.hudTex = hudTex;

        //    for (int i = 0; i < hudPos.Length; i++)
        //        hudPos[i].X = i * bgWidth;
        //}

        public void Update(GameTime game){

        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(hudTex, new Vector2(0, 0), Color.White);
        }
    }
}
