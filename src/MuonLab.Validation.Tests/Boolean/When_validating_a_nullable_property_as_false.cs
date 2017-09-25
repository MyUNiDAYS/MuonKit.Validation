using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.Boolean
{
	[TestFixture]
	public class When_validating_a_nullable_property_as_false
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public async Task ensure_false_returns_true()
		{
			var testClass = new TestClass(false);

			var validationReport = await this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public async Task ensure_true_returns_false()
		{
			var testClass = new TestClass(true);

			var validationReport = await this.validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("BeFalse");
		}

		private class TestClass
		{
			public bool? Value { get; }

			public TestClass(bool value)
			{
				this.Value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Value.IsFalse());
			}
		}
	}
}