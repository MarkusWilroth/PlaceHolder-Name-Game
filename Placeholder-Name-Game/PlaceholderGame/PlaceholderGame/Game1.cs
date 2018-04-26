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

    public class Game1 : Game { //Vi måste komma överäns om hur klasserna ska vara uppdelade! Vart ska vapenklassen vara och hur ser den ut?
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GroundObjects groundO;
        GameObjects gameO;
        WallObjects wallO;
        Bullet bulletO;
        WeaponObjects weaponO;
        PlayerObjects[] playerO;
        OptionsMenu optionsMenu;
        Hud hud;
        MouseState mouseState, oldMouse;

        Vector2 pos, wallPos, groundPos, playerPos;
        Rectangle[] sourceRect;
        Rectangle wallRect;
        Vector2 startPos, optionsPos, quitPos; //För menyn
        Rectangle startRec, optionsRec, quitRec, mousePos; //För menyn
        List<GameObjects> gameList;
        List<WeaponObjects> weaponList;
        List<Vector2> wallPosList, groundPosList, playerPosList, posList, weaponPosList;
        List<Rectangle> wallRectList;       

        string getLine;
        bool isPicked, showMenu;        
        int groundX, groundY, count, player, weaponID;
        public int levels, currentLevel, players;
        char textLetter;

        String[] printMap, printObjects;        
        Texture2D ground, tileWall, picPlayer, spriteSheet, shot, startMenu, hudTex;
        
        KeyboardState keyState, oldKeyState;
        Random rnd;
        GameStates currentGS;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            players = 4; //Flyttas till menyn, bestämmer hur många spelare det är!
            levels = 1; //Hur många banfiler, asså hur många banor man man spela på
            player = 0;
            rnd = new Random();
            sourceRect = new Rectangle[3];
            printMap = new String[levels];
            printObjects = new string[levels];
            playerO = new PlayerObjects[players]; //Ska ta siffra från antalet aktiva vapen... måste kopplas från menyn

            gameList = new List<GameObjects>();
            wallPosList = new List<Vector2>();
            groundPosList = new List<Vector2>();
            playerPosList = new List<Vector2>();
            weaponPosList = new List<Vector2>();
            weaponList = new List<WeaponObjects>();
            posList = new List<Vector2>();
            wallRectList = new List<Rectangle>();
            currentLevel = 0;
            count = 0;
            FileReader();
            showMenu = true;
            
        }

        protected override void Initialize() {
            IsMouseVisible = true;
            base.Initialize();
        }
       
        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            startMenu = Content.Load<Texture2D>("Startmenyn"); //För menyn
            spriteSheet = Content.Load<Texture2D>("Spritesheet"); //Måste fixas så att mellanrummet mellan spelarna är identiska så vi slipper hårdkodning
            shot = Content.Load<Texture2D>("Skott");
            hudTex = Content.Load<Texture2D>("Hud version 3");
            ResetMap();
            startRec = new Rectangle(695, 432, 199, 49);
            optionsRec = new Rectangle(697, 503, 199, 49);
            quitRec = new Rectangle(698, 577, 199, 49);

            groundPosList = posGiver(printMap, '-');
            foreach (Vector2 pos in groundPosList) {
                groundO = new GroundObjects(spriteSheet, pos);
                gameList.Add(groundO);
            }

            wallPosList = posGiver(printMap, 'v');
            foreach (Vector2 pos in wallPosList) {
                wallO = new WallObjects(spriteSheet, pos);
                gameList.Add(wallO);
                wallRect = new Rectangle((int)pos.X, (int)pos.Y, 25, 25);
                wallRectList.Add(wallRect);
            }

            playerPosList = posGiver(printObjects, 's');
            foreach (Vector2 pos in playerPosList) {
                playerO[player] = new PlayerObjects(spriteSheet, pos, wallRectList, player);
                player++;
            }

            weaponPosList = posGiver(printObjects, 'w');
            foreach (Vector2 pos in weaponPosList) {
                weaponSpawn(pos);
            }
            player = 0;
        }


        protected override void Update(GameTime gameTime) { //Testa att ta bort Game1 game från alla updates
            mouseState = Mouse.GetState();
            switch (currentGS) { //gameStates
                case GameStates.Menu:
                    
                    mousePos = new Rectangle(mouseState.X, mouseState.Y, 5, 5);
                    if (mouseState.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
                    {
                        //if (startRec.Intersects(mousePos))
                        //{
                        //    //currentGS = GameStates.Game;
                        //    //showMenu = false;
                        //}                      
                    }
                        break;
                case GameStates.Game:
                    playerO[player].Update(gameTime);
                    foreach (WeaponObjects weaponO in weaponList) {
                        weaponO.Update(gameTime);
                    }
                    
                    keyState = Keyboard.GetState();
                    PickGun();
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
            oldMouse = mouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) { //Zoomfunktionen borde vara något vi kan få från Fungus Invasion
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            foreach (GameObjects gameO in gameList) {
                gameO.Draw(spriteBatch);
            }
            for (int i = 0; i < players; i++) {
                playerO[i].Draw(spriteBatch);
            }
            if (showMenu)
            {
                spriteBatch.Draw(startMenu, Vector2.Zero, Color.White); //Menyn
            }
            

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void FileReader() { //Ska vi ha fler banor behöver vi ändra de till arrays och ha struktur 
            for (int i = 0; i < 1; i++) {
                StreamReader fileMap = new StreamReader("basemap.txt"); //Filena finns i Place... > Place... > Place... > bin > Windows > x86 > Debug
                while (!fileMap.EndOfStream) {
                    getLine += fileMap.ReadLine();
                }
                fileMap.Close();
                printMap[i] = getLine;
                getLine = "";

                StreamReader fileObjects = new StreamReader("objectsMap.txt");
                while (!fileObjects.EndOfStream) {
                    getLine += fileObjects.ReadLine();
                }
                fileObjects.Close();
                printObjects[i] = getLine;
                getLine = "";
            }
        }

        public List<Vector2> posGiver(String[] printLevel, char getLetter) { //Skapar listor som ger positioner till föremål beroend på textfilen printLevel
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
        }  //Nivåerna

        public void PickGun() {
            playerPos = playerO[player].SendPos();
            if (keyState.IsKeyDown(Keys.Space) && !(oldKeyState.IsKeyDown(Keys.Space))) {

                foreach (WeaponObjects weaponStats in weaponList) {
                    isPicked = weaponStats.NewWeapon(playerPos);
                    if (isPicked) {
                        playerO[player].EquipedWeapon(weaponStats);
                        gameList.Remove(weaponStats);
                        weaponList.Remove(weaponStats);
                        break;
                    }
                }
            }
        }

        public void weaponSpawn (Vector2 pos) {
            int weapon = rnd.Next(0, 4);
            switch (weapon) { //sourceRect?
                case 0:
                    //sourceRect = new Rectangle(56, 140, 7, 32); banan uppifrån
                    weaponO = new WeaponObjects("BananaGun", 3, 2, 2, 1, spriteSheet, pos, shot, weapon);
                    break;
                case 1:
                    weaponO = new WeaponObjects("WaterGun", 4, 2, 3, 1, spriteSheet, pos, shot, weapon);
                    break;
                case 2:
                    weaponO = new WeaponObjects("LaserSword", 1, 4, 3, 1, spriteSheet, pos, shot, weapon);
                    break;
                case 3:
                    weaponO = new WeaponObjects("BaseballBat", 1, 3, 5, 1, spriteSheet, pos, shot, weapon);
                    break;
            }
            gameList.Add(weaponO);
            weaponList.Add(weaponO);
            weaponID++;
        }

        public void WeaponSource () {
            
        }
        public void ResetMap() { //Onödig funderar på att ta bort, vi får se hur det blir när man ska zooma in och zooma ut
            groundX = 350;
            groundY = 0;
        }
    }
}
