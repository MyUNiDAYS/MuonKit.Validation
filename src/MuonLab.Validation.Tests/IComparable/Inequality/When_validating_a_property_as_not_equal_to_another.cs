using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.IComparable.Inequality
{
	[TestFixture]
	public class When_validating_a_property_as_not_equal_to_another
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public async Task test_1_not_equals_4_returns_true()
		{
			var testClass = new TestClass(1, 4);

			var validationReport = await this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public async Task test_4_not_equals_1_returns_true()
		{
			var testClass = new TestClass(4, 1);

			var validationReport = await this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public async Task test_2_not_equals_2_returns_false()
		{
			var testClass = new TestClass(2, 2);

			var validationReport = await this.validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("NotEqualTo");
			validationReport.Violations.First().Error.Replacements["arg0"].ToString().ShouldEqual("Value2");
		}

		private class TestClass
		{
			public int? Value { get; }
			public int Value2 { get; }

			public TestClass(int value, int value2)
			{
				this.Value = value;
				this.Value2 = value2;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Value.IsNotEqualTo(x.Value2));
			}
		}
	}
}