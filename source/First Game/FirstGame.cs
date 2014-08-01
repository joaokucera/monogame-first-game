#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace First_Game
{
    /// <summary>
    /// Classe principal do jogo.
    /// </summary>
    public class FirstGame : Game
    {
        /// <summary>
        /// Manipula a configuração e o gerenciamento do dispositivo gráfico.
        /// </summary>
        private GraphicsDeviceManager graphics;
        /// <summary>
        /// Permite que um grupo de sprites seja desenhado usando as mesmas configurações.
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Textura do personagem.
        /// </summary>
        private Texture2D personTexture;
        /// <summary>
        /// Posição do personagem.
        /// </summary>
        private Vector2 personPosition = Vector2.Zero;
        /// <summary>
        /// Velocidade de movimentação do personagem.
        /// </summary>
        private Vector2 personSpeed = new Vector2(50f, 50f);

        /// <summary>
        /// Contrutor (início da classe).
        /// </summary>
        public FirstGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Initialize será chamado somente no início do jogo para iniciar as variáveis.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent será chamado somente no início do jogo para carregar os assets.
        /// </summary>
        protected override void LoadContent()
        {
            // Criando um novo SpriteBatch que será utilizado para desenhar texturas.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Carrega parao jogo a textura do personagem.
            personTexture = Content.Load<Texture2D>("Person");
        }

        /// <summary>
        /// UnloadContent será chamado somente uma vez no jogo para descarregar os assets.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Executação da lógica e atualização dos assets no mundo do jogo.
        /// Verificação de colisões, reunindo entradas de comandos e reprodução de áudio.
        /// </summary>
        /// <param name="gameTime">Tempo de jogo.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Incrementa a movimentação do personagem.
            personPosition += personSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Tamanho máximos e mínimo de largura da tela parao personagem permanecer dentro dela.
            int xMax = graphics.GraphicsDevice.Viewport.Width - personTexture.Width;
            int xMin = 0;

            // Tamanho máximos e mínimo de altura da tela parao personagem permanecer dentro dela.
            int yMax = graphics.GraphicsDevice.Viewport.Height - personTexture.Height;
            int yMin = 0;

            if (personPosition.X > xMax) // Controlando o personagem no canto direito do cenário.
            {
                personSpeed.X *= -1;
                personPosition.X = xMax;
            }
            else if (personPosition.X < xMin) // Controlando o personagem no canto esquerdo do cenário.
            {
                personSpeed.X *= -1;
                personPosition.X = xMin;
            }

            if (personPosition.Y > yMax) // Controlando o personagem no canto superior do cenário.
            {
                personSpeed.Y *= -1;
                personPosition.Y = yMax;
            }
            else if (personPosition.Y < yMin) // Controlando o personagem no canto inferior do cenário.
            {
                personSpeed.Y *= -1;
                personPosition.Y = yMin;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Executação dos desenhos dos assets no mundo do jogo.
        /// </summary>
        /// <param name="gameTime">Tempo de jogo.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Comando para iniciar os desenhos.
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            // Desenhando a textura do personagem.
            spriteBatch.Draw(personTexture, personPosition, Color.White);
            // Comando para finalizar os desenhos.
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
