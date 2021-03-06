﻿using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame2._0
{
    class Block : Sprite
    {
        const int MAX_DISTANCE = 550;

        Vector2 mStartPosition;
        Vector2 mSpeed;
        Vector2 mDirection;

        public Texture2D Texture;
        public new Vector2 Position = Vector2.Zero;
        public Color Color = Color.White;
        public new Vector2 Origin;
        public float Rotation = 0f;
        public new float Scale = 1f;
        public new SpriteEffects SpriteEffect;
        public Rectangle[] Rectangles;
        protected int FrameIndex = 0;

        /// <summary>
        /// Block constructor.
        /// </summary>
        /// <param name="Texture">Texture to use</param>
        /// <param name="frames"></param>
        public Block(Texture2D Texture, int frames)
        {
            this.Texture = Texture;
            int width = 50;//Texture.Width / frames;
            int height = 50;
            int j = 0;
            Rectangles = new Rectangle[frames * 5];
            //first row (a-f)
            for (int i = 0; i < frames; i++)
            {
                Rectangles[i] = new Rectangle(
                    j * width, 0, width, height);
                j++;
            }
            //second row (g-l)
            j = 0;
            for (int i = 6; i < frames * 2; i++)
            {
                Rectangles[i] = new Rectangle(
                    j * width, 50, width, height);
                j++;
            }
            //third row (m-r)
            j = 0;
            for (int i = 12; i < frames * 3; i++)
            {
                Rectangles[i] = new Rectangle(
                    j * width, 100, width, height);
                j++;
            }
            //fourth row (s-x)
            j = 0;
            for (int i = 18; i < frames * 4; i++)
            {
                Rectangles[i] = new Rectangle(
                    j * width, 150, width, height);
                j++;
            }
            //fifth row (y-z)
            j = 0;
            for (int i = 24; i < 30; i++)
            {
                Rectangles[i] = new Rectangle(
                    j * width, 200, width, height);
                j++;
            }
        }

        /// <summary>
        /// Sets block to use.
        /// </summary>
        /// <param name="frame"></param>
        public void SetFrame(int frame)
        {
            if (frame < Rectangles.Length)
                FrameIndex = frame;
        }
        /// <summary>
        /// Updates the block position when the it's fired
        /// </summary>
        /// <param name="theGameTime"></param>
        public void Update(GameTime theGameTime)
        {
            base.Update(theGameTime, mSpeed, mDirection);
        }

        /// <summary>
        ///  Draws the sprite.
        /// </summary>
        /// <param name="theSpriteBatch"></param>
        public override void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(Texture, Position, Rectangles[FrameIndex],
                Color, Rotation, Origin, Scale, SpriteEffect, 0f);
        }

        /// <summary>
        /// Sets position
        /// </summary>
        /// <param name="thePosition">New Position</param>
        public void setPosition(Vector2 thePosition)
        {
            Position = thePosition;
        }
    }
}
