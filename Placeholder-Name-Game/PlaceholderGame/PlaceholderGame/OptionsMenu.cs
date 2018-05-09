using Microsoft.Xna.Framework;
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

        Rectangle returnRec, twoPlayerRec, threePlayerRec, fourPlayerRec;
        Rectangle bananaRec, waterGunRec, laserSwordRec, baseBallBatRec;
        Rectangle soundRec;

        public OptionsMenu(int nrPlayers, int nrWeapons, int seeWeapons) {
            this.nrPlayers = nrPlayers;
            this.nrWeapons = nrWeapons;
            this.seeWeapons = seeWeapons;

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
            

        }

        public void Draw() {

        }

    }
}
