using WPFPatientMonitor.Domain.Entities;

namespace WPFPatientMonitor.Domain.Ports;

public interface IEcgParameters
{
    IObservable<EcgReading> GetEcgParameterStream(string patientId);
    void StartMonitoring(string patientId);
    void StopMonitoring(string patientId);
}
