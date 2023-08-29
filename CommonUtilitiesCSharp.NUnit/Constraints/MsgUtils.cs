using System.Text;

namespace CommonUtilitiesCSharp.NUnit.Constraints
{
    /// <summary>
    /// Utility methods used in formatting messages for constraints.
    /// </summary>
    public static class MsgUtils
    {
        public static string FormatArray(Array array)
        {
            if (array is null)
                return "<null>";

            int rank = array.Rank;
            if (array.Length == 0)
                return "<empty>";

            int[] products = new int[rank];

            for (int product = 1, r = rank; --r >= 0;)
                products[r] = product *= array.GetLength(r);

            int count = 0;
            StringBuilder sb = new();
            foreach (object? obj in array)
            {
                if (count > 0)
                    sb.Append(", ");

                bool startSegment = false;
                for (int r = 0; r < rank; r++)
                {
                    startSegment = startSegment || count % products[r] == 0;
                    if (startSegment) sb.Append("< ");
                }

                sb.Append(obj?.ToString());

                ++count;

                bool nextSegment = false;
                for (int r = 0; r < rank; r++)
                {
                    nextSegment = nextSegment || count % products[r] == 0;
                    if (nextSegment) sb.Append(" >");
                }
            }

            return sb.ToString();
        }
    }
}
