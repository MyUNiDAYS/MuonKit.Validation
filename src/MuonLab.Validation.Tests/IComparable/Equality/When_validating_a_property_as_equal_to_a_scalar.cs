using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.IComparable.Equality
{
	[TestFixture]
	public class When_validating_a_property_as_equal_to_a_scalar
	{
		TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public async Task test_1_equals_4_returns_false()
		{
			var testClass = new TestClass(1);

			var validationReport = await this.validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("EqualTo");
			validationReport.Violations.First().Error.Replacements["arg0"].ToString().ShouldEqual("4");
		}

		[Test]
		public async Task test_8_equals_4_returns_false()
		{
			var testClass = new TestClass(8);

			var validationReport = await this.validator.Validate(testClass);

			Assert.IsFalse(validationReport.IsValid);
		}

		[Test]
		public async Task test_4_equals_4_returns_true()
		{
			var testClass = new TestClass(4);

			var validationReport = await this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		private class TestClass
		{
			public int Value { get; }

			public TestClass(int value)
			{
				this.Value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Value.IsEqualTo(4));
			}
		}
	}
}