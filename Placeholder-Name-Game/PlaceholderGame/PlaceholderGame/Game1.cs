﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace PlaceholderGame {
    enum GameStates {
        Menu,
        OptionMenu,
        Game,
        PauseMenu,
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
        PauseMenu pauseMenu;
        Hud hud;
        MouseState mouseState, oldMouse;

        Vector2 pos, wallPos, groundPos, playerPos;
        Rectangle[] sourceRect;
        Vector2 startPos, optionsPos, quitPos; //För menyn
        Rectangle startRec, optionsRec, quitRec, mousePos, wallRect, playerRect, sourceWeapon; //För menyn
        List<GameObjects> gameList;
        List<WeaponObjects> weaponList;
        List<Vector2> wallPosList, groundPosList, playerPosList, posList, weaponPosList;
        List<Rectangle> wallRectList;
        List<Bullet> bulletList;
        SpriteFont spriteFont;

        string getLine;
        bool isPicked, showMenu, isBulletDead, turnCounter;
        bool[] isPlayerDead, seeWeapons;
        int groundX, groundY, count, player, weaponID, amountWeapon, HP, ammo, nrPlayers, nrWeapons;
        public int levels, currentLevel, players;
        char textLetter;

        String[] printMap, printObjects;        
        Texture2D ground, tileWall, picPlayer, spriteSheet, shot, startMenu, hudTex, pauseTex;
        Texture2D pausTex, optionTex;
        
        KeyboardState keyState, oldKeyState;
        Random rnd;
        GameStates currentGS;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1625;
            graphics.PreferredBackBufferHeight = 900;
            players = 4; //Flyttas till menyn, bestämmer hur många spelare det är!
            levels = 1; //Hur många banfiler, asså hur många banor man man spela på
            player = 0;
            amountWeapon = 8;
            rnd = new Random();
            sourceRect = new Rectangle[3];
            printMap = new String[levels];
            isPlayerDead = new bool[] { false, false, false, false };
            seeWeapons = new bool[] {true, true, true, true, true, true, true, true };
            printObjects = new string[levels];
            playerO = new PlayerObjects[players]; //Ska ta siffra från antalet aktiva vapen... måste kopplas från menyn

            gameList = new List<GameObjects>();
            wallPosList = new List<Vector2>();
            groundPosList = new List<Vector2>();
            playerPosList = new List<Vector2>();
            weaponPosList = new List<Vector2>();
            weaponList = new List<WeaponObjects>();
            bulletList = new List<Bullet>();
            posList = new List<Vector2>();
            wallRectList = new List<Rectangle>();
            currentLevel = 0;
            count = 0;
            
            FileReader();
            showMenu = false;
            
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
            hudTex = Content.Load<Texture2D>("Hud");
            pausTex = Content.Load<Texture2D>(@"PauseMeny");
            optionTex = Content.Load<Texture2D>(@"OptionMeny");
            spriteFont = Content.Load<SpriteFont>("spriteFont");
            pauseTex = Content.Load<Texture2D>("PauseMeny");
            startRec = new Rectangle(695, 432, 200, 50);
            optionsRec = new Rectangle(697, 503, 199, 49);
            quitRec = new Rectangle(698, 577, 199, 49);
            optionsMenu = new OptionsMenu(optionTex, nrPlayers, nrWeapons, seeWeapons);
            
            pauseMenu = new PauseMenu(pauseTex);

            hud = new Hud(hudTex, spriteSheet, players);

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
            currentGS = GameStates.Menu;
        }


        protected override void Update(GameTime gameTime) { //Testa att ta bort Game1 game från alla updates
            mouseState = Mouse.GetState();
            switch (currentGS) { //gameStates
                case GameStates.Menu:
                    mousePos = new Rectangle(mouseState.X, mouseState.Y, 5, 5);
                    if (mouseState.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released) {
                        if (startRec.Intersects(mousePos))
                        {
                            currentGS = GameStates.Game;                            
                        }
                        if (quitRec.Intersects(mousePos)) {
                            Exit();
                        }
                        if (optionsRec.Intersects(mousePos)) {
                            currentGS = GameStates.OptionMenu;
                        }
                    }
                    break;

                case GameStates.OptionMenu:
                    optionsMenu.Update(this);
                    break;

                case GameStates.Game:
                    playerO[player].Update(gameTime);
                    hud.Update(this);
                    foreach (WeaponObjects weaponO in weaponList) {
                        weaponO.Update(gameTime);
                        foreach (Bullet bulletO in bulletList) {
                            bulletO.Update(gameTime);
                            isBulletDead = bulletO.KillBullet();
                            if (isBulletDead) {
                                bulletList.Remove(bulletO);
                                break;
                            }
                        }
                    }
                    
                    keyState = Keyboard.GetState();
                    PickGun();
                    turnCounter = playerO[player].Counter();
                    if (turnCounter) {                        
                        do {
                            player++;
                            if (player >= players) {
                                player = 0;
                            }
                        } while (isPlayerDead[player]);
                        turnCounter = false;
                        Console.WriteLine("Players turn: " + player);                                                     
                    }
                    if (keyState.IsKeyDown(Keys.Escape)){
                        currentGS = GameStates.PauseMenu;
                    }
                    oldKeyState = keyState;
                    break;
                case GameStates.PauseMenu:
                    pauseMenu.Update(this);
                    break;
                case GameStates.Scoreboard:
                    break;
            }
            oldMouse = mouseState;


            base.Update(gameTime);
        }

        public bool HitPlayer(Rectangle bulletRect, int damage, int player) {
            for (int i = 0; i < players; i++) {
                playerRect = playerO[i].GetRect();
                if (bulletRect.Intersects(playerRect) && !(i==player)) {
                    isPlayerDead[i] = playerO[i].GetHit(damage);
                    return true;
                }
            }
            return false;
            
        }
        public void LeaveOptions(int players) {
            this.players = players;
            currentGS = GameStates.Menu;
        }

        protected override void Draw(GameTime gameTime) { //Zoomfunktionen borde vara något vi kan få från Fungus Invasion
            GraphicsDevice.Clear(Color.DimGray);
            spriteBatch.Begin();
            switch (currentGS) {
                case GameStates.Menu:
                    spriteBatch.Draw(startMenu, Vector2.Zero, Color.White);
                    break;

                case GameStates.OptionMenu:
                    optionsMenu.Draw(spriteBatch);
                    break;

                case GameStates.Game:
                    
                    foreach (GameObjects gameO in gameList) {
                        gameO.Draw(spriteBatch);
                    }
                    foreach (Bullet bulletO in bulletList) {
                        bulletO.Draw(spriteBatch);
                    }
                    for (int i = 0; i < players; i++) {
                        if(!isPlayerDead[i]) {
                            playerO[i].Draw(spriteBatch);
                        }
                        
                    }      
                    hud.Draw(spriteBatch, spriteFont);
                    break;
                case GameStates.PauseMenu:
                    pauseMenu.Draw(spriteBatch);
                    break;

                case GameStates.Scoreboard:
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void LeavePauseMenu() {
            currentGS = GameStates.Game;
        }
        public void RestartGame() {
            weaponPosList.Clear();
            gameList.Clear();
            LoadContent();            
        }
        public int GetHP(int i) {
            HP = playerO[i].GetHP();
            return HP;
        }

        public int GetAmmo(int i) {
            ammo = playerO[i].GetSendAmmo();
            return ammo;
        }
        public Rectangle GetWeapon(int i, int weapon) {
            sourceWeapon = playerO[i].GetSendWeapon(weapon);
            return sourceWeapon;
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
        public void CreateBullet(String name, int range, int damage, int durability, int AOE, Vector2 direction, Texture2D shot, Texture2D spriteSheet, Vector2 pos, List<Rectangle> wallRectList, int player, int weapon) {
            bulletO = new Bullet(name, range, damage, durability, AOE, direction, shot, spriteSheet, pos, wallRectList, player, weapon, this);
            bulletList.Add(bulletO);
        }
        public void KillBullet() {
            bulletList.Clear();   //Ska vi har vapen som skjuter mer än ett skott måste vi ändra detta!
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

                foreach (WeaponObjects weaponO in weaponList) {
                    isPicked = weaponO.NewWeapon(playerPos);
                    if (isPicked) {
                        playerO[player].EquipedWeapon(weaponO);
                        gameList.Remove(weaponO);
                        weaponList.Remove(weaponO);
                        break;
                    }
                }
            }
        }

        public void weaponSpawn (Vector2 pos) {
            int weapon = rnd.Next(0, amountWeapon);
            switch (weapon) { //sourceRect?
                case 0:
                    //sourceRect = new Rectangle(56, 140, 7, 32); banan uppifrån
                    weaponO = new WeaponObjects("BananaGun", 3, 5, 3, 1, spriteSheet, pos, shot, weapon, this);
                    break;
                case 1:
                    weaponO = new WeaponObjects("WaterGun", 4, 8, 5, 1, spriteSheet, pos, shot, weapon, this);
                    break;
                case 2:
                    weaponO = new WeaponObjects("BaseballBat", 3, 1, 5, 1, spriteSheet, pos, shot, weapon, this);
                    break;
                case 3:
                    weaponO = new WeaponObjects("LaserSword", 3, 1, 5, 1, spriteSheet, pos, shot, weapon, this);
                    break;                
                case 4:
                    weaponO = new WeaponObjects("SlingShot", 4, 5, 3, 1, spriteSheet, pos, shot, weapon, this);
                    break;
                case 5:
                    weaponO = new WeaponObjects("LaserRifle", 2, 10, 5, 1, spriteSheet, pos, shot, weapon, this);
                    break;
                case 6:
                    weaponO = new WeaponObjects("PickleGun", 3, 6, 3, 1, spriteSheet, pos, shot, weapon, this);
                    break;
                case 7:
                    weaponO = new WeaponObjects("ChickenClub", 3, 1, 6, 1, spriteSheet, pos, shot, weapon, this);
                    break;
            }
            gameList.Add(weaponO);
            weaponList.Add(weaponO);
            weaponID++;
        }
    }
}
