using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GarbageTest
{
    class GamePiece
    {
        private const int blockWidth = 50;
        private const int blockHeight = 50;
        private char blockName;


        public char getValue()
        {
            return blockName;
        }

        public void setValue(char value)
        {
            this.blockName = value;
        }

        public GamePiece(char value)
        {
            this.blockName = value;
        }
    }
}
