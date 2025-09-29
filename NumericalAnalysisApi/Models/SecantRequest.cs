namespace NumericalAnalysisApi.Models
{
    public class SecantRequest
    {
        public string Function { get; set; } = string.Empty;
        public double X0 { get; set; }   // First guess
        public double X1 { get; set; }   // Second guess
        public double Tolerance { get; set; } = 0.0001;
        public int MaxIterations { get; set; } = 100;
    }
}
