using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceholderGame {
    class OptionsMenu {

        //Antal spelare, antalet vapen, se alla vapen

        int nrPlayers;
        int nrWeapons;
        int seeWeapons;

        Texture2D optionsTex;

        public OptionsMenu(int nrPlayers, int nrWeapons, int seeWeapons) {
            this.nrPlayers = nrPlayers;
            this.nrWeapons = nrWeapons;
            this.seeWeapons = seeWeapons;
        }

        public void Update() {

        }

        public void Draw() {

        }

    }
}
