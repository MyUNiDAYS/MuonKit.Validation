using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.IComparable
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
		public void test_1_equals_4_returns_false()
		{
			var testClass = new TestClass(1);

			var validationReport = Task.Run(() => this.validator.Validate(testClass)).Result;

			validationReport.Violations.First().Error.Key.ShouldEqual("EqualTo");
			validationReport.Violations.First().Error.Replacements["arg0"].Value.ShouldEqual("4");
		}

		[Test]
		public void test_8_equals_4_returns_false()
		{
			var testClass = new TestClass(8);

			var validationReport = Task.Run(() => this.validator.Validate(testClass)).Result;

			Assert.IsFalse(validationReport.IsValid);
		}

		[Test]
		public void test_4_equals_4_returns_true()
		{
			var testClass = new TestClass(4);

			var validationReport = Task.Run(() => this.validator.Validate(testClass)).Result;

			Assert.IsTrue(validationReport.IsValid);
		}

		private class TestClass
		{
			public int value { get; set; }

			public TestClass(int value)
			{
				this.value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.IsEqualTo(4));
			}
		}
	}
}