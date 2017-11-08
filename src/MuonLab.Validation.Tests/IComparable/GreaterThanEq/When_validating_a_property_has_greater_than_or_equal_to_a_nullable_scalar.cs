using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.IComparable.GreaterThanEq
{
	
	public class When_validating_a_property_has_greater_than_or_equal_to_a_nullable_scalar
	{
		[Fact]
		public async Task test_1_greater_than_or_equal_4_returns_false()
		{
			var testClass = new TestClass(1);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("GreaterThanEq");
			validationReport.Violations.First().Error.Replacements["arg0"].ToString().ShouldEqual("4");
		}

		[Fact]
		public async Task test_4_greater_than_or_equal_1_returns_true()
		{
			var testClass = new TestClass(4);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public async Task test_4_greater_than_or_equal_4_returns_true()
		{
			var testClass = new TestClass(4);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		private class TestClass
		{
			public int Value { get; }

			public TestClass(int value)
			{
				this.Value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Value.IsGreaterThanOrEqualTo((int?)4));
			}
		}
	}
}