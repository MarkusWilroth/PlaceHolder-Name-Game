using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlaceholderGame {
    class OptionsMenu {
        int nrPlayers,nrWeapons;
        bool[] seeWeapons;
        Texture2D optionTex;
        MouseState mouseState, oldMouseState;
        Rectangle returnRec, twoPlayerRec, threePlayerRec, fourPlayerRec, mouseRectPos, soundRec;

        public OptionsMenu(Texture2D optionTex ,int nrPlayers, int nrWeapons, bool[] seeWeapons) {
            this.nrPlayers = nrPlayers;
            this.nrWeapons = nrWeapons;
            this.seeWeapons = seeWeapons;
            this.optionTex = optionTex;

            CreateRectangle();
        }

        private void CreateRectangle() {
            returnRec = new Rectangle(666, 717, 305, 70);
            twoPlayerRec = new Rectangle(684, 473, 77, 77);
            threePlayerRec = new Rectangle(778, 473, 77, 77);
            fourPlayerRec = new Rectangle(875, 473, 77, 77);

            soundRec = new Rectangle(766, 173, 100, 100);
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
