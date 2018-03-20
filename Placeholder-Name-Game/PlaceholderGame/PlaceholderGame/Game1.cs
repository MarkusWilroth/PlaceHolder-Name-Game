﻿using Microsoft.Xna.Framework;
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
        PlayerObjects playerO;
        List<GameObjects> gameList;
        string getLine;
        Vector2 pos, wallPos, groundPos, playerPos;
        int groundX, groundY;
        char textLetter;
        String[] printMap, printObjects;
        public int levels, currentLevel, players;
        Texture2D ground, tileWall, picPlayer;
        GameStates currentGS;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            players = 4; //Flyttas till menyn, bestämmer hur många spelare det är!
            levels = 1; //Hur många banfiler, asså hur många banor man man spela på

            printMap = new String[levels];
            printObjects = new string[levels];
            gameList = new List<GameObjects>();
            currentLevel = 0;
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
            for (int i = 0; i < printMap[currentLevel].Length; i++) {
                wallPos = posGiver(printMap, 'v', i);
                wallO = new WallObjects(tileWall, wallPos, this);
                gameList.Add(wallO);
            }
            ResetMap();
            for (int i = 0; i < printMap[currentLevel].Length; i++) {
                groundPos = posGiver(printMap, '-', i);
                groundO = new GroundObjects(ground, groundPos, this);
                gameList.Add(groundO);
            }
            ResetMap();
            for (int i = 0; i < printMap[currentLevel].Length; i++) {
                playerPos = posGiver(printMap, 's', i);
                playerO = new PlayerObjects(picPlayer, playerPos, this);
                gameList.Add(playerO);
            }
            ResetMap();

        }
        protected override void Update(GameTime gameTime) { //Testa att ta bort Game1 game från alla updates
            switch (currentGS) { //gameStates
                case GameStates.Menu:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter)){
                        currentGS = GameStates.Game;
                    }
                    break;
                case GameStates.Game:
                    playerO.Update(gameTime);
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
        public Vector2 posGiver(String[] printLevel, char getLetter, int i) {
            //for (int i = 0; i < printLevel[currentLevel].Length; i++) {
                textLetter = printLevel[currentLevel][i];
                if (textLetter == getLetter) {
                    pos = new Vector2(groundX, groundY);
                    groundX += 25;
                    //break;
                } else if (textLetter == '|') {
                    groundX = 350;
                    groundY += 25;
                } else if (textLetter == '#') {
                    groundX = 350;
                    groundY = 0;
                } else {
                    groundX += 25;
                }
            //}
            return pos;
        }
        public void ResetMap () {
            groundX = 350;
            groundY = 0;
        }
    }
}
