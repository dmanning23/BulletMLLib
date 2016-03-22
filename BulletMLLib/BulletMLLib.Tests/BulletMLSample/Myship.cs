using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BulletMLSample
{
	public class Myship
	{
		public Vector2 pos;
		float speed = 3;

		public Vector2 Position()
		{
			return pos;
		}

		public void Init()
		{
			pos.X = Game1.graphics.PreferredBackBufferWidth / 2f;
			pos.Y = Game1.graphics.PreferredBackBufferHeight / 2f;
		}

		public void Update()
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Left))
				pos.X -= speed;
			if (Keyboard.GetState().IsKeyDown(Keys.Right))
				pos.X += speed;
			if (Keyboard.GetState().IsKeyDown(Keys.Up))
				pos.Y -= speed;
			if (Keyboard.GetState().IsKeyDown(Keys.Down))
				pos.Y += speed;

		}
	}
}
