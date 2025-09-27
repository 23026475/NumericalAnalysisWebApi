namespace NumericalAnalysisApi.Models
{
    public class RootRequest
    {
        public string Function { get; set; } = string.Empty; // e.g. "x^3 - x - 2"
        public double A { get; set; }   // Left interval bound
        public double B { get; set; }   // Right interval bound
        public double Tolerance { get; set; } = 0.0001;
        public int MaxIterations { get; set; } = 100;
    }
}
