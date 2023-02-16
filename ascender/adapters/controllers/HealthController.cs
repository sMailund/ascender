using Microsoft.AspNetCore.Mvc;

namespace ascender.adapters.controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HealthController
{

    [HttpGet]
    [Route("ping")]
    public string GetPing() => "pong";
}