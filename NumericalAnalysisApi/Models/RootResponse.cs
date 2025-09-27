namespace NumericalAnalysisApi.Models
{
    public class RootResponse
    {
        /// <summary>
        /// The approximate root found.
        /// </summary>
        public double Root { get; set; }

        /// <summary>
        /// Number of iterations taken.
        /// </summary>
        public int Iterations { get; set; }

        /// <summary>
        /// Whether the method succeeded.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Optional error message (if failed).
        /// </summary>
        public string? ErrorMessage { get; set; }
    }
}
