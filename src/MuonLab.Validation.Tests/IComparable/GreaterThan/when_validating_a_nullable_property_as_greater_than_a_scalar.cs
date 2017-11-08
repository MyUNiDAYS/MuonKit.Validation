using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.IComparable.GreaterThan
{
	
	public class when_validating_a_nullable_property_as_greater_than_a_scalar
	{
		[Fact]
		public async Task test_1_greater_than_4_returns_false()
		{
			var testClass = new TestClass(1);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("GreaterThan");
			validationReport.Violations.First().Error.Replacements["arg0"].ToString().ShouldEqual("4");
		}

		[Fact]
		public async Task test_8_greater_than_4_returns_true()
		{
			var testClass = new TestClass(8);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public async Task test_4_greater_than_4_returns_false()
		{
			var testClass = new TestClass(4);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("GreaterThan");
			validationReport.Violations.First().Error.Replacements["arg0"].ToString().ShouldEqual("4");
		}

		private class TestClass
		{
			public int? Value { get; }
			public TestClass(int value)
			{
				this.Value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass> {
			protected override void Rules()
			{
				Ensure(x => x.Value.IsGreaterThan(4));
			}
		}
	}
}