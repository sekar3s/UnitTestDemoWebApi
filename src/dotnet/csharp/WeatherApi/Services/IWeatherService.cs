using WeatherApi.Models;

namespace WeatherApi.Services
{
    public interface IWeatherService
    {
        /// <summary>
        ///  Get all weather forecasts
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync();

        /// <summary>
        /// Get weather forecast by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<WeatherForecast> GetWeatherForecastAsync(int id);

        /// <summary>
        /// Add a new weather forecast
        /// </summary> 
        /// <param name="weatherForecast"></param>
        /// <returns></returns>
        Task<WeatherForecast> AddWeatherForecastAsync(WeatherForecast weatherForecast);
    }
}
