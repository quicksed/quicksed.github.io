using System.Collections.Generic;

namespace Telegram_bot.Models
{
    public class WeatherResponse
    {
        public string Name { get; set; }

        public List<Weather> Weather { get; set; }

        public TemperatureInfo Main { get; set; }
    }
}
