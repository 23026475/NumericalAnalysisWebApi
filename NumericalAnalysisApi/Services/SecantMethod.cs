using System;

namespace NumericalAnalysisApi.Methods
{
    public class SecantMethod
    {
        public (double root, int iterations) Solve(
            Func<double, double> f,
            double x0,
            double x1,
            double tol = 1e-6,
            int maxIter = 100)
        {
            double xPrev = x0;
            double xCurr = x1;
            int iterations = 0;

            for (int i = 1; i <= maxIter; i++)
            {
                iterations++;
                double fPrev = f(xPrev);
                double fCurr = f(xCurr);

                if (fCurr - fPrev == 0)
                    throw new DivideByZeroException("Division by zero, Secant method fails.");

                double xNext = xCurr - fCurr * (xCurr - xPrev) / (fCurr - fPrev);

                if (Math.Abs(xNext - xCurr) < tol || Math.Abs(f(xNext)) < tol)
                    return (xNext, iterations);

                xPrev = xCurr;
                xCurr = xNext;
            }

            return (xCurr, iterations);
        }
    }
}
