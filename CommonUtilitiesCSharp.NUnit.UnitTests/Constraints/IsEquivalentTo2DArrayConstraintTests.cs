using CommonUtilitiesCSharp.NUnit.Constraints;

namespace CommonUtilitiesCSharp.NUnit.UnitTests.Constraints
{
    public class IsEquivalentTo2DArrayConstraintTests
    {
        #region Constraint
        [Test]
        public void Constraint_ReturnsSuccess_ForEquivalentArrays()
        {
            var expected = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var actual = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            var constraint = new IsEquivalentTo2DArrayConstraint<int>(expected);
            var constraintResult = constraint.ApplyTo(actual);
            
            Assert.Multiple(() =>
            {
                Assert.That(actual, constraint);
                Assert.That(constraintResult.IsSuccess);
            });
        }

        [Test]
        public void Constraint_ReturnsFailure_ForDifferentArrays()
        {
            var expected = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var actual = new int[,] { { 1, 2, 9 }, { 4, 5, 6 } };

            var constraint = new IsEquivalentTo2DArrayConstraint<int>(expected);
            var constraintResult = constraint.ApplyTo(actual);

            Assert.That(constraintResult.IsSuccess, Is.False);
        }
        #endregion Constraint

        #region Description
        [Test]
        public void Description_ShowsExpected_ForDifferentArrays()
        {
            var expected = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var actual = new int[,] { { 1, 2, 9 }, { 4, 5, 6 } };

            var constraint = new IsEquivalentTo2DArrayConstraint<int>(expected);
            var constraintResult = constraint.ApplyTo(actual);

            Assert.That(constraintResult.Description, Is.EqualTo($"equivalent to {MsgUtils.FormatArray(expected)}"));
        }
        #endregion Description
    }
}
