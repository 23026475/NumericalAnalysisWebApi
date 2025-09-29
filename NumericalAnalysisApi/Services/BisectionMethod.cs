namespace NumericalAnalysisApi.Methods
{
    public class BisectionMethod
    {
        
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
