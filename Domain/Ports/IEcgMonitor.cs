using WPFTest.Domain.Entities;

namespace WPFTest.Domain.Ports;

public interface IEcgMonitor
{
    IObservable<EcgReading> GetHeartRateStream(string patientId);
    void StartMonitoring(string patientId);
    void StopMonitoring(string patientId);
}
