using NCalc;
using System.Text.RegularExpressions;

namespace NumericalAnalysisApi.Utils
{
    public static class FunctionParser
    {
        public static Func<double, double> Parse(string expression)
        {
            // Convert ^ into Pow(x, n)
            expression = Regex.Replace(expression, @"(\w+)\s*\^\s*(\d+)", "Pow($1,$2)");

            return (x) =>
            {
                try
                {
                    var e = new Expression(expression);
                    e.Parameters["x"] = x;
                    var result = e.Evaluate();
                    return Convert.ToDouble(result);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Invalid function expression: {expression}. Details: {ex.Message}");
                }
            };
        }
    }
}
