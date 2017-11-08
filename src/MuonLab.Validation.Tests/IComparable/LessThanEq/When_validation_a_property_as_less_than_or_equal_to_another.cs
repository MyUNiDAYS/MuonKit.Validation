using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.IComparable.LessThanEq
{
	
	public class When_validation_a_property_as_less_than_or_equal_to_another
	{
		[Fact]
		public async Task test_1_less_than_or_equal_to_4_returns_true()
		{
			var testClass = new TestClass(1, 4);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public async Task test_4_less_than_or_equal_to_1_returns_false()
		{
			var testClass = new TestClass(4, 1);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("LessThanEq");
			validationReport.Violations.First().Error.Replacements["arg0"].ToString().ShouldEqual("Value2");
		}

		[Fact]
		public async Task test_2_less_than_or_equal_to_2_returns_true()
		{
			var testClass = new TestClass(2, 2);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		private class TestClass
		{
			public int value { get; set; }
			public int Value2 { get; set; }

			public TestClass(int value, int value2)
			{
				this.value = value;
				this.Value2 = value2;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.IsLessThanOrEqualTo(x.Value2));
			}
		}
	}
}