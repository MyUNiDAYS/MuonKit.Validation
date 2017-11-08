using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.IComparable.LessThan
{

	public class When_validating_a_property_as_less_than_a_nullable_scalar
	{
		[Fact]
		public async Task test_1_less_than_4_returns_true()
		{
			var testClass = new TestClass(1);
			
			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public async Task test_8_less_than_4_returns_false()
		{
			var testClass = new TestClass(4);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			var violations = validationReport.Violations.ToArray();

			validationReport.Violations.First().Error.Key.ShouldEqual("LessThan");
			validationReport.Violations.First().Error.Replacements["arg0"].ToString().ShouldEqual("4");
		}

		[Fact]
		public async Task test_4_less_than_4_returns_false()
		{
			var testClass = new TestClass(4);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("LessThan");
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
				Ensure(x => x.Value.IsLessThan((int?)4));
			}
		}
	}
}