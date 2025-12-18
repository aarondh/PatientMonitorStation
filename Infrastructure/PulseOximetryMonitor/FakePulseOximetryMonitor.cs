using System.Reactive.Linq;
using System.Reactive.Subjects;
using WPFTest.Domain.Entities;
using WPFTest.Domain.Ports;

namespace WPFTest.Infrastructure.PulseOximetryMonitor;

public class FakePulseOximetryMonitor : IPulseOximetryMonitor, IDisposable
{
    private readonly Dictionary<string, Subject<PulseOximetryReading>> _streams = new();
    private readonly Dictionary<string, IDisposable> _timers = new();
    private readonly Dictionary<string, PulseOxGenerator> _generators = new();
    private readonly Random _random = new();

    public IObservable<PulseOximetryReading> GetPulseOximetryStream(string patientId)
    {
        if (!_streams.ContainsKey(patientId))
        {
            _streams[patientId] = new Subject<PulseOximetryReading>();
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
            _streams[patientId] = new Subject<PulseOximetryReading>();
        }

        var generator = new PulseOxGenerator(_random);
        _generators[patientId] = generator;

        var timer = Observable
            .Interval(TimeSpan.FromSeconds(1))
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

internal class PulseOxGenerator
{
    private readonly Random _random;
    private readonly double _baseSpO2;
    private readonly double _basePulseRate;
    private double _breathingPhase;
    private double _activityPhase;
    private double _slowDriftPhase;

    public PulseOxGenerator(Random random)
    {
        _random = random;
        _baseSpO2 = 96 + random.Next(4);
        _basePulseRate = 60 + random.Next(30);
        _breathingPhase = random.NextDouble() * Math.PI * 2;
        _activityPhase = random.NextDouble() * Math.PI * 2;
        _slowDriftPhase = random.NextDouble() * Math.PI * 2;
    }

    public PulseOximetryReading GetNextReading()
    {
        var breathingEffect = Math.Sin(_breathingPhase) * 0.5;
        var activityVariation = Math.Sin(_activityPhase) * 1.5;
        var slowDrift = Math.Sin(_slowDriftPhase) * 1.0;
        var noise = (_random.NextDouble() - 0.5) * 0.5;

        var spO2 = _baseSpO2 + breathingEffect + activityVariation + slowDrift + noise;

        var pulseVariation = Math.Sin(_breathingPhase) * 3.0;
        var pulseActivity = Math.Sin(_activityPhase) * 8.0;
        var pulseDrift = Math.Sin(_slowDriftPhase) * 5.0;
        var pulseNoise = (_random.NextDouble() - 0.5) * 2.0;

        var pulseRate = _basePulseRate + pulseVariation + pulseActivity + pulseDrift + pulseNoise;

        _breathingPhase += 2.0 * Math.PI * 0.25;
        _activityPhase += 0.15;
        _slowDriftPhase += 0.03;

        if (_breathingPhase > Math.PI * 2) _breathingPhase -= Math.PI * 2;
        if (_activityPhase > Math.PI * 2) _activityPhase -= Math.PI * 2;
        if (_slowDriftPhase > Math.PI * 2) _slowDriftPhase -= Math.PI * 2;

        var spO2Int = (int)Math.Max(90, Math.Min(100, spO2));
        var pulseRateInt = (int)Math.Max(40, Math.Min(180, pulseRate));

        return new PulseOximetryReading(DateTime.Now, spO2Int, pulseRateInt);
    }
}
