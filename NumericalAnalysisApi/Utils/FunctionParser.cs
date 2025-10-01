using NCalc;
using System.Text.RegularExpressions;

namespace NumericalAnalysisApi.Utils
{
    public static class FunctionParser
    {
        public static Func<double, double> Parse(string expression)
        {
            // Replace ^ with Pow() to support exponents (works for x^2, x^y, etc.)
            expression = Regex.Replace(expression, @"(\w+|\([^)]*\))\s*\^\s*([-\w.()]+)", "Pow($1,$2)");

            return (x) =>
            {
                try
                {
                    var e = new Expression(expression);

                    // Add variable
                    e.Parameters["x"] = x;

                    // Register extra functions if needed
                    e.EvaluateFunction += (name, args) =>
                    {
                        switch (name.ToLower())
                        {
                            case "ln":
                                args.Result = Math.Log(Convert.ToDouble(args.Parameters[0].Evaluate()));
                                break;
                            case "log10":
                                args.Result = Math.Log10(Convert.ToDouble(args.Parameters[0].Evaluate()));
                                break;
                            case "exp":
                                args.Result = Math.Exp(Convert.ToDouble(args.Parameters[0].Evaluate()));
                                break;
                                // NCalc already supports sin, cos, tan, abs, sqrt, Pow, etc.
                        }
                    };

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
