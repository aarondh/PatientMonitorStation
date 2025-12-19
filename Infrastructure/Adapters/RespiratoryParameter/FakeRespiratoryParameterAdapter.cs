using System.Reactive.Linq;
using System.Reactive.Subjects;
using WPFPatientMonitor.Domain.Entities;
using WPFPatientMonitor.Domain.Ports;

namespace WPFPatientMonitor.Infrastructure.Adapters.RespiratoryParameter;

public class FakeRespiratoryParameterAdapter : IRespiratoryParameter, IDisposable
{
    private readonly Dictionary<string, Subject<RespiratoryReading>> _streams = new();
    private readonly Dictionary<string, IDisposable> _timers = new();
    private readonly Dictionary<string, RespiratoryGenerator> _generators = new();
    private readonly Random _random = new();

    public IObservable<RespiratoryReading> GetRespiratoryStream(string patientId)
    {
        if (!_streams.ContainsKey(patientId))
        {
            _streams[patientId] = new Subject<RespiratoryReading>();
        }
        return _streams[patientId].AsObservable();
    }

    public void StartMonitoring(string patientId)
    {
        if (_timers.ContainsKey(patientId))
        {
            return;
        }

        if (!_streams.ContainsKey(patientId))
        {
            _streams[patientId] = new Subject<RespiratoryReading>();
        }

        var generator = new RespiratoryGenerator(_random);
        _generators[patientId] = generator;

        var timer = Observable
            .Interval(TimeSpan.FromMilliseconds(100))
            .Subscribe(_ =>
            {
                var rate = generator.GetNextValue();
                var reading = new RespiratoryReading(DateTime.Now, rate);
                _streams[patientId].OnNext(reading);
            });

        _timers[patientId] = timer;
    }

    public void StopMonitoring(string patientId)
    {
        if (_timers.ContainsKey(patientId))
        {
            _timers[patientId].Dispose();
            _timers.Remove(patientId);
        }

        if (_generators.ContainsKey(patientId))
        {
            _generators.Remove(patientId);
        }
    }

    public void Dispose()
    {
        foreach (var timer in _timers.Values)
        {
            timer.Dispose();
        }
        _timers.Clear();

        foreach (var stream in _streams.Values)
        {
            stream.Dispose();
        }
        _streams.Clear();
        _generators.Clear();
    }
}

internal class RespiratoryGenerator
{
    private readonly Random _random;
    private readonly double _baseRespiratoryRate;
    private readonly double _breathsPerSecond;
    private double _phase;
    private double _variationPhase;
    private const double SampleRate = 10.0;

    public RespiratoryGenerator(Random random)
    {
        _random = random;
        _baseRespiratoryRate = 12 + random.Next(8);
        _breathsPerSecond = _baseRespiratoryRate / 60.0;
        _phase = random.NextDouble() * Math.PI * 2;
        _variationPhase = random.NextDouble() * Math.PI * 2;
    }

    public int GetNextValue()
    {
        var breathCycle = GenerateBreathWaveform(_phase);

        var slowVariation = Math.Sin(_variationPhase) * 2.0;

        var noise = (_random.NextDouble() - 0.5) * 1.5;

        var respiratoryRate = _baseRespiratoryRate + breathCycle + slowVariation + noise;

        _phase += 2.0 * Math.PI * _breathsPerSecond / SampleRate;
        _variationPhase += 2.0 * Math.PI * 0.05 / SampleRate;

        if (_phase > Math.PI * 2) _phase -= Math.PI * 2;
        if (_variationPhase > Math.PI * 2) _variationPhase -= Math.PI * 2;

        return (int)Math.Max(8, Math.Min(30, respiratoryRate));
    }

    private double GenerateBreathWaveform(double phase)
    {
        var normalizedPhase = phase % (2.0 * Math.PI);

        if (normalizedPhase < Math.PI * 0.4)
        {
            var inhalePhase = normalizedPhase / (Math.PI * 0.4);
            return Math.Sin(inhalePhase * Math.PI) * 6.0;
        }
        else if (normalizedPhase < Math.PI * 0.6)
        {
            var exhalePhase = (normalizedPhase - Math.PI * 0.4) / (Math.PI * 0.2);
            return Math.Sin((1 - exhalePhase) * Math.PI) * 4.0;
        }
        else
        {
            return Math.Sin(normalizedPhase * 0.3) * 1.5;
        }
    }
}
