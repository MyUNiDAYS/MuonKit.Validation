using System.Threading.Tasks;
using NUnit.Framework;

namespace MuonLab.Validation.Tests
{
	[TestFixture]
	public class when_validating_with_a_conditional_predicate
	{
		[Test]
		public async Task when_a_condition_is_false_the_validation_rule_should_not_be_run()
		{
			var testClass = new TestClass(2, 2);

			var validationReport = await new ConditionalValidator(false).Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public async Task when_a_condition_is_true_the_validation_rule_should_be_run()
		{
			var testClass = new TestClass(1, 2);

			var validationReport = await new ConditionalValidator(true).Validate(testClass);

			Assert.IsFalse(validationReport.IsValid);
		}

		private class TestClass
		{
			public int Value { get; set; }
			public int Value2 { get; set; }

			public TestClass(int value, int value2)
			{
				this.Value = value;
				this.Value2 = value2;
			}
		}

		private class ConditionalValidator : Validator<TestClass>
		{
			private readonly bool run;

			public ConditionalValidator(bool run)
			{
				this.run = run;
			}

			protected override void Rules()
			{
				When(() => run, () => Ensure(x => x.Value2.IsEqualTo(3)));
			}
		}
	}
}