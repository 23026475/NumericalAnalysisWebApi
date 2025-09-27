using Microsoft.AspNetCore.Mvc;
using NumericalAnalysisApi.Models;
using NumericalAnalysisApi.Services;
using NumericalAnalysisApi.Utils;

namespace NumericalAnalysisApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RootFindingController : ControllerBase
    {
        private readonly BisectionMethod _bisectionMethod;

        public RootFindingController()
        {
            _bisectionMethod = new BisectionMethod();
        }

        /// <summary>
        /// Finds a root using the Bisection Method.
        /// </summary>
        [HttpPost("bisection")]
        public ActionResult<RootResponse> Bisection([FromBody] RootRequest request)
        {
            try
            {
                
                // NEW
                Func<double, double> func = FunctionParser.Parse(request.Function);


                double root = _bisectionMethod.Solve(func, request.A, request.B, request.Tolerance, request.MaxIterations);

                return Ok(new RootResponse
                {
                    Root = root,
                    Iterations = request.MaxIterations,
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
