using WPFTest.Domain.Entities;

namespace WPFTest.Domain.Ports;

public interface IRespiratoryMonitor
{
    IObservable<RespiratoryReading> GetRespiratoryStream(string patientId);
    void StartMonitoring(string patientId);
    void StopMonitoring(string patientId);
}
