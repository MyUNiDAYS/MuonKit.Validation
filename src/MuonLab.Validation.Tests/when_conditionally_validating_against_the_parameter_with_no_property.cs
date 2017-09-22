using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests
{
	[TestFixture]
	public class when_conditionally_validating_against_the_parameter_with_no_property
	{
		private TestValidator validator;
		private ValidationReport report;

		[SetUp]
		public async Task SetUp()
		{
			this.validator = new TestValidator();
			this.report = await this.validator.Validate(new TestClass());
		}

		[Test]
		public void the_validation_report_should_be_invalid()
		{
			report.IsValid.ShouldBeFalse();
		}

		public class TestValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				When(x => x.Satisfies(p => true, ""), () => 
					Ensure(x => x.Name.IsNotNullOrEmpty()));
			}
		}
	}
}