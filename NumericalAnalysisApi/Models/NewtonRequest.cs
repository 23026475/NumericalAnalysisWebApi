namespace NumericalAnalysisApi.Models
{
    public class NewtonRequest
    {
        public string Function { get; set; } = string.Empty;
        public string Derivative { get; set; } = string.Empty;
        public double InitialGuess { get; set; }
        public double Tolerance { get; set; } = 0.0001;
        public int MaxIterations { get; set; } = 100;
    }
}
