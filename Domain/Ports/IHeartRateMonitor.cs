using WPFTest.Domain.Entities;

namespace WPFTest.Domain.Ports;

public interface IHeartRateMonitor
{
    IObservable<HeartRateReading> GetHeartRateStream(string patientId);
    void StartMonitoring(string patientId);
    void StopMonitoring(string patientId);
}
