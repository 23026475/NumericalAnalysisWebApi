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
                    ErrorMessage = $"Regula Falsi failed for {request.Function}: {ex.Message}"
                });
            }
        }
    }
}
