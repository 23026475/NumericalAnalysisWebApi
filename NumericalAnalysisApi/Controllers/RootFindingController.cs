using Microsoft.AspNetCore.Mvc;
using NumericalAnalysisApi.Models;
using NumericalAnalysisApi.Methods;
using NumericalAnalysisApi.Utils;

namespace NumericalAnalysisApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RootFindingController : ControllerBase
    {
        private readonly BisectionMethod _bisectionMethod;
        private readonly NewtonRaphsonMethod _newtonMethod;
        private readonly SecantMethod _secantMethod;
        private readonly RegulaFalsiMethod _regulaFalsiMethod;

        public RootFindingController()
        {
            _bisectionMethod = new BisectionMethod();
            _newtonMethod = new NewtonRaphsonMethod();
            _secantMethod = new SecantMethod();
            _regulaFalsiMethod = new RegulaFalsiMethod();
        }

        [HttpPost("bisection")]
        [ProducesResponseType(typeof(RootResponse), 200)]
        [ProducesResponseType(typeof(RootResponse), 400)]
        public ActionResult<RootResponse> Bisection([FromBody] BisectionRequest request)
        {
            try
            {
                var func = FunctionParser.Parse(request.Function);
                var (root, iterations) = _bisectionMethod.Solve(
                    func, request.A, request.B, request.Tolerance, request.MaxIterations);

                return Ok(new RootResponse
                {
                    Root = root,
                    Iterations = iterations,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new RootResponse
                {
                    Success = false,
                    ErrorType = "BISECTION_ERROR",
                    ErrorMessage = $"Bisection failed for {request.Function}: {ex.Message}"
                });
            }
        }

        [HttpPost("newton")]
        [ProducesResponseType(typeof(RootResponse), 200)]
        [ProducesResponseType(typeof(RootResponse), 400)]
        public ActionResult<RootResponse> Newton([FromBody] NewtonRequest request)
        {
            try
            {
                var func = FunctionParser.Parse(request.Function);
                var fPrime = FunctionParser.Parse(request.Derivative);

                var (root, iterations) = _newtonMethod.Solve(
                    func, fPrime, request.InitialGuess, request.Tolerance, request.MaxIterations);

                return Ok(new RootResponse
                {
                    Root = root,
                    Iterations = iterations,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new RootResponse
                {
                    Success = false,
                    ErrorType = "NEWTON_ERROR",
                    ErrorMessage = $"Newton-Raphson failed for {request.Function}: {ex.Message}"
                });
            }
        }

        [HttpPost("secant")]
        [ProducesResponseType(typeof(RootResponse), 200)]
        [ProducesResponseType(typeof(RootResponse), 400)]
        public ActionResult<RootResponse> Secant([FromBody] SecantRequest request)
        {
            try
            {
                var func = FunctionParser.Parse(request.Function);
                var (root, iterations) = _secantMethod.Solve(
                    func, request.X0, request.X1, request.Tolerance, request.MaxIterations);

                return Ok(new RootResponse
                {
                    Root = root,
                    Iterations = iterations,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new RootResponse
                {
                    Success = false,
                    ErrorType = "SECANT_ERROR",
                    ErrorMessage = $"Secant failed for {request.Function}: {ex.Message}"
                });
            }
        }

        [HttpPost("regulafalsi")]
        [ProducesResponseType(typeof(RootResponse), 200)]
        [ProducesResponseType(typeof(RootResponse), 400)]
        public ActionResult<RootResponse> RegulaFalsi([FromBody] RegulaFalsiRequest request)
        {
            try
            {
                var func = FunctionParser.Parse(request.Function);
                var (root, iterations) = _regulaFalsiMethod.Solve(
                    func, request.A, request.B, request.Tolerance, request.MaxIterations);

                return Ok(new RootResponse
                {
                    Root = root,
                    Iterations = iterations,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new RootResponse
                {
                    Success = false,
                    ErrorType = "REGULA_FALSI_ERROR",
                    ErrorMessage = $"Regula Falsi failed for {request.Function}: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Preview function evaluation at sample points or a range
        /// </summary>
        [HttpPost("preview")]
        public IActionResult PreviewFunction([FromBody] PreviewRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Expression))
            {
                return BadRequest(new
                {
                    Success = false,
                    ErrorType = "MISSING_EXPRESSION",
                    ErrorMessage = "Expression is required. Example: sin(x), x^2 - 4, ln(x)"
                });
            }

            try
            {
                var func = FunctionParser.Parse(request.Expression);

                if (request.Points != null && request.Points.Any())
                {
                    var results = request.Points.Select(p => new { x = p, y = func(p) });
                    return Ok(new { Success = true, Results = results });
                }

                if (request.Start.HasValue && request.End.HasValue && request.Steps > 0)
                {
                    double stepSize = (request.End.Value - request.Start.Value) / request.Steps;
                    var results = Enumerable.Range(0, request.Steps + 1)
                        .Select(i =>
                        {
                            double x = request.Start.Value + i * stepSize;
                            return new { x, y = func(x) };
                        });

                    return Ok(new { Success = true, Results = results });
                }

                return BadRequest(new
                {
                    Success = false,
                    ErrorType = "INVALID_REQUEST",
                    ErrorMessage = "Provide either explicit sample points OR (start, end, steps)"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    ErrorType = "EVALUATION_ERROR",
                    ErrorMessage = $"Could not evaluate function: {ex.Message}"
                });
            }
        }
    }

    // DTO for preview requests
    public class PreviewRequest
    {
        public string Expression { get; set; } = "";
        public List<double>? Points { get; set; }
        public double? Start { get; set; }
        public double? End { get; set; }
        public int Steps { get; set; } = 0;
    }
}
