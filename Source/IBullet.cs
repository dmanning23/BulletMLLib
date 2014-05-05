using System.Collections.Generic;

namespace BulletMLLib
{
	/// <summary>
	/// If you want a thing to be bullet-like, you can inherit from this interface.
	/// Useful if you have an object that encapsulates a Bullet.
	/// </summary>
	public interface IBullet
	{
		/// <summary>
		/// A list of tasks that will define this bullets behavior
		/// </summary>
		List<BulletMLTask> Tasks { get; }

		/// <summary>
		/// Abstract property to get the X location of this bullet.
		/// measured in pixels from upper left
		/// </summary>
		/// <value>The horizontrla position.</value>
		float X { get; set; }

		/// <summary>
		/// Gets or sets the y parameter of the location
		/// measured in pixels from upper left
		/// </summary>
		/// <value>The vertical position.</value>
		float Y { get; set; }

		/// <summary>
		/// Gets or sets the speed
		/// </summary>
		/// <value>The speed, in pixels/frame</value>
		float Speed { get; set; }

		/// <summary>
		/// Gets or sets the direction.
		/// </summary>
		/// <value>The direction in radians.</value>
		float Direction { get; set; }

		/// <summary>
		/// Initialize this bullet with a top level node
		/// </summary>
		/// <param name="rootNode">This is a top level node... find the first "top" node and use it to define this bullet</param>
		void InitTopNode(BulletMLNode rootNode);

		/// <summary>
		/// This bullet is fired from another bullet, initialize it from the node that fired it
		/// </summary>
		/// <param name="subNode">Sub node that defines this bullet</param>
		void InitNode(BulletMLNode subNode);
	}
}
