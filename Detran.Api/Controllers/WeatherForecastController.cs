using Detran.Infrastructure.Entity;
using Detran.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detran.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRepository<ApiUserRole> _apiUserRoleRepository;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRepository<ApiUserRole> apiUserRoleRepository)
        {
            _logger = logger;
            _apiUserRoleRepository = apiUserRoleRepository;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost]
        public async ValueTask<ActionResult> Post([FromBody] RoleInput request)
        {
            var role = new ApiUserRole()
            {
                Role = request.Role,
                Description = request.Description

            };
            var result = await _apiUserRoleRepository.CreateAsync(role);
            _apiUserRoleRepository.SaveChanges();

            return Ok(result);
        }
    }
}
