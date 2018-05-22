﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceholderGame {
    class OptionsMenu {

        //Antal spelare, antalet vapen, se alla vapen
        Game1 game;
        int nrPlayers,nrWeapons;
        bool[] seeWeapons;
        Vector2 mousePos;
        Texture2D optionTex;
        MouseState mouseState, oldMouseState;
        Rectangle returnRec, twoPlayerRec, threePlayerRec, fourPlayerRec;
        Rectangle bananaRec, waterGunRec, laserSwordRec, baseBallBatRec;
        Rectangle soundRec;

        public OptionsMenu(Texture2D optionTex ,int nrPlayers, int nrWeapons, bool[] seeWeapons) {
            this.nrPlayers = nrPlayers;
            this.nrWeapons = nrWeapons;
            this.seeWeapons = seeWeapons;
            this.optionTex = optionTex;

            this.returnRec = new Rectangle(701,832, 198, 46);
            this.twoPlayerRec = new Rectangle(714, 733, 49, 49);
            this.threePlayerRec = new Rectangle(776, 733, 49, 49);
            this.fourPlayerRec = new Rectangle(839, 733, 49, 49);

            this.bananaRec = new Rectangle(727, 259, 49, 49);
            this.waterGunRec = new Rectangle(835, 259, 49, 49);
            this.laserSwordRec = new Rectangle(726, 332, 49, 49);
            this.baseBallBatRec = new Rectangle(835, 334, 49, 49);

            soundRec = new Rectangle(786, 564, 49, 49);

        }

        public void Update() {
            //Beroende på vilken ruta man klickar på ska rätt bool[i] blir false/true
            //If (bananaRect.Intesect(mousePos)... bool[0] = false;
            //for (int i = 0; i < 3; i++) {
            //    if()
            //}
            //If (returnRect.Intersect(mousePos)... Game1
            mouseState = Mouse.GetState();
            mousePos = new Vector2(mousePos.X, mousePos.Y);

            if (twoPlayerRec.Contains(mousePos)) {
                nrPlayers = 2;
            }
            if (threePlayerRec.Contains(mousePos)) {
                nrPlayers = 3;
            }

            if (fourPlayerRec.Contains(mousePos)) {
                nrPlayers = 4;
            }

            if (returnRec.Contains(mousePos)) {
                game.HideMenu();
            }
        }
        public bool[] ReturnWeapon() {
            return seeWeapons;
        }
        public int ReturnPlayers() {
            return nrPlayers;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(optionTex, new Vector2(0,0) , Color.White);

        }

    }
}
