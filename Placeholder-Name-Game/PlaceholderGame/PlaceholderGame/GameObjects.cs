using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceholderGame
{
    abstract class GameObjects
    {
        protected Vector2 pos;
        protected Texture2D spriteSheet;
        protected List<Vector2> posList, wallList;
        protected char textLetter;
        protected String[] printLevel;
        protected int groundX, groundY;
        protected Rectangle wallRect;
        GroundObjects groundO;
        bool hitWall;

        public GameObjects(Texture2D spriteSheet, String[] printLevel, Game1 game)
        {
            this.spriteSheet = spriteSheet;
            this.printLevel = printLevel;
            groundX = 350;
            groundY = 0;
            posList = new List<Vector2>();
            wallList = new List<Vector2>();


            wallList = new List<Vector2>(GetPos('v', game.currentLevel));
            int i = 0;
            foreach (Vector2 pos in wallList) {
                i++;
            }
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Pos (gameO) [" + i + "] ");

        }

        public abstract void Update(Game game, GameTime gameTime);

        public abstract void Draw(SpriteBatch sb);

        //protected void getWall(List<Vector2> wallList) {
        //    this.wallList = wallList;

        //}

        protected bool HitWall(Rectangle playerDest) { //pos i WallList verkar tom... varför????
            foreach (Vector2 pos in wallList) {
                wallRect = new Rectangle((int)pos.X, (int)pos.Y, 50, 50);
                if (wallRect.Intersects(playerDest)) {
                    hitWall = true;
                    break;
                }
                else {
                    hitWall = false;
                }

            }
            return hitWall;
        }

        protected List<Vector2> GetPos(char getLetter, int currentLevel)
        {
            posList.Clear();
            for (int i = 0; i < printLevel[currentLevel].Length; i++) 
            {
                textLetter = printLevel[currentLevel][i];
                if (textLetter == getLetter)
                {
                    pos = new Vector2(groundX, groundY);
                    posList.Add(pos);
                    groundX += 50;
                }
                else if (textLetter == '|')
                {
                    groundX = 350;
                    groundY += 50;
                }
                else if (textLetter == '#')
                {
                    groundX = 350;
                    groundY = 0;
                }
                else
                {
                    groundX += 50;
                }
            }
            return posList;
        }
    }
}
