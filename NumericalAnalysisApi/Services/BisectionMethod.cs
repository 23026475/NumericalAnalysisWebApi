namespace NumericalAnalysisApi.Services
{
    public class BisectionMethod
    {
        /// <summary>
        /// Approximates a root of a continuous function using the Bisection Method.
        /// </summary>
        /// <param name="func">The function f(x).</param>
        /// <param name="a">The start of the interval.</param>
        /// <param name="b">The end of the interval.</param>
        /// <param name="tolerance">The acceptable error margin.</param>
        /// <param name="maxIterations">The maximum number of iterations.</param>
        /// <returns>The approximate root.</returns>
        public double Solve(Func<double, double> func, double a, double b, double tolerance = 0.001, int maxIterations = 100)
        {
            if (func(a) * func(b) >= 0)
            {
                throw new ArgumentException("Function must have opposite signs at endpoints a and b.");
            }

            double mid = a;

            for (int i = 0; i < maxIterations; i++)
            {
                mid = (a + b) / 2.0;
                double fMid = func(mid);

                if (Math.Abs(fMid) < tolerance || (b - a) / 2.0 < tolerance)
                {
                    return mid;
                }

                if (func(a) * fMid < 0)
                {
                    b = mid;
                }
                else
                {
                    a = mid;
                }
            }

            return mid;
        }
    }
}
