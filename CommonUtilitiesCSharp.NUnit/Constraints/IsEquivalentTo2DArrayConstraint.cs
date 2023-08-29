using CommonUtilitiesCSharp.Extensions;
using NUnit.Framework.Constraints;

namespace CommonUtilitiesCSharp.NUnit.Constraints
{
    /// <summary>
    /// Constraint used to determine whether two two-dimensional arrays are equivalent.
    /// </summary>
    /// <typeparam name="TExpected">The type of item in the array.</typeparam>
    public class IsEquivalentTo2DArrayConstraint<TExpected> : Constraint
    {
        private readonly TExpected[,] _expected;

        /// <summary>
        /// Initializes a new instance of the <see cref="IsEquivalentTo2DArrayConstraint{TExpected}"/> class.
        /// </summary>
        /// <param name="expected">Expected array.</param>
        public IsEquivalentTo2DArrayConstraint(TExpected[,] expected) : base(expected)
        {
            _expected = expected;
        }

        /// <inheritdoc/>
        public override string DisplayName => "Equivalent";

        /// <inheritdoc/>
        public override string Description => "equivalent to " + MsgUtils.FormatArray(_expected);

        /// <inheritdoc/>
        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            if (actual == null)
            {
                return new ConstraintResult(this, actual, Arguments[0] is null);
            }

            if (Arguments[0] is null)
            {
                return new ConstraintResult(this, actual, false);
            }

            if (actual is not TExpected[,] actualArray)
            {
                return new ConstraintResult(this, actual, false);
            }

            var expectedArray = (TExpected[,])Arguments[0];

            return new ConstraintResult(this, actual, ArrayExtensions.AreArraysEquivalent(actualArray, expectedArray));
        }
    }
}
