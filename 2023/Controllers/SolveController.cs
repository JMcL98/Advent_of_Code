namespace _2023.Controllers;

using Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class SolveController : ControllerBase
{
    private readonly ILogger<SolveController> _logger;
    private readonly ISolveService _solveService;

    public SolveController(ILogger<SolveController> logger, ISolveService solveService)
    {
        _logger = logger;
        _solveService = solveService;
    }

    [HttpGet(Name = "Solve")]
    public string Get(int day, int part, [FromQuery(Name = "args")] List<string> args)
    {
        return _solveService.Solve(day, part, args);
    }
}