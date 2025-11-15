using CommonUtilitiesCSharp.NUnit.Constraints;
using NUnit.Framework.Constraints;

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

            Assert.Multiple(() =>
            {
                Assert.That(actual, new NotConstraint(constraint));
                Assert.That(constraintResult.IsSuccess, Is.False);
            });
        }

        [Test]
        public void Constraint_ReturnsSuccess_ForNullArrays()
        {
            int[,] expected = null!;
            int[,] actual = null!;

            var constraint = new IsEquivalentTo2DArrayConstraint<int>(expected);
            var constraintResult = constraint.ApplyTo(actual);

            Assert.Multiple(() =>
            {
                Assert.That(actual, constraint);
                Assert.That(constraintResult.IsSuccess);
            });
        }

        [Test]
        public void Constraint_ReturnsFailure_ForNullArgument()
        {
            int[,] expected = null!;
            var actual = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            var constraint = new IsEquivalentTo2DArrayConstraint<int>(expected);
            var constraintResult = constraint.ApplyTo(actual);

            Assert.Multiple(() =>
            {
                Assert.That(actual, new NotConstraint(constraint));
                Assert.That(constraintResult.IsSuccess, Is.False);
            });
        }

        [Test]
        public void Constraint_ReturnsFailure_ForInvalidType()
        {
            var expected = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var actual = new string[,] { { "1", "2", "3" }, { "4", "5", "6" } };

            var constraint = new IsEquivalentTo2DArrayConstraint<int>(expected);
            var constraintResult = constraint.ApplyTo(actual);

            Assert.Multiple(() =>
            {
                Assert.That(actual, new NotConstraint(constraint));
                Assert.That(constraintResult.IsSuccess, Is.False);
            });
        }
        #endregion Constraint

        #region DisplayName
        [Test]
        public void DisplayName_ReturnsExpected()
        {
            var expected = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            var constraint = new IsEquivalentTo2DArrayConstraint<int>(expected);

            Assert.That(constraint.DisplayName, Is.EqualTo($"Equivalent Arrays"));
        }
        #endregion DisplayName

        #region Description
        [Test]
        public void Description_ReturnsExpected()
        {
            var expected = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            var constraint = new IsEquivalentTo2DArrayConstraint<int>(expected);

            Assert.That(constraint.Description, Is.EqualTo($"equivalent to {MsgUtils.FormatArray(expected)}"));
        }
        #endregion Description
    }
}
