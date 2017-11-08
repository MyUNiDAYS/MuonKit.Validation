using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.IComparable.Equality
{
	
	public class When_validating_a_property_as_equal_to_another
	{
		[Fact]
		public async Task test_1_equals_4_returns_false()
		{
			var testClass = new TestClass(1, 4);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("EqualTo");
			validationReport.Violations.First().Error.Replacements["arg0"].ToString().ShouldEqual("Value2");
		}

		[Fact]
		public async Task test_4_equals_1_returns_false()
		{
			var testClass = new TestClass(4, 1);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			var violations = validationReport.Violations.ToArray();

			validationReport.Violations.First().Error.Key.ShouldEqual("EqualTo");
			validationReport.Violations.First().Error.Replacements["arg0"].ToString().ShouldEqual("Value2");
		}

		[Fact]
		public async Task test_2_equals_2_returns_true()
		{
			var testClass = new TestClass(2, 2);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		private class TestClass
		{
			public int Value { get; }
			public int? Value2 { get; }

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
				// int - int
				Ensure(x => x.Value.IsEqualTo(x.Value));
				// int - int?
				Ensure(x => x.Value.IsEqualTo(x.Value2));
				// int? - int
				Ensure(x => x.Value2.IsEqualTo(x.Value));
				// int? - int?
				Ensure(x => x.Value2.IsEqualTo(x.Value2));
			}
		}
	}
}