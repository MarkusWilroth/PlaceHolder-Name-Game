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
        Texture2D pauseTex, controlsTex;
        Rectangle continueRec, controlsRec, restartRec, quitRec, mouseRecPos;
        MouseState mouseState, oldMouseState;
        bool showControls;


        public PauseMenu(Texture2D pauseTex, Texture2D controlsTex) {
            this.pauseTex = pauseTex;
            this.controlsTex = controlsTex;

            continueRec = new Rectangle(692, 353, 200, 46);
            controlsRec = new Rectangle(695, 432, 200, 46);
            restartRec = new Rectangle(693, 508, 200, 46);
            quitRec = new Rectangle(692, 585, 200, 46);
            showControls = false;
        }

        public void Update(Game1 game, KeyboardState keyboardState, KeyboardState oldkeyboardState) {
            mouseState = Mouse.GetState();
            mouseRecPos = new Rectangle((int)mouseState.X, (int)mouseState.Y, 5, 5);

            if (!showControls) {
                if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released) {
                    if (continueRec.Intersects(mouseRecPos)) {
                        game.LeavePauseMenu();
                    }

                    if (controlsRec.Intersects(mouseRecPos)) {
                        showControls = true;
                    }

                    if (restartRec.Intersects(mouseRecPos)) {
                        game.RestartGame();

                    }

                    if (quitRec.Intersects(mouseRecPos)) {
                        game.Exit();
                    }

                }
            }
            
            if (keyboardState.IsKeyDown(Keys.Escape)) {
                showControls = false;
            }
            oldMouseState = mouseState;
        }



        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(pauseTex, Vector2.Zero, Color.White);
            if (showControls) {
                spriteBatch.Draw(controlsTex, Vector2.Zero, Color.White);
            }
        }
    }
}
