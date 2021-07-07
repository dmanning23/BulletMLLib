using Equationator;

namespace BulletMLLib
{
	/// <summary>
	/// This is an equation used in BulletML nodes.
	/// This is an eays way to set up the grammar for all our equations.
	/// </summary>
	public class BulletMLEquation : Equation
	{
		public BulletMLEquation(IBulletManager manager)
		{
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

