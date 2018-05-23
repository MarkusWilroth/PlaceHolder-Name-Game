using Microsoft.Xna.Framework;
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

    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GroundObjects groundO;
        WallObjects wallO;
        Bullet bulletO;
        WeaponObjects weaponO;
        PlayerObjects[] playerO;
        OptionsMenu optionsMenu;
        PauseMenu pauseMenu;
        Hud hud;
        MouseState mouseState, oldMouse;

        Vector2 pos, playerPos;
        Rectangle[] sourceRect;
        Rectangle startRec, optionsRec, quitRec, mousePos, wallRect, playerRect, sourceWeapon;
        List<GameObjects> gameList;
        List<GroundObjects> groundList;
        List<WeaponObjects> weaponList;
        List<Vector2> wallPosList, groundPosList, playerPosList, posList, weaponPosList;
        List<Rectangle> wallRectList;
        List<Bullet> bulletList;
        SpriteFont spriteFont;

        string getLine, winnerText;
        bool isPicked, isBulletDead, turnCounter;
        bool[] isPlayerDead, seeWeapons;
        int groundX, groundY, count, player, weaponID, amountWeapon, HP, ammo, deadPlayers;
        public int levels, currentLevel, players;
        char textLetter;

        String[] printMap, printObjects;        
        Texture2D spriteSheet, shot, startMenu, hudTex, pauseTex, controlsTex, optionTex;
        
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
            amountWeapon = 8;
            rnd = new Random();
            sourceRect = new Rectangle[3];
            printMap = new String[levels];
            seeWeapons = new bool[] {true, true, true, true, true, true, true, true };
            printObjects = new string[levels];

            gameList = new List<GameObjects>();
            wallPosList = new List<Vector2>();
            groundPosList = new List<Vector2>();
            playerPosList = new List<Vector2>();
            weaponPosList = new List<Vector2>();
            weaponList = new List<WeaponObjects>();
            bulletList = new List<Bullet>();
            posList = new List<Vector2>();
            wallRectList = new List<Rectangle>();
            groundList = new List<GroundObjects>();
            currentLevel = 0;
            
            FileReader();            
        }
       
        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            startMenu = Content.Load<Texture2D>("Startmenyn"); //För menyn
            spriteSheet = Content.Load<Texture2D>("Spritesheet"); //Måste fixas så att mellanrummet mellan spelarna är identiska så vi slipper hårdkodning
            shot = Content.Load<Texture2D>("Skott");
            hudTex = Content.Load<Texture2D>("Hud");
            optionTex = Content.Load<Texture2D>("OptionMeny");
            spriteFont = Content.Load<SpriteFont>("spriteFont");
            pauseTex = Content.Load<Texture2D>("PauseMeny");
            controlsTex = Content.Load<Texture2D>("Controls");

            startRec = new Rectangle(695, 432, 200, 50);
            optionsRec = new Rectangle(697, 503, 199, 49);
            quitRec = new Rectangle(698, 577, 199, 49);

            hud = new Hud(hudTex, spriteSheet, players);
            pauseMenu = new PauseMenu(pauseTex, controlsTex);
            optionsMenu = new OptionsMenu(optionTex, players, amountWeapon, seeWeapons);
            playerO = new PlayerObjects[players];

            IsMouseVisible = true;
            isPlayerDead = new bool[] { true, true, true, true };
            deadPlayers = 1;
            winnerText = ""; 
                        
            for (int i = 0; i < players; i++) {
                isPlayerDead[i] = false;
            }

            groundPosList = posGiver(printMap, '-');
            foreach (Vector2 pos in groundPosList) {
                groundO = new GroundObjects(spriteSheet, pos);
                gameList.Add(groundO);
                groundList.Add(groundO);
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
                if(player < players) {
                    playerO[player] = new PlayerObjects(spriteSheet, pos, wallRectList, player);
                    player++;
                }
                
            }

            weaponPosList = posGiver(printObjects, 'w');
            foreach (Vector2 pos in weaponPosList) {
                weaponSpawn(pos);
            }
            player = 0;
            currentGS = GameStates.Menu;
        }

        protected override void Update(GameTime gameTime) {
            mouseState = Mouse.GetState();
            keyState = Keyboard.GetState();

            switch (currentGS) {
                case GameStates.Menu:
                    mousePos = new Rectangle(mouseState.X, mouseState.Y, 5, 5);
                    if (mouseState.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released) {
                        if (startRec.Intersects(mousePos)) {
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
                    IsMouseVisible = false;
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
                    }

                    for (int i = 0; i < players; i++) {
                        if(isPlayerDead[i]) {
                            deadPlayers++;
                        }
                    }

                    if(deadPlayers >= players) {
                        currentGS = GameStates.Scoreboard;
                    }

                    else {
                        deadPlayers = 1;
                    }

                    if (keyState.IsKeyDown(Keys.Escape)){
                        IsMouseVisible = true;
                        currentGS = GameStates.PauseMenu;
                    }
                    
                    break;

                case GameStates.PauseMenu:
                    pauseMenu.Update(this, keyState, oldKeyState);
                    break;

                case GameStates.Scoreboard:
                    for (int i = 0; i < players; i++) {
                        if (!isPlayerDead[i]) {
                            winnerText = "And the winner is Player" + (i+1)+ " !!!!!";
                        }
                    }
                    break;
            }
            oldKeyState = keyState;
            oldMouse = mouseState;


            base.Update(gameTime);
        }

        #region Player

        public bool HitPlayer(Rectangle bulletRect, int damage, int player) {
            for (int i = 0; i < players; i++) {
                playerRect = playerO[i].GetRect();
                if (bulletRect.Intersects(playerRect) && !(i==player)) {
                    isPlayerDead[i] = playerO[i].GetHit(damage);

                    foreach (GroundObjects groundO in groundList) {
                        groundO.GetBlood(playerRect);
                    }
                    return true;
                }
            }
            return false;
            
        }        

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

        public int GetHP(int i) {
            HP = playerO[i].GetHP();
            if (i < players && HP >= 0) {
                return HP;
            }
            return 0;
        }

        public int GetAmmo(int i) {
            ammo = playerO[i].GetSendAmmo();
            return ammo;
        }

        public Rectangle GetWeapon(int i, int weapon) {
            sourceWeapon = playerO[i].GetSendWeapon(weapon);
            return sourceWeapon;
        }

        #endregion

        #region Menu

        public void LeaveOptions(int players) {
            this.players = players;
            RestartGame();
        }

        public void LeavePauseMenu() {
            currentGS = GameStates.Game;
        }

        public void RestartGame() {
            weaponPosList.Clear();
            weaponList.Clear();
            gameList.Clear();
            LoadContent();
        }

        #endregion

        #region Create Map

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
        }

        public void weaponSpawn(Vector2 pos) {
            int weapon = rnd.Next(0, amountWeapon);
            switch (weapon) {
                case 0:
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
                    weaponO = new WeaponObjects("LaserRifle", 4, 10, 2, 1, spriteSheet, pos, shot, weapon, this);
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

        #endregion

        #region Bullet

        public void CreateBullet(String name, int range, int damage, int durability, int AOE, Vector2 direction, Texture2D shot, Texture2D spriteSheet, Vector2 pos, List<Rectangle> wallRectList, int player, int weapon) {
            bulletO = new Bullet(name, range, damage, durability, AOE, direction, shot, spriteSheet, pos, wallRectList, player, weapon, this);
            bulletList.Add(bulletO);
        }

        public void KillBullet() {
            bulletList.Clear();   //Ska vi har vapen som skjuter mer än ett skott måste vi ändra detta!
        }

        #endregion

        protected override void Draw(GameTime gameTime) {
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
                        if (!isPlayerDead[i]) {
                            playerO[i].Draw(spriteBatch);
                        }

                    }
                    hud.Draw(spriteBatch, spriteFont);
                    break;

                case GameStates.PauseMenu:
                    pauseMenu.Draw(spriteBatch);
                    break;

                case GameStates.Scoreboard:
                    spriteBatch.DrawString(spriteFont, winnerText, new Vector2(775, 435), Color.GhostWhite);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
