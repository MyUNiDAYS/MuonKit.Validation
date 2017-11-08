using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.String
{
	public class When_validating_a_property_as_not_equal_to
	{
		[Fact]
		public async Task ensure_mismatch_fail_validation()
		{
			var testClass = new TestClass("HeLlo");

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("error");
		}

		[Fact]
		public async Task ensure_match_passes_validation()
		{
			var testClass = new TestClass("different");

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public async Task ensure_one_null_value_pass_validation()
		{
			var testClass = new TestClass(null);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public async Task ensure_matching_null_values_fail_validation()
		{
			var testClass = new TestClass(null);

			var validatorWithTestClassValidatorWithNull = new TestClassValidatorWithNullValueParameter();
			var validationReport = await validatorWithTestClassValidatorWithNull.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("error");
		}

		sealed class TestClass
		{
			public string value { get; set; }

			public TestClass(string value)
			{
				this.value = value;
			}
		}

		sealed class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.IsNotEqualTo("hello", StringComparison.InvariantCultureIgnoreCase, "error"));
			}
		}

		sealed class TestClassValidatorWithNullValueParameter : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.IsNotEqualTo(null, StringComparison.InvariantCultureIgnoreCase, "error"));
			}
		}
	}
}
