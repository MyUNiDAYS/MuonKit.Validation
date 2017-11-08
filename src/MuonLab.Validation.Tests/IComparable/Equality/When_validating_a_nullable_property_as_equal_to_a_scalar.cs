using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.IComparable.Equality
{
	
	public class When_validating_a_nullable_property_as_equal_to_a_scalar
	{
		[Fact]
		public async Task test_1_equals_4_returns_false()
		{
			var testClass = new TestClass(1);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("EqualTo");
			validationReport.Violations.First().Error.Replacements["arg0"].ToString().ShouldEqual("4");
		}

		[Fact]
		public async Task test_8_equals_4_returns_false()
		{
			var testClass = new TestClass(8);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeFalse();
		}

		[Fact]
		public async Task test_4_equals_4_returns_true()
		{
			var testClass = new TestClass(4);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		private class TestClass
		{
			public int? value { get; set; }

			public TestClass(int value)
			{
				this.value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.IsEqualTo(4));
			}
		}
	}
}