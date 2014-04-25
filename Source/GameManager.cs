
namespace BulletMLLib
{
	/// <summary>
	/// This thing manages a few gameplay variables that used by the bulletml lib
	/// </summary>
	public static class GameManager
	{
		//TODO: get rid of this class and move game difficulty in to bullet manager

		//TODO: bullet should store the difficulty when they are fired

		/// <summary>
		/// callback method to get the game difficulty.
		/// You need to set this at the start of the game
		/// </summary>
		static public FloatDelegate GameDifficulty;
	}
}

