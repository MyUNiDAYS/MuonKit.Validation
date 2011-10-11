namespace MuonLab.Validation
{
	internal sealed class ChildValidationCondition<TValue> : ICondition<TValue>
	{
		public IValidator<TValue> Validator { get; protected set; }

		public ChildValidationCondition(IValidator<TValue> validator)
		{
			this.Validator = validator;
		}
	}
}