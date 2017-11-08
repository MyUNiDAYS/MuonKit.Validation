using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.IComparable.Inequality
{
	public class When_validating_a_property_as_not_equal_to_another
	{
		[Fact]
		public async Task test_1_not_equals_4_returns_true()
		{
			var testClass = new TestClass(1, 4);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public async Task test_4_not_equals_1_returns_true()
		{
			var testClass = new TestClass(4, 1);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public async Task test_2_not_equals_2_returns_false()
		{
			var testClass = new TestClass(2, 2);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("NotEqualTo");
			validationReport.Violations.First().Error.Replacements["arg0"].ToString().ShouldEqual("Value2");
		}

		private class TestClass
		{
			public int? Value { get; }
			public int Value2 { get; }

			public TestClass(int value, int value2)
			{
				this.Value = value;
				this.Value2 = value2;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Value.IsNotEqualTo(x.Value2));
			}
		}
	}
}