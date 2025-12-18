using System.Reactive.Linq;
using System.Reactive.Subjects;
using WPFTest.Domain.Entities;
using WPFTest.Domain.Ports;

namespace WPFTest.Infrastructure.BloodPressureMonitor;

public class FakeBloodPressureMonitor : IBloodPressureMonitor, IDisposable
{
    private readonly Dictionary<string, Subject<BloodPressureReading>> _streams = new();
    private readonly Dictionary<string, IDisposable> _timers = new();
    private readonly Dictionary<string, BloodPressureGenerator> _generators = new();
    private readonly Random _random = new();

    public IObservable<BloodPressureReading> GetBloodPressureStream(string patientId)
    {
        if (!_streams.ContainsKey(patientId))
        {
            _streams[patientId] = new Subject<BloodPressureReading>();
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
            _streams[patientId] = new Subject<BloodPressureReading>();
        }

        var generator = new BloodPressureGenerator(_random);
        _generators[patientId] = generator;

        var timer = Observable
            .Interval(TimeSpan.FromSeconds(5))
            .Subscribe(_ =>
            {
                var reading = generator.GetNextReading();
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

internal class BloodPressureGenerator
{
    private readonly Random _random;
    private double _baseSystolic;
    private double _baseDiastolic;
    private double _activityPhase;
    private double _slowDriftPhase;

    public BloodPressureGenerator(Random random)
    {
        _random = random;
        _baseSystolic = 110 + random.Next(20);
        _baseDiastolic = 70 + random.Next(15);
        _activityPhase = random.NextDouble() * Math.PI * 2;
        _slowDriftPhase = random.NextDouble() * Math.PI * 2;
    }

    public BloodPressureReading GetNextReading()
    {
        var activityVariation = Math.Sin(_activityPhase) * 8.0;
        var slowDrift = Math.Sin(_slowDriftPhase) * 5.0;
        var noise = (_random.NextDouble() - 0.5) * 4.0;

        var systolic = _baseSystolic + activityVariation + slowDrift + noise;
        var diastolic = _baseDiastolic + activityVariation * 0.5 + slowDrift * 0.5 + noise * 0.5;

        _activityPhase += 0.1;
        _slowDriftPhase += 0.02;

        if (_activityPhase > Math.PI * 2) _activityPhase -= Math.PI * 2;
        if (_slowDriftPhase > Math.PI * 2) _slowDriftPhase -= Math.PI * 2;

        var systolicInt = (int)Math.Max(90, Math.Min(180, systolic));
        var diastolicInt = (int)Math.Max(60, Math.Min(120, diastolic));

        return new BloodPressureReading(DateTime.Now, systolicInt, diastolicInt);
    }
}
