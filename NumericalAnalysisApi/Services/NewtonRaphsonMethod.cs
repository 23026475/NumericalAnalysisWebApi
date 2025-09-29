using System;

namespace NumericalAnalysisApi.Methods
{
    public class NewtonRaphsonMethod
    {
        public (double root, int iterations) Solve(
            Func<double, double> f,
            Func<double, double> fPrime,
            double x0,
            double tol = 1e-6,
            int maxIter = 100)
        {
            double x = x0;
            int iterations = 0;

            for (int i = 1; i <= maxIter; i++)
            {
                iterations++;
                double fx = f(x);
                double dfx = fPrime(x);

                if (dfx == 0)
                    throw new DivideByZeroException("Derivative is zero, Newton-Raphson fails.");

                double x1 = x - fx / dfx;

                if (Math.Abs(x1 - x) < tol || Math.Abs(f(x1)) < tol)
                    return (x1, iterations);

                x = x1;
            }

            return (x, iterations);
        }
    }
}
