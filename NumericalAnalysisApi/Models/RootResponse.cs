namespace NumericalAnalysisApi.Models
{
    public class RootResponse
    {
        /// <summary>
        /// Whether the computation was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Root approximation (if successful)
        /// </summary>
        public double? Root { get; set; }

        /// <summary>
        /// Number of iterations performed
        /// </summary>
        public int Iterations { get; set; }

        /// <summary>
        /// Error category (e.g., NEWTON_ERROR, BISECTION_ERROR, etc.)
        /// </summary>
        public string? ErrorType { get; set; }

        /// <summary>
        /// Human-readable error message with context
        /// </summary>
        public string? ErrorMessage { get; set; }
    }
}
