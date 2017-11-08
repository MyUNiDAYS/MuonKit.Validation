using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.Boolean
{
	public class When_validating_a_nullable_property_as_false
	{
		class TestClass
		{
			public TestClass(bool value)
			{
				Value = value;
			}
				
			public bool? Value { get; }
		}

		class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Value.IsFalse());
			}
		}

		[Fact]
		public async Task ensure_false_returns_true()
		{
			var testClass = new TestClass(false);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public async Task ensure_true_returns_false()
		{
			var testClass = new TestClass(true);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("BeFalse");
		}
	}
}