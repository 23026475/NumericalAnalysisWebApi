namespace NumericalAnalysisApi.Models
{
    public class BisectionRequest
    {
        public string Function { get; set; } = string.Empty;
        public double A { get; set; }   // Left interval bound
        public double B { get; set; }   // Right interval bound
        public double Tolerance { get; set; } = 0.0001;
        public int MaxIterations { get; set; } = 100;
    }
}
