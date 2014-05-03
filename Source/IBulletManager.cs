using Microsoft.Xna.Framework;

namespace BulletMLLib
{
	/// <summary>
	/// This is the interface that outisde assemblies will use to manage bullets... mostly for creating/destroying them
	/// </summary>
	public interface IBulletManager
	{
		#region Methods

		/// <summary>
		/// a mathod to get current position of the player
		/// This is used to target bullets at that position
		/// </summary>
		/// <returns>The position to aim the bullet at</returns>
		/// <param name="targettedBullet">the bullet we are getting a target for</param>
		Vector2 PlayerPosition(Bullet targettedBullet);

		/// <summary>
		/// A bullet is done being used, do something to get rid of it.
		/// </summary>
		/// <param name="deadBullet">the Dead bullet.</param>
		void RemoveBullet(Bullet deadBullet);

		/// <summary>
		/// Create a new bullet.
		/// </summary>
		/// <returns>A shiny new bullet</returns>
		Bullet CreateBullet();

		/// <summary>
		/// Create a new bullet that will be initialized from a top level node.
		/// These are usually special bullets that dont need to be drawn or kept around after they finish tasks etc.
		/// </summary>
		/// <returns>A shiny new top-level bullet</returns>
		Bullet CreateTopBullet();

		#endregion //Methods
	}
}
