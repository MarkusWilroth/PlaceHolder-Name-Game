using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceholderGame {
    class PauseMenu {
        Game1 game;
        Texture2D pauseTex;
        Rectangle continueRec, controlsRec, restartRec, quitRec, mouseRecPos;
        MouseState mouseState, oldMouseState;


        public PauseMenu(Texture2D pauseTex) {
            this.pauseTex = pauseTex;

            continueRec = new Rectangle(692, 353, 200, 46);
            controlsRec = new Rectangle(695, 432, 200, 46);
            restartRec = new Rectangle(693, 508, 200, 46);
            quitRec = new Rectangle(692, 585, 200, 46);
        }

        public void Update(Game1 game) {
            mouseState = Mouse.GetState();
            mouseRecPos = new Rectangle((int)mouseState.X, (int)mouseState.Y, 5, 5);

            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released) {
                if (continueRec.Intersects(mouseRecPos)) {
                    game.LeavePauseMenu();
                }

                if (controlsRec.Intersects(mouseRecPos)) {

                }

                if (restartRec.Intersects(mouseRecPos)) {
                    game.RestartGame();

                }

                if (quitRec.Intersects(mouseRecPos)) {
                    game.Exit();
                }
            }
            

            oldMouseState = mouseState;


        }



        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(pauseTex, new Vector2(0, 0), Color.White);

        }
    }
}
