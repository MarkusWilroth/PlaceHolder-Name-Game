using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceholderGame
{
    class GroundObjects : GameObjects
    {
        List<Vector2> groundList, wallList;
        Texture2D ground, TileWall;
        float scale;
        SpriteEffects fx;

        public GroundObjects(Texture2D ground, Texture2D TileWall, String[] printLevel, Game1 game):base(ground, printLevel, game)
        {
            this.ground = ground;
            this.TileWall = TileWall;

            groundList = new List<Vector2>();
            wallList = new List<Vector2>();

            groundList = new List<Vector2>(GetPos('-', game.currentLevel));
            wallList = new List<Vector2>(GetPos('v', game.currentLevel));

            fx = SpriteEffects.None;
            int i = 0;
            foreach (Vector2 pos in wallList) {
                
                i++;
            }
            Console.WriteLine("Pos (groundO) [" + i + "] ");

            //getWall(wallList);
        }
        public override void Update(Game game, GameTime gameTime)
        {
            
        }


        public override void Draw(SpriteBatch sb)
        {
            foreach(Vector2 pos in groundList)
            {
                sb.Draw(ground, pos, null, Color.White, null, scale, fx, 1);
            }
            foreach(Vector2 pos in wallList)
            {
                sb.Draw(TileWall, pos, Color.White);
            }
        }
    }
}
