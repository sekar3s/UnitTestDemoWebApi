﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnitTestDemoWebApi.DAL;
using UnitTestDemoWebApi.Models;
using UnitTestDemoWebApi.Services;

namespace UnitTestDemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastsController : ControllerBase
    {

        private readonly IWeatherService _weatherService;
        private readonly ILogger<WeatherForecastsController> _logger;

        public WeatherForecastsController(ILogger<WeatherForecastsController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        // GET: api/WeatherForecasts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetWeatherForecast()
        {
            var weatherForecasts = await _weatherService.GetWeatherForecasts();

            return weatherForecasts.ToList();
        }

        // GET: api/WeatherForecasts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecast>> GetWeatherForecast(int id)
        {
            var weatherForecast = await _weatherService.GetWeatherForecast(id);

            #region break the test
            if (weatherForecast.Id == -1)
            {
                weatherForecast.Summary = "this is not right!";
            }
            #endregion

            if (weatherForecast == null)
            {
                return NotFound();
            }

            return weatherForecast;
        }

        //// PUT: api/WeatherForecasts/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutWeatherForecast(int id, WeatherForecast weatherForecast)
        //{
        //    if (id != weatherForecast.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(weatherForecast).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!WeatherForecastExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/WeatherForecasts
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<WeatherForecast>> PostWeatherForecast(WeatherForecast weatherForecast)
        //{
        //    if (_context.WeatherForecast == null)
        //    {
        //        return Problem("Entity set 'WebApiContext.WeatherForecast'  is null.");
        //    }
        //    _context.WeatherForecast.Add(weatherForecast);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetWeatherForecast", new { id = weatherForecast.Id }, weatherForecast);
        //}

        //// DELETE: api/WeatherForecasts/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteWeatherForecast(int id)
        //{
        //    if (_context.WeatherForecast == null)
        //    {
        //        return NotFound();
        //    }
        //    var weatherForecast = await _context.WeatherForecast.FindAsync(id);
        //    if (weatherForecast == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.WeatherForecast.Remove(weatherForecast);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool WeatherForecastExists(int id)
        //{
        //    return (_context.WeatherForecast?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
