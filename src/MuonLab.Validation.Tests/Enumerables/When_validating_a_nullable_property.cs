using System.Collections;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.Enumerables
{
	
	public class when_validating_an_enumerable_contains_elements
	{
		[Fact]
		public async Task an_empty_list_should_be_false()
		{
			var testClass = new TestClass();

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeFalse();
		}


		[Fact]
		public async Task an_non_empty_list_should_be_true()
		{
			var testClass = new TestClass
								{
									List = new[] { "an item" }
								};

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		private class TestClass
		{
			public IEnumerable List { get; set; }
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.List.ContainsElements());
			}
		}
	}
}