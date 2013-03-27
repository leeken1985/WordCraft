using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TestGame2._0.Backend
{
    //TODO: Implement gamePiece class
    class GameArea 
    {
        private const int areaWidth = 8; //8 columns
        private const int areaHeight = 14; //14 rows
        private GamePiece[,] boardSquares = new GamePiece[areaWidth, areaHeight]; //board squares
        
        public GameArea()
        {
            //clear game area.
            ClearArea();
        }

        public void ClearArea()
        {
            for (int x = 0; x < areaWidth; x++)
            {
                for (int y = 0; y < areaHeight; y++)
                {
                    //initialize entire board area to nothing
                    boardSquares[x, y] = new GamePiece(""); 
                }
            }
        }
    }
}
