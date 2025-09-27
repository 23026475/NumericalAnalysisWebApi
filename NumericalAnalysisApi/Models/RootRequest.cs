namespace NumericalAnalysisApi.Models
{
    public class RootRequest
    {
        /// <summary>
        /// The mathematical expression as a string, e.g. "x^3 - x - 2".
        /// (Later we’ll parse this to a function.)
        /// </summary>
        public string Function { get; set; } = string.Empty;

        /// <summary>
        /// The start of the interval.
        /// </summary>
        public double A { get; set; }

        /// <summary>
        /// The end of the interval.
        /// </summary>
        public double B { get; set; }

        /// <summary>
        /// The acceptable error margin.
        /// </summary>
        public double Tolerance { get; set; } = 0.001;

        /// <summary>
        /// Maximum number of iterations.
        /// </summary>
        public int MaxIterations { get; set; } = 100;
    }
}
