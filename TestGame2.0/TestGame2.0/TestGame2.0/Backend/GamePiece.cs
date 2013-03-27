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
    class GamePiece
    {
        private const int blockWidth = 50;
        private const int blockHeight = 50;
        private string blockName;

        
        public string getValue(){
            return blockName;
        }

        public void setValue(string value)
        {
            this.blockName = value;
        }

        public GamePiece(string value)
        {
            this.blockName = value;
        }
    }
}
