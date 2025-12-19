using System.Reactive.Linq;
using System.Reactive.Subjects;
using WPFPatientMonitor.Domain.Entities;
using WPFPatientMonitor.Domain.Ports;

namespace WPFPatientMonitor.Infrastructure.Adapters.EcgParameters;

public class FakeEcgParametersAdapter : IEcgParameters, IDisposable
{
    private readonly Dictionary<string, Subject<EcgReading>> _streams = new();
    private readonly Dictionary<string, IDisposable> _timers = new();
    private readonly Dictionary<string, HeartBeatGenerator> _generators = new();
    private readonly Random _random = new();

    public IObservable<EcgReading> GetEcgParameterStream(string patientId)
    {
        if (!_streams.ContainsKey(patientId))
        {
            _streams[patientId] = new Subject<EcgReading>();
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
            _streams[patientId] = new Subject<EcgReading>();
        }

        var generator = new HeartBeatGenerator(_random);
        _generators[patientId] = generator;

        var timer = Observable
            .Interval(TimeSpan.FromMilliseconds(10))
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

internal class HeartBeatGenerator
{
    private readonly Random _random;
    private readonly double _baseHeartRate;
    private readonly double _beatsPerSecond;
    private double _phase;
    private double _breathingPhase;
    private double _slowDriftPhase;

    private const double SampleRate = 100.0;

    public HeartBeatGenerator(Random random)
    {
        _random = random;
        _baseHeartRate = 60 + random.Next(30);
        _beatsPerSecond = _baseHeartRate / 60.0;
        _phase = random.NextDouble() * Math.PI * 2;
        _breathingPhase = random.NextDouble() * Math.PI * 2;
        _slowDriftPhase = random.NextDouble() * Math.PI * 2;
    }

    public EcgReading GetNextReading()
    {
        var breathingVariation = Math.Sin(_breathingPhase) * 3.0;
        var slowDrift = Math.Sin(_slowDriftPhase) * 5.0;
        var noise = (_random.NextDouble() - 0.5) * 2.0;

        var leadIIWaveform = GenerateECGWaveform(_phase, 1.0);
        var leadIWaveform = GenerateECGWaveform(_phase, 0.7);
        var leadIIIWaveform = GenerateECGWaveform(_phase, 0.5);
        var leadAVRWaveform = GenerateECGWaveform(_phase, -0.6);
        var leadAVLWaveform = GenerateECGWaveform(_phase, 0.4);

        var heartRate = _baseHeartRate + leadIIWaveform + breathingVariation + slowDrift + noise;
        var leadI = _baseHeartRate + leadIWaveform + breathingVariation * 0.8 + slowDrift * 0.8 + noise * 0.8;
        var leadII = _baseHeartRate + leadIIWaveform + breathingVariation + slowDrift + noise;
        var leadIII = _baseHeartRate + leadIIIWaveform + breathingVariation * 0.6 + slowDrift * 0.6 + noise * 0.6;
        var leadAVR = _baseHeartRate + leadAVRWaveform + breathingVariation * 0.5 + slowDrift * 0.5 - noise * 0.5;
        var leadAVL = _baseHeartRate + leadAVLWaveform + breathingVariation * 0.7 + slowDrift * 0.7 + noise * 0.7;

        _phase += 2.0 * Math.PI * _beatsPerSecond / SampleRate;
        _breathingPhase += 2.0 * Math.PI * 0.25 / SampleRate;
        _slowDriftPhase += 2.0 * Math.PI * 0.05 / SampleRate;

        if (_phase > Math.PI * 2) _phase -= Math.PI * 2;
        if (_breathingPhase > Math.PI * 2) _breathingPhase -= Math.PI * 2;
        if (_slowDriftPhase > Math.PI * 2) _slowDriftPhase -= Math.PI * 2;

        return new EcgReading(
            DateTime.Now,
            (int)Math.Max(40, Math.Min(180, heartRate)),
            (int)Math.Max(40, Math.Min(180, leadI)),
            (int)Math.Max(40, Math.Min(180, leadII)),
            (int)Math.Max(40, Math.Min(180, leadIII)),
            (int)Math.Max(40, Math.Min(180, leadAVR)),
            (int)Math.Max(40, Math.Min(180, leadAVL))
        );
    }

    private double GenerateECGWaveform(double phase, double amplitude)
    {
        var normalizedPhase = phase % (2.0 * Math.PI);

        if (normalizedPhase < 0.05)
        {
            var pWavePhase = normalizedPhase / 0.05;
            return Math.Sin(pWavePhase * Math.PI) * 4 * amplitude;
        }
        else if (normalizedPhase < 0.15)
        {
            var prSegmentPhase = (normalizedPhase - 0.05) / 0.1;
            return Math.Sin(prSegmentPhase * Math.PI * 0.5) * 1.5 * amplitude;
        }
        else if (normalizedPhase < 0.25)
        {
            var qrsPhase = (normalizedPhase - 0.15) / 0.1;
            if (qrsPhase < 0.2)
            {
                return -qrsPhase * 10 * Math.Abs(amplitude);
            }
            else if (qrsPhase < 0.6)
            {
                return ((qrsPhase - 0.2) / 0.4) * 35 * amplitude - 2 * Math.Abs(amplitude);
            }
            else
            {
                return 33 * amplitude - ((qrsPhase - 0.6) / 0.4) * 35 * amplitude;
            }
        }
        else if (normalizedPhase < 0.3)
        {
            var stSegmentPhase = (normalizedPhase - 0.25) / 0.05;
            return Math.Sin(stSegmentPhase * Math.PI * 0.3) * 1 * amplitude;
        }
        else if (normalizedPhase < 0.5)
        {
            var tWavePhase = (normalizedPhase - 0.3) / 0.2;
            return Math.Sin(tWavePhase * Math.PI) * 10 * amplitude;
        }
        else
        {
            return Math.Sin(normalizedPhase * 0.5) * 2 * amplitude;
        }
    }
}
