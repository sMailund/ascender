using ascender.Providers;
using Microsoft.AspNetCore.Mvc;

namespace ascender.controllers;

[ApiController]
[Route("[controller]")]
public class DebugController
{

    private readonly ITimeProvider _timeProvider;

    public DebugController(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    [HttpPost]
    [Route("addDays/{days}")]
    public void Commit(int days)
    {
        _timeProvider.AddDays(days);
    }
}