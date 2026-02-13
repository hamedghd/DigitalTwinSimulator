using Microsoft.Extensions.Hosting;
using DigitalTwinSimulator.Models;

namespace DigitalTwinSimulator.Services
{
    public class MachineSimulatorService : BackgroundService
    {
        private readonly Random _random = new();
        private readonly SensorDataState? _state;

        public MachineSimulatorService(SensorDataState state)
        {
            _state = state;
            Console.WriteLine($"Simulator State Hash: {_state.GetHashCode()}");
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Simulate machine data generation
                var sensorData = new SensorData
                {
                    Timestamp = DateTime.UtcNow,
                    Temperature = _random.Next(20, 100),
                    Pressure = _random.Next(1, 10),
                    Vibration = _random.Next(0, 5),
                    Rpm = _random.Next(1000, 5000)
                };

                Console.WriteLine("Updating state");

                // update the shared state
                _state.CurrentData = sensorData;

                // Here you would typically send this data to a message queue or database
                if (sensorData != null)
                {
                    Console.WriteLine(
                        $"Time: {sensorData.Timestamp:HH:mm:ss} | " +
                        $"Temp: {sensorData.Temperature:F2}°C | " +
                        $"Pressure: {sensorData.Pressure:F2} bar | " +
                        $"Vibration: {sensorData.Vibration:F2} mm/s | " +
                        $"RPM: {sensorData.Rpm:F2}"
                        );
                }
                // Wait for a short period before generating the next data point
                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
