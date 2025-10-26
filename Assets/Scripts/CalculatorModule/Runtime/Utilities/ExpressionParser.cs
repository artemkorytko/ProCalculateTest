using System.Text.RegularExpressions;


namespace ProCalculate.Calculator
{
    public static class ExpressionParser
    {
        private static readonly Regex ValidExpr = new Regex(@"^\d+\+\d+$", RegexOptions.Compiled);

        public static bool TryParse(string expr, out long left, out long right)
        {
            left = 0;
            right = 0;
            if (string.IsNullOrWhiteSpace(expr)) return false;

            expr = expr.Trim();

            if (!ValidExpr.IsMatch(expr)) return false;

            var parts = expr.Split('+');
            if (parts.Length != 2) return false;

            if (!long.TryParse(parts[0], out left)) return false;
            if (!long.TryParse(parts[1], out right)) return false;

            return true;
        }
    }
}