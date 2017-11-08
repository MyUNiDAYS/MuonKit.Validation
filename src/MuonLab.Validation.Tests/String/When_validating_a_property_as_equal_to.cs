using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.String
{
	public class When_validating_a_property_as_equal_to
	{
		[Fact]
		public async Task ensure_mismatch_fail_validation()
		{
			var testClass = new TestClass("different");

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("error");
		}


		[Fact]
		public async Task ensure_match_passes_validation()
		{
			var testClass = new TestClass("HeLlO");

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}


		sealed class TestClass
		{
			public string Value { get; set; }

			public TestClass(string value)
			{
				this.Value = value;
			}
		}

		sealed class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Value.IsEqualTo("hello", StringComparison.InvariantCultureIgnoreCase, "error"));
			}
		}
	}
}