using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.NullProperties
{
	public class when_validating_a_null_property
	{
		[Fact]
		public async Task the_validation_report_should_be_valid()
		{
			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(new TestClass());

			validationReport.IsValid.ShouldBeTrue();
		}

		class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Name.Satisfies(p => true, "should work!"));
			}
		}
	}
}