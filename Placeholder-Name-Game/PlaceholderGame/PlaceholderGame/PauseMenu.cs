using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlaceholderGame {
    class PauseMenu {
        Texture2D pauseTex, controlsTex;
        Rectangle continueRec, controlsRec, restartRec, quitRec, mouseRecPos;
        MouseState mouseState, oldMouseState;
        bool showControls;


        public PauseMenu(Texture2D pauseTex, Texture2D controlsTex) {
            this.pauseTex = pauseTex;
            this.controlsTex = controlsTex;

            CreateVariables();
        }

        private void CreateVariables() {
            continueRec = new Rectangle(620, 335, 360, 90);
            controlsRec = new Rectangle(620, 477, 360, 90);
            restartRec = new Rectangle(620, 615, 360, 90);
            quitRec = new Rectangle(620, 754, 360, 90);
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
