using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlaceholderGame {
    class OptionsMenu {
        int nrPlayers,nrWeapons;
        bool[] seeWeapons;
        Texture2D optionTex;
        MouseState mouseState, oldMouseState;
        Rectangle returnRec, twoPlayerRec, threePlayerRec, fourPlayerRec, mouseRectPos;
        Rectangle bananaRec, waterGunRec, laserSwordRec, baseBallBatRec;
        Rectangle soundRec;

        public OptionsMenu(Texture2D optionTex ,int nrPlayers, int nrWeapons, bool[] seeWeapons) {
            this.nrPlayers = nrPlayers;
            this.nrWeapons = nrWeapons;
            this.seeWeapons = seeWeapons;
            this.optionTex = optionTex;

            CreateRectangle();
        }

        private void CreateRectangle() {
            returnRec = new Rectangle(700, 786, 200, 46);
            twoPlayerRec = new Rectangle(714, 733, 49, 49);
            threePlayerRec = new Rectangle(776, 733, 49, 49);
            fourPlayerRec = new Rectangle(839, 733, 49, 49);

            bananaRec = new Rectangle(727, 259, 49, 49);
            waterGunRec = new Rectangle(835, 259, 49, 49);
            laserSwordRec = new Rectangle(726, 332, 49, 49);
            baseBallBatRec = new Rectangle(835, 334, 49, 49);

            soundRec = new Rectangle(786, 564, 49, 49);
        }

        public void Update(Game1 game) {
            mouseState = Mouse.GetState();
            mouseRectPos = new Rectangle ((int)mouseState.X, (int)mouseState.Y, 5, 5);
            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released) {
                if (twoPlayerRec.Contains(mouseRectPos)) {
                    nrPlayers = 2;
                }
                if (threePlayerRec.Contains(mouseRectPos)) {
                    nrPlayers = 3;
                }

                if (fourPlayerRec.Contains(mouseRectPos)) {
                    nrPlayers = 4;
                }

                if (returnRec.Intersects(mouseRectPos)) {
                    game.LeaveOptions(nrPlayers);
                }
            }
            oldMouseState = mouseState;
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
