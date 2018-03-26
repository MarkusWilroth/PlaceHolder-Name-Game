using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceholderGame {
    abstract class GameObjects {
        protected Vector2 pos;
        protected Texture2D spriteSheet;
        protected List<Vector2> posList, wallList;
        protected char textLetter;
        protected String[] printLevel;
        protected int groundX, groundY;
        protected Rectangle wallRect;
        GroundObjects groundO;
        bool hitWall;

        public GameObjects(Texture2D pic, Vector2 pos) {
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch sb);

        protected bool HitWall(Rectangle playerDest) { //pos i WallList verkar tom... varför????
            foreach (Vector2 pos in wallList) {
                wallRect = new Rectangle((int)pos.X, (int)pos.Y, 25, 25);
                if (wallRect.Intersects(playerDest)) {
                    hitWall = true;
                    break;
                } else {
                    hitWall = false;
                }
            }
            return hitWall;
        }
    }
}
