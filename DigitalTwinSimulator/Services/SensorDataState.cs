using System;
using DigitalTwinSimulator.Models;

namespace DigitalTwinSimulator.Services
{
    public class SensorDataState
    {
        private SensorData? _currentData;

        // Event to notify subscribers when new data is available
        public event Action? OnChange;

        // Property for getting/setting the current sensor data
        public SensorData? CurrentData
        {
            get => _currentData;
            set
            {
                _currentData = value;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged()
        {
            Console.WriteLine($"NotifyStateChanged fired. Subscribers: {OnChange?.GetInvocationList().Length ?? 0}");
            OnChange?.Invoke();
        }
    }
}
