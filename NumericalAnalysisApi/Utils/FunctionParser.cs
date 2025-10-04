using NCalc;
using System.Text.RegularExpressions;

namespace NumericalAnalysisApi.Utils
{
    public static class FunctionParser
    {
        public static Func<double, double> Parse(string expression)
        {
            try
            {
                // Normalize casing (all lowercase for easier handling)
                expression = expression.ToLower().Trim();

                // Replace constants
                expression = expression.Replace("pi", Math.PI.ToString());
                expression = expression.Replace("e", Math.E.ToString());

                // Replace ^ with Pow()
                expression = Regex.Replace(expression, @"(\w+|\([^)]*\))\s*\^\s*([-\w.()]+)", "Pow($1,$2)");

                return (x) =>
                {
                    var e = new Expression(expression);

                    // Add variable
                    e.Parameters["x"] = x;

                    // Extend function support
                    e.EvaluateFunction += (name, args) =>
                    {
                        switch (name.ToLower())
                        {
                            case "ln":
                                args.Result = Math.Log(Convert.ToDouble(args.Parameters[0].Evaluate()));
                                break;
                            case "log":
                                if (args.Parameters.Length == 1)
                                    args.Result = Math.Log10(Convert.ToDouble(args.Parameters[0].Evaluate())); // log(x)
                                else if (args.Parameters.Length == 2)
                                    args.Result = Math.Log(
                                        Convert.ToDouble(args.Parameters[0].Evaluate()),
                                        Convert.ToDouble(args.Parameters[1].Evaluate())); // log(x, base)
                                break;
                            case "exp":
                                args.Result = Math.Exp(Convert.ToDouble(args.Parameters[0].Evaluate()));
                                break;
                                // NCalc already supports sin, cos, tan, sqrt, abs, Pow etc.
                        }
                    };

                    var result = e.Evaluate();
                    return Convert.ToDouble(result);
                };
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Could not parse function '{expression}'. " +
                    $"Check syntax (e.g. sin(x), cos(x), ln(x), x^2). Details: {ex.Message}"
                );
            }
        }
    }
}
