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

        public Hud(Texture2D spriteSheet, Vector2 playerPos, List<Rectangle> wallRectList, int player) : base(spriteSheet, playerPos, wallRectList, player){

        }
        
        public void Draw(SpriteBatch sb){
            sb.Draw(hudTex, new Vector2(0, 0), Color.White);
        }
    }
}
