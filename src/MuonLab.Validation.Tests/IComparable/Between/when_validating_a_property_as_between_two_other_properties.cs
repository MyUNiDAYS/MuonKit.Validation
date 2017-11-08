using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.IComparable.Between
{
	
	public class when_validating_a_property_as_between_two_other_properties
	{
		[Fact]
		public async Task test_1_between_4_and_6_returns_false()
		{
			var testClass = new TestClass(1, 4, 6);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("Between");
			validationReport.Violations.First().Error.Replacements["arg0"].ToString().ShouldEqual("Value2");
			validationReport.Violations.First().Error.Replacements["arg1"].ToString().ShouldEqual("Value3");
		}

		[Fact]
		public async Task test_4_between_1_and_6_returns_true()
		{
			var testClass = new TestClass(4, 1, 6);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public async Task test_2_between_2_and_2_returns_true()
		{
			var testClass = new TestClass(2, 2, 2);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		private class TestClass
		{
			public int Value { get; }
			public int? Value2 { get; }
			public int? Value3 { get; }

			public TestClass(int value, int value2, int value3)
			{
				this.Value = value;
				this.Value2 = value2;
				this.Value3 = value3;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Value.IsBetween(x.Value2, x.Value3));
			}
		}
	}
}