using BulletMLLib;
using Microsoft.Xna.Framework;

namespace BulletMLSample
{
	public class Mover : Bullet
	{
		#region Members

		public Vector2 pos;

		#endregion //Members

		#region Properties

		public override float X
		{
			get { return pos.X; }
			set { pos.X = value; }
		}
		
		public override float Y
		{
			get { return pos.Y; }
			set { pos.Y = value; }
		}

		public bool Used { get; set; }
		
		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletMLSample.Mover"/> class.
		/// </summary>
		/// <param name="myBulletManager">My bullet manager.</param>
		public Mover(IBulletManager myBulletManager) : base(myBulletManager)
		{
		}

		public void Init()
		{
			Used = true;
		}

		public override void PostUpdate()
		{
			if (X < 0 || X > Game1.graphics.PreferredBackBufferWidth || Y < 0 || Y > Game1.graphics.PreferredBackBufferHeight)
			{
				Used = false;
			}
		}

		#endregion //Methods
	}
}
