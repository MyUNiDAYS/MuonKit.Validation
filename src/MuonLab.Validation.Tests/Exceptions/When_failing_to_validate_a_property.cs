using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.Exceptions
{
	
	public class When_failing_to_validate_a_property
	{
		[Fact]
		public async Task ensure_exception_is_caught_and_reported()
		{
			var testClass = new TestClass();

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			var errorDescriptor = validationReport.Violations.First().Error;
			errorDescriptor.Key.ShouldEqual("ValidationError");
		}
		

		private class TestClass
		{
			public bool? NullableBool { get; set; }
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.NullableBool.Satisfies(b => Throw(), "foo"));
			}

			static bool Throw()
			{
				throw new Exception();
			}
		}
	}
}