using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace M_Topic_1___Monogame_Recap
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D lightningTexture, rainDropTexture, rainCloudTexture;
        Rectangle lightningRect;
        List<Rectangle> rainDrops = new List<Rectangle>();
        List<int> x = new List<int>(), y = new List<int>();
        Random random = new Random();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Window.Title = "Insane Storm";

            _graphics.PreferredBackBufferHeight = 500;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.ApplyChanges();

            base.Initialize();

            for (int i = 0; i < 100; i++)
            {
                x.Add(random.Next(0, 750));
                y.Add(random.Next(0, 500));
                rainDrops.Add(new Rectangle(x[i], y[i], 50, 40));
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            rainDropTexture = Content.Load<Texture2D>("raindrop");
            rainCloudTexture = Content.Load<Texture2D>("rainclouds");
            lightningTexture = Content.Load<Texture2D>("lightning_bolt");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = 0; i < rainDrops.Count; i++)
            {
                y[i] += 5;
                x[i] -= 7;

                if (y[i] > _graphics.PreferredBackBufferHeight)
                {
                    y[i] -= _graphics.PreferredBackBufferHeight + rainDrops[i].Height;
                }

                if (x[i] > _graphics.PreferredBackBufferWidth)
                {
                    x[i] -= _graphics.PreferredBackBufferWidth + rainDrops[i].Width;
                }
                else if (x[i] < 0)
                {
                    x[i] += _graphics.PreferredBackBufferWidth;
                }

                rainDrops[i] = new Rectangle(x[i], y[i], 50, 40);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _spriteBatch.Draw(rainCloudTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            for (int i = 0; i < rainDrops.Count; i++)
                _spriteBatch.Draw(rainDropTexture, rainDrops[i], null, Color.White, 1f, new Vector2(0, 0), SpriteEffects.None, 1f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}