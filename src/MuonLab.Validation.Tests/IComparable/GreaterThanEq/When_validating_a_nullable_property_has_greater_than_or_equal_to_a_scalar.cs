using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.IComparable.GreaterThanEq
{
	[TestFixture]
	public class When_validating_a_nullable_property_has_greater_than_or_equal_to_a_scalar
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public async Task test_1_greater_than_or_equal_4_returns_false()
		{
			var testClass = new TestClass(1);

			var validationReport = await this.validator.Validate(testClass);
            validationReport.Violations.First().Error.Key.ShouldEqual("GreaterThanEq");
			validationReport.Violations.First().Error.Replacements["arg0"].Value.ShouldEqual("4");
		}

		[Test]
		public async Task test_4_greater_than_or_equal_1_returns_true()
		{
			var testClass = new TestClass(4);

			var validationReport = await this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public async Task test_4_greater_than_or_equal_4_returns_true()
		{
			var testClass = new TestClass(4);

			var validationReport = await this.validator.Validate(testClass);

            Assert.IsTrue(validationReport.IsValid);
		}

		private class TestClass
		{
			public int? Value { get; }

			public TestClass(int value)
			{
				this.Value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Value.IsGreaterThanOrEqualTo(4));
			}
		}
	}
}