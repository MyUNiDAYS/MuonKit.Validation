using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.ReferenceType
{
	public class When_validating_a_property_as_not_null
	{
		[Fact]
		public async Task ensure_not_null_returns_true()
		{
			var testClass = new TestClass(new object());

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public async Task ensure_not_null_returns_false()
		{
			var testClass = new TestClass(null);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("Required");
			validationReport.Violations.Skip(1).First().Error.Key.ShouldEqual("test key");
		}

		sealed class TestClass
		{
			public object Value { get; set; }

			public TestClass(object value)
			{
				this.Value = value;
			}
		}

		sealed class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Value.IsNotNull());
				Ensure(x => x.Value.IsNotNull("test key"));
			}
		}
	}
}