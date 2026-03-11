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
                string originalExpression = expression;
                expression = expression.ToLower().Trim();

                // Replace constants
                expression = expression.Replace("pi", Math.PI.ToString());
                expression = expression.Replace("e", Math.E.ToString());

                // Replace ^ with Pow()
                expression = Regex.Replace(expression, @"(\w+|\([^)]*\))\s*\^\s*([-\w.()]+)", "Pow($1,$2)");

                // Convert function names to proper case (Sin, Cos, Tan, etc.)
                expression = Regex.Replace(expression, @"\b(sin|cos|tan|asin|acos|atan|sinh|cosh|tanh|sqrt|abs|log|ln|exp)\b",
                    match => match.Value switch
                    {
                        "sin" => "Sin",
                        "cos" => "Cos",
                        "tan" => "Tan",
                        "asin" => "Asin",
                        "acos" => "Acos",
                        "atan" => "Atan",
                        "sinh" => "Sinh",
                        "cosh" => "Cosh",
                        "tanh" => "Tanh",
                        "sqrt" => "Sqrt",
                        "abs" => "Abs",
                        "log" => "Log",
                        "ln" => "Ln",
                        "exp" => "Exp",
                        _ => match.Value
                    });

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
                                    args.Result = Math.Log10(Convert.ToDouble(args.Parameters[0].Evaluate()));
                                else if (args.Parameters.Length == 2)
                                    args.Result = Math.Log(
                                        Convert.ToDouble(args.Parameters[0].Evaluate()),
                                        Convert.ToDouble(args.Parameters[1].Evaluate()));
                                break;
                            case "exp":
                                args.Result = Math.Exp(Convert.ToDouble(args.Parameters[0].Evaluate()));
                                break;
                                // Add more custom functions if needed
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