using Equationator;
using System;

namespace BulletMLLib
{
	/// <summary>
	/// This is an equation used in BulletML nodes.
	/// This is an easy way to set up the grammar for all our equations.
	/// </summary>
	public class BulletMLEquation : Equation
	{
		/// <summary>
	/// Initializes a new instance of the <see cref="BulletMLEquation"/> class.
	/// Registers rank, rand, and any custom callback functions from the manager.
	/// </summary>
	/// <param name="manager">The bullet manager providing callback functions.</param>
	public BulletMLEquation(IBulletManager manager)
		{
			//make sure the manager is set up, otherwise the user just gets a null reference error with no hint what is missing
			if (null == manager)
			{
				throw new ArgumentNullException("manager", "A BulletMLEquation needs an IBulletManager");
			}
			if (null == manager.GameDifficulty)
			{
				throw new ArgumentException("IBulletManager.GameDifficulty is null, it needs to be set before loading any bullet patterns", "manager");
			}
			if (null == manager.Rand)
			{
				throw new ArgumentException("IBulletManager.Rand is null, it needs to be set before loading any bullet patterns", "manager");
			}
			if (null == manager.CallbackFunctions)
			{
				throw new ArgumentException("IBulletManager.CallbackFunctions is null, it needs to be set (an empty dictionary is fine) before loading any bullet patterns", "manager");
			}

			//add the specific functions we will use for bulletml grammar
			AddFunction("rank", manager.GameDifficulty);
			AddFunction("rand", manager.Rand.NextDouble);

			//Add any additional methods that have been added to this specific bulletml implementation
			foreach (var function in manager.CallbackFunctions)
			{
				AddFunction(function.Key, function.Value);
			}
		}
	}
}

