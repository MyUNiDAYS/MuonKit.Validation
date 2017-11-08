using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.Boolean
{
	
	public class When_validating_a_property_as_true
	{
		[Fact]
		public async Task ensure_true_returns_true()
		{
			var testClass = new TestClass(true);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public async Task ensure_false_returns_false()
		{
			var testClass = new TestClass(false);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			var violations = validationReport.Violations.ToArray();

			violations[0].Error.Key.ShouldEqual("BeTrue");
		}

		private class TestClass
		{
			public bool value { get; set; }

			public TestClass(bool value)
			{
				this.value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.IsTrue());
			}
		}
	}
}