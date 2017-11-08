using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.IComparable.Equality
{
	
	public class When_validating_a_null_property_as_equal_to_another_null_property
	{
		[Fact]
		public async Task should_be_valid()
		{
			var testClass = new TestClass();

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}


		private class TestClass
		{
			public string Value { get; set; }
			public string Value2 { get; set; }

		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Value.IsEqualTo(x.Value2));
			}
		}
	}
}