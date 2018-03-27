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
        WallObjects wallO;
        PlayerObjects[] playerO;
        List<GameObjects> gameList;
        List<Vector2> wallPosList, groundPosList, playerPosList, posList;
        List<Rectangle> wallRectList;
        Rectangle wallRect;
        string getLine;
        Vector2 pos, wallPos, groundPos, playerPos;
        int groundX, groundY, count, player;
        char textLetter;
        String[] printMap, printObjects;
        public int levels, currentLevel, players;
        Texture2D ground, tileWall, picPlayer;
        GameStates currentGS;
        KeyboardState keyState, oldKeyState;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            players = 4; //Flyttas till menyn, bestämmer hur många spelare det är!
            levels = 1; //Hur många banfiler, asså hur många banor man man spela på
            player = 0;

            printMap = new String[levels];
            printObjects = new string[levels];
            playerO = new PlayerObjects[players];

            gameList = new List<GameObjects>();
            wallPosList = new List<Vector2>();
            groundPosList = new List<Vector2>();
            playerPosList = new List<Vector2>();
            posList = new List<Vector2>();
            wallRectList = new List<Rectangle>();
            currentLevel = 0;
            count = 0;
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
            ResetMap();

            groundPosList = posGiver(printMap, '-');
            foreach (Vector2 pos in groundPosList) {
                groundO = new GroundObjects(ground, pos);
                gameList.Add(groundO);
            }

            wallPosList = posGiver(printMap, 'v');
            foreach (Vector2 pos in wallPosList) {
                wallO = new WallObjects(tileWall, pos);
                gameList.Add(wallO);
                wallRect = new Rectangle((int)pos.X, (int)pos.Y, 25, 25);
                wallRectList.Add(wallRect);
            }

            playerPosList = posGiver(printObjects, 's');
            foreach (Vector2 pos in playerPosList) {
                playerO[player] = new PlayerObjects(picPlayer, pos, wallRectList);
                player++;
            }
            player = 0;

        }

        protected override void Update(GameTime gameTime) { //Testa att ta bort Game1 game från alla updates
            switch (currentGS) { //gameStates
                case GameStates.Menu:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter)){
                        currentGS = GameStates.Game;
                    }
                    break;
                case GameStates.Game:
                    playerO[player].Update(gameTime);
                    keyState = Keyboard.GetState();
                    if (keyState.IsKeyDown(Keys.End) && !(oldKeyState.IsKeyDown(Keys.End))) {
                        player++;
                        if (player >= players) {
                            player = 0;
                        }
                        Console.WriteLine("Players turn: " + player);
                    }
                    oldKeyState = keyState;
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
            for (int i = 0; i < players; i++) {
                playerO[i].Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void FileReader() { //Läser av filerna i c filen, ska vi ha fler banor behöver vi ändra de till arrays och ha struktur 
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
        public List<Vector2> posGiver(String[] printLevel, char getLetter) {
            posList.Clear();
            for (int i = 0; i < printLevel[currentLevel].Length; i++) {
                textLetter = printLevel[currentLevel][i];
                if (textLetter == getLetter) {
                    pos = new Vector2(groundX, groundY);
                    groundX += 25;
                    posList.Add(pos);
                    count++;
                } else if (textLetter == '|') {
                    groundX = 350;
                    groundY += 25;
                } else if (textLetter == '#') {
                    groundX = 350;
                    groundY = 0;
                } else {
                    groundX += 25;
                }
            }
            
            return posList;
        }
        public void ResetMap () {
            groundX = 350;
            groundY = 0;
        }
    }
}
