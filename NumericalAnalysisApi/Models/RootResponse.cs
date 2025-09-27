namespace NumericalAnalysisApi.Models
{
    public class RootResponse
    {
        public double Root { get; set; }
        public int Iterations { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
