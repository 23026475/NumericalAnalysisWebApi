using System;

namespace NumericalAnalysisApi.Methods
{
    public class RegulaFalsiMethod
    {
        public (double root, int iterations) Solve(
            Func<double, double> f,
            double a,
            double b,
            double tol = 1e-6,
            int maxIter = 100)
        {
            if (f(a) * f(b) >= 0)
                throw new ArgumentException("f(a) and f(b) must have opposite signs.");

            double c = a;
            int iterations = 0;

            for (int i = 1; i <= maxIter; i++)
            {
                iterations++;
                c = b - f(b) * (b - a) / (f(b) - f(a));

                if (Math.Abs(f(c)) < tol)
                    return (c, iterations);

                if (f(a) * f(c) < 0)
                    b = c;
                else
                    a = c;
            }

            return (c, iterations);
        }
    }
}
