using System;
using System.Threading.Tasks;

namespace MuonLab.Validation
{
	public abstract class PropertyCondition
	{
		public string ErrorKey { get; protected set; }
	}

	public class PropertyCondition<TValue> : PropertyCondition, ICondition<TValue>
	{
		public Func<TValue, Task<bool>> Condition { get; protected set; }

		public PropertyCondition(Func<TValue, Task<bool>> condition, string errorKey)
		{
			this.Condition = condition;
			this.ErrorKey = errorKey;
		}
	}
}