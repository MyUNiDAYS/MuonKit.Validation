using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.IComparable.Inequality
{
	public class When_validating_a_property_as_not_equal_to_a_scalar
	{
		[Fact]
		public async Task test_1_not_equals_4_returns_true()
		{
			var testClass = new TestClass(1);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public async Task test_8_not_equals_4_returns_true()
		{
			var testClass = new TestClass(8);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public async Task test_4_not_equals_4_returns_false()
		{
			var testClass = new TestClass(4);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("NotEqualTo");
			validationReport.Violations.First().Error.Replacements["arg0"].ToString().ShouldEqual("4");
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
				Ensure(x => x.Value.IsNotEqualTo(4));
			}
		}
	}
}