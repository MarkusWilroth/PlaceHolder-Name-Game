using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace PlaceholderGame {
    enum GameStates {
        Menu,
        Game,
        Scoreboard,
    }
        
         
        
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GroundObjects groundO;
        GameObjects gameO;
        PlayerObjects playerO;
        List<GameObjects> gameList;
        string getLine;
        String[] printMap, printObjects;
        public int levels, currentLevel, players;
        Texture2D ground, tileWall, picPlayer;
        GameStates currentGS;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            players = 4;
            levels = 1;
            printMap = new String[levels];
            printObjects = new string[levels];
            gameList = new List<GameObjects>();
            FileReader();
            
        }
        protected override void Initialize() {
            IsMouseVisible = true;
            base.Initialize();
        }
        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ground = Content.Load<Texture2D>("Tile");
            tileWall = Content.Load<Texture2D>("TileWall");
            picPlayer = Content.Load<Texture2D>("Placeholder_Player");

            groundO = new GroundObjects(ground, tileWall, printMap, this);
            gameList.Add(groundO);

            playerO = new PlayerObjects(picPlayer, printObjects, this);
            gameList.Add(playerO);

        }
        protected override void Update(GameTime gameTime) { //Testa att ta bort Game1 game från alla updates
            switch (currentGS) {
                case GameStates.Menu:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter)){
                        currentGS = GameStates.Game;
                    }
                    break;
                case GameStates.Game:
                    playerO.Update(this, gameTime);
                    break;
                case GameStates.Scoreboard:

                    break;
            }

            
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            foreach (GameObjects gameO in gameList) {
                gameO.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void FileReader() {
            for (int i = 0; i < 1; i++) {
                StreamReader fileMap = new StreamReader("c:\\basemap.txt");
                while (!fileMap.EndOfStream) {
                    getLine += fileMap.ReadLine();
                }
                fileMap.Close();
                printMap[i] = getLine;
                getLine = "";

                StreamReader fileObjects = new StreamReader("c:\\objectsMap.txt");
                while (!fileObjects.EndOfStream) {
                    getLine += fileObjects.ReadLine();
                }
                fileObjects.Close();
                printObjects[i] = getLine;
                getLine = "";
            }
        }
    }
}
