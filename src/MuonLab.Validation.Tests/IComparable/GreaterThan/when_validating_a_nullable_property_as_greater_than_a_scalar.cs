using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.IComparable.GreaterThan
{
	[TestFixture]
	public class when_validating_a_nullable_property_as_greater_than_a_scalar
	{
		TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public async Task test_1_greater_than_4_returns_false()
		{
			var testClass = new TestClass(1);

			var validationReport = await this.validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("GreaterThan");
			validationReport.Violations.First().Error.Replacements["arg0"].Value.ShouldEqual("4");
		}

		[Test]
		public async Task test_8_greater_than_4_returns_true()
		{
			var testClass = new TestClass(8);

			var validationReport = await this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public async Task test_4_greater_than_4_returns_false()
		{
			var testClass = new TestClass(4);

			var validationReport = await this.validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("GreaterThan");
			validationReport.Violations.First().Error.Replacements["arg0"].Value.ShouldEqual("4");
		}

		private class TestClass
		{
			public int? Value { get; }
			public TestClass(int value)
			{
				this.Value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass> {
			protected override void Rules()
			{
				Ensure(x => x.Value.IsGreaterThan(4));
			}
		}
	}
}