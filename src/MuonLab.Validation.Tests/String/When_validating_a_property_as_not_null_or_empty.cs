using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.String
{
	
	public class When_validating_a_property_as_not_null_or_empty
	{
		[Fact]
		public async Task ensure_nulls_fail_validation()
		{
			var testClass = new TestClass(null);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);
			validationReport.Violations.First().Error.Key.ShouldEqual("Required");
		}

		[Fact]
		public async Task ensure_empty_string_fail_validation()
		{
			var testClass = new TestClass(string.Empty);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("Required");
		}

		[Fact]
		public async Task ensure_not_null_or_empty_passes_validation()
		{
			var testClass = new TestClass("a");

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}


		private class TestClass
		{
			public string value { get; set; }

			public TestClass(string value)
			{
				this.value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.IsNotNullOrEmpty());
			}
		}
	}
}