using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MuonLab.Validation.Tests.Nullable
{
	
	public class When_validating_a_nullable_property
	{
		[Fact]
		public async Task ensure_false_returns_false()
		{
			var testClass = new TestClass(false);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("BeTrue");
		}

		[Fact]
		public async Task ensure_true_returns_true()
		{
			var testClass = new TestClass(true);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public async Task ensure_null_returns_true()
		{
			var testClass = new TestClass(null);

			var validator = new TestClassValidator();
			var validationReport = await validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		private class TestClass
		{
			public bool? NullableBool { get; set; }

			public TestClass(bool? value)
			{
				this.NullableBool = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				When(x => x.NullableBool.HasValue(), () => Ensure(x => x.NullableBool.Value.IsTrue()));
			}
		}
	}
}