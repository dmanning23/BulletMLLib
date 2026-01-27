using BulletMLLib;
//using FontBuddyLib;
//using GameTimer;
//using HadoukInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BulletMLSample
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	class Game1 : Game
	{
		#region Members

		static public GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Texture2D texture;
		static public Myship myship;
		Mover mover;

		MoverManager _moverManager;

		//GameClock _clock;

		//InputState _inputState;
		//InputWrapper _inputWrapper;

		float _Rank = 0.0f;

		//private FontBuddy _text = new FontBuddy();

		/// <summary>
		/// A list of all the bulletml samples we have loaded
		/// </summary>
		private List<BulletPattern> _myPatterns = new List<BulletPattern>();

		/// <summary>
		/// The names of all the bulletml patterns that are loaded, stored so we can display what is being fired
		/// </summary>
		private List<string> _patternNames = new List<string>();

		/// <summary>
		/// The current Bullet ML pattern to use to shoot bullets
		/// </summary>
		private int _CurrentPattern = 0;

		#endregion //Members

		#region Methods

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);

			Content.RootDirectory = "Content";
			myship = new Myship();

			//_clock = new GameClock();
			//_inputState = new InputState();
			//_inputWrapper = new InputWrapper(new ControllerWrapper(PlayerIndex.One, true), _clock.GetCurrentTime);
			//_inputWrapper.Controller.UseKeyboard = true;
			_moverManager = new MoverManager(myship.Position);

			_moverManager.Difficulty = _Rank;
		}

		protected override void Initialize()
		{
			myship.Init();

			//_clock.Start();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			//_text.LoadContent(Content, "ArialBlack14");

			texture = Content.Load<Texture2D>("Sprites\\bullet");

			//Get all the xml files in the Content\\Samples directory
			foreach (var source in Directory.GetFiles("Content\\Samples", "*.xml"))
			{
				//store the name
				_patternNames.Add(source);

				//load the pattern
				BulletPattern pattern = new BulletPattern(_moverManager);
				pattern.ParseXML(source);
				_myPatterns.Add(pattern);
			}


			AddBullet();
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				this.Exit();
			}

			////update the timer
			//_clock.Update(gameTime);

			////update the input
			//_inputState.Update();
			//_inputWrapper.Update(_inputState, false);

			////check input to increment/decrement the current bullet pattern
			//if (_inputWrapper.Controller.CheckKeystroke(EKeystroke.LShoulder))
			//{
			//	//decrement the pattern
			//	if (0 >= _CurrentPattern)
			//	{
			//		//if it is at the beginning, move to the end
			//		_CurrentPattern = _myPatterns.Count - 1;
			//	}
			//	else
			//	{
			//		_CurrentPattern--;
			//	}

			//	AddBullet();
			//}
			//else if (_inputWrapper.Controller.CheckKeystroke(EKeystroke.RShoulder))
			//{
			//	//increment the pattern
			//	if ((_myPatterns.Count - 1) <= _CurrentPattern)
			//	{
			//		//if it is at the beginning, move to the end
			//		_CurrentPattern = 0;
			//	}
			//	else
			//	{
			//		_CurrentPattern++;
			//	}

			//	AddBullet();
			//}

			////reset the bullet pattern
			//if (_inputWrapper.Controller.CheckKeystroke(EKeystroke.A))
			//{
			//	AddBullet();
			//}

			////increase/decrease the rank
			//if (_inputWrapper.Controller.CheckKeystroke(EKeystroke.LShoulder))
			//{
			//	if (_Rank > 0.0f)
			//	{
			//		_Rank -= 0.1f;
			//	}

			//	if (_Rank < 0.0f)
			//	{
			//		_Rank = 0.0f;
			//	}
			//}
			//else if (_inputWrapper.Controller.CheckKeystroke(EKeystroke.RShoulder))
			//{
			//	if (_Rank < 1.0f)
			//	{
			//		_Rank += 0.1f;
			//	}

			//	if (_Rank > 1.0f)
			//	{
			//		_Rank = 1.0f;
			//	}
			//}

			////if y is held down, do some slowdown
			//if (_inputWrapper.Controller.CheckKeystroke(EKeystroke.Y))
			//{
			//	_moverManager.TimeSpeed = 0.5f;
			//}
			//else
			//{
			//	_moverManager.TimeSpeed = 1.0f;
			//}

			////if b is held down, make it bigger
			//if (_inputWrapper.Controller.CheckKeystroke(EKeystroke.LTrigger))
			//{
			//	_moverManager.Scale -= 0.1f;
			//}
			//else if (_inputWrapper.Controller.CheckKeystroke(EKeystroke.RTrigger))
			//{
			//	_moverManager.Scale += 0.1f;
			//}

			_moverManager.Update();

			myship.Update();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			Vector2 position = Vector2.Zero;

			////say what pattern we are shooting
			//_text.Write(_patternNames[_CurrentPattern], position, Justify.Left, 1.0f, Color.White, spriteBatch, _clock);
			//position.Y += _text.Font.MeasureString("test").Y;

			////how many bullets on the screen
			//_text.Write(_moverManager.movers.Count.ToString(), position, Justify.Left, 1.0f, Color.White, spriteBatch, _clock);
			//position.Y += _text.Font.MeasureString("test").Y;

			////the current rank
			//StringBuilder rankText = new StringBuilder();
			//rankText.Append("Rank: ");
			//rankText.Append(((int)(_Rank * 10)).ToString());
			//_text.Write(rankText.ToString(), position, Justify.Left, 1.0f, Color.White, spriteBatch, _clock);
			//position.Y += _text.Font.MeasureString("test").Y;

			////the current time speed
			//rankText = new StringBuilder();
			//rankText.Append("Time Speed: ");
			//rankText.Append(_moverManager.TimeSpeed.ToString());
			//_text.Write(rankText.ToString(), position, Justify.Left, 1.0f, Color.White, spriteBatch, _clock);
			//position.Y += _text.Font.MeasureString("test").Y;

			////the current scale
			//rankText = new StringBuilder();
			//rankText.Append("Scale: ");
			//rankText.Append(_moverManager.Scale.ToString());
			//_text.Write(rankText.ToString(), position, Justify.Left, 1.0f, Color.White, spriteBatch, _clock);
			//position.Y += _text.Font.MeasureString("test").Y;

			foreach (Mover mover in _moverManager.movers)
				spriteBatch.Draw(texture, mover.pos, Color.Black);

			spriteBatch.Draw(texture, myship.pos, Color.Black);

			spriteBatch.End();

			base.Draw(gameTime);
		}

		private void AddBullet()
		{
			//clear out all the bulelts
			_moverManager.Clear();

			//add a new bullet in the center of the screen
			mover = (Mover)_moverManager.CreateTopBullet();
			mover.pos = new Vector2(graphics.PreferredBackBufferWidth / 2f, graphics.PreferredBackBufferHeight / 2f);
			mover.InitTopNode(_myPatterns[_CurrentPattern].RootNode);
		}

		#endregion //Methods
	}
}
