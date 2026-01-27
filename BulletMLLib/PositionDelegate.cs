using Microsoft.Xna.Framework;

namespace BulletMLLib
{
	/// <summary>
	/// This is a callback method for getting a position
	/// used to break out dependencies
	/// </summary>
	/// <returns>a method to get a position.</returns>
	public delegate Vector2 PositionDelegate();

	/// <summary>
	/// a method to get a float from somewhere
	/// separate from delgates
	/// </summary>
	/// <returns>get a float from somewhere</returns>
	public delegate float FloatDelegate();
}
