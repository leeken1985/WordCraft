﻿using System;
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
        private const int column = 8; //8 columns
        private const int row = 14; //14 rows
        private GamePiece[,] boardSquares = new GamePiece[column, row]; //board squares
        
        public GameArea()
        {
            //clear game area.
            ClearArea();
        }

        public void ClearArea()
        {
            for (int x = 0; x < column; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    //initialize entire board area to nothing
                    boardSquares[x, y] = new GamePiece(""); 
                }
            }
        }

        /// <summary>
        /// Returns a gamepiece on the specified co-ordinate.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public GamePiece getPiece(int userColumn, int userRow)
        {
            return boardSquares[userColumn, userRow];
        }


        /// <summary>
        /// Sets a game piece on the specified co-ordinate.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="piece"></param>
        public void setPiece(int userColumn, int userRow, GamePiece piece)
        {
            boardSquares[userColumn, userRow] = piece;
        }


        /// <summary>
        /// Requires a Y coordinate
        /// </summary>
        /// <param name="y"></param>
        public string getLine(int userRow)
        {
            string lineString = "";
            for (int i = 0; i < column; i++)
            {
                GamePiece temp = boardSquares[column, userRow];
                lineString += temp.getValue();
            }
            return lineString;
        }

        /// <summary>
        /// Returns the string in the Y direction (top down)
        /// </summary>
        /// <param name="userColumn"></param>
        /// <returns></returns>
        public string getColumn(int userColumn)
        {
            string lineString = "";
            for (int i = 0; i < row; i++)
            {
                GamePiece temp = boardSquares[userColumn, row];
                lineString += temp.getValue();
            }
            return lineString;
        }

    }
}
