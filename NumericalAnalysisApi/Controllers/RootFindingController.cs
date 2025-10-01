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

        /// <summary>
        /// Finds a root using the Bisection Method.
        /// </summary>
        [HttpPost("bisection")]
        public ActionResult<RootResponse> Bisection([FromBody] BisectionRequest request)
        {
            try
            {
                Func<double, double> func = FunctionParser.Parse(request.Function);
                var (root, iterations) = _bisectionMethod.Solve(func, request.A, request.B, request.Tolerance, request.MaxIterations);

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
                    ErrorMessage = ex.Message
                });
            }
        }

        /// <summary>
        /// Finds a root using the Newton-Raphson Method.
        /// </summary>
        [HttpPost("newton")]
        public ActionResult<RootResponse> Newton([FromBody] NewtonRequest request)
        {
            try
            {
                Func<double, double> func = FunctionParser.Parse(request.Function);
                Func<double, double> fPrime = FunctionParser.Parse(request.Derivative);

                var (root, iterations) = _newtonMethod.Solve(func, fPrime, request.InitialGuess, request.Tolerance, request.MaxIterations);

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
                    ErrorMessage = ex.Message
                });
            }
        }

        /// <summary>
        /// Finds a root using the Secant Method.
        /// </summary>
        [HttpPost("secant")]
        public ActionResult<RootResponse> Secant([FromBody] SecantRequest request)
        {
            try
            {
                Func<double, double> func = FunctionParser.Parse(request.Function);
                var (root, iterations) = _secantMethod.Solve(func, request.X0, request.X1, request.Tolerance, request.MaxIterations);

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
                    ErrorMessage = ex.Message
                });
            }
        }

        /// <summary>
        /// Finds a root using the Regula-Falsi Method.
        /// </summary>
        [HttpPost("regulafalsi")]
        public ActionResult<RootResponse> RegulaFalsi([FromBody] RegulaFalsiRequest request)
        {
            try
            {
                Func<double, double> func = FunctionParser.Parse(request.Function);
                var (root, iterations) = _regulaFalsiMethod.Solve(func, request.A, request.B, request.Tolerance, request.MaxIterations);

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
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
