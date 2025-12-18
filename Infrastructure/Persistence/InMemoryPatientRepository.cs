using WPFTest.Domain.Entities;
using WPFTest.Domain.Ports;
using Range = WPFTest.Domain.Entities.Range;

namespace WPFTest.Infrastructure.Persistence;

public class InMemoryPatientRepository : IPatientRepository
{
    private readonly List<Room> _rooms = new()
    {
        // ICU Rooms
        new Room("icu-101", "ICU 101"),
        new Room("icu-102", "ICU 102"),
        new Room("icu-103", "ICU 103"),
        new Room("icu-104", "ICU 104"),
        new Room("icu-105", "ICU 105"),
        new Room("icu-106", "ICU 106"),

        // Ward Rooms
        new Room("ward-201", "Ward 201"),
        new Room("ward-202", "Ward 202"),
        new Room("ward-203", "Ward 203"),
        new Room("ward-204", "Ward 204"),
        new Room("ward-205", "Ward 205"),
        new Room("ward-206", "Ward 206")
    };
    private readonly List<MonitorProfile> _monitorProfiles = new()
    {
        new MonitorProfile
        {
            Id = "profile-default",
            ProfileName = "Default",
            MonitorSettings = new Dictionary<MonitorType, MonitorSetting>
            {
                [MonitorType.HeartRate] = new MonitorSetting
                {
                    State = MonitorViewState.Both,
                    GraphLabel = "HR",
                    Unit = "bpm",
                    StrokeColor = "#E74C3C",
                    FillColor = "#C0392B",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting
                    {
                        IsEnabled = true,
                        NormalRange = new Range(60, 100),
                        WarningRange = new Range(50, 120),
                        CriticalRange = new Range(40, 150),
                        WarningColor = "#F39C12",
                        CriticalColor = "#E74C3C"
                    }
                },
                [MonitorType.LeadI] = new MonitorSetting
                {
                    State = MonitorViewState.Graph,
                    GraphLabel = "Lead I",
                    Unit = "mV",
                    StrokeColor = "#2ECC71",
                    FillColor = "#27AE60",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting { IsEnabled = false }
                },
                [MonitorType.LeadII] = new MonitorSetting
                {
                    State = MonitorViewState.Graph,
                    GraphLabel = "Lead II",
                    Unit = "mV",
                    StrokeColor = "#2ECC71",
                    FillColor = "#27AE60",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting { IsEnabled = false }
                },
                [MonitorType.LeadIII] = new MonitorSetting
                {
                    State = MonitorViewState.Graph,
                    GraphLabel = "Lead III",
                    Unit = "mV",
                    StrokeColor = "#2ECC71",
                    FillColor = "#27AE60",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting { IsEnabled = false }
                },
                [MonitorType.LeadAVR] = new MonitorSetting
                {
                    State = MonitorViewState.Graph,
                    GraphLabel = "aVR",
                    Unit = "mV",
                    StrokeColor = "#2ECC71",
                    FillColor = "#27AE60",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting { IsEnabled = false }
                },
                [MonitorType.LeadAVL] = new MonitorSetting
                {
                    State = MonitorViewState.Graph,
                    GraphLabel = "aVL",
                    Unit = "mV",
                    StrokeColor = "#2ECC71",
                    FillColor = "#27AE60",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting { IsEnabled = false }
                },
                [MonitorType.BloodPressure] = new MonitorSetting
                {
                    State = MonitorViewState.Vital,
                    GraphLabel = "BP",
                    Unit = "mmHg",
                    StrokeColor = "#3498DB",
                    FillColor = "#2980B9",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting
                    {
                        IsEnabled = true,
                        NormalRange = new Range(90, 140),
                        WarningRange = new Range(80, 160),
                        CriticalRange = new Range(70, 180),
                        WarningColor = "#F39C12",
                        CriticalColor = "#E74C3C"
                    }
                },
                [MonitorType.RespiratoryRate] = new MonitorSetting
                {
                    State = MonitorViewState.Both,
                    GraphLabel = "Resp",
                    Unit = "/min",
                    StrokeColor = "#3498DB",
                    FillColor = "#2980B9",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting
                    {
                        IsEnabled = true,
                        NormalRange = new Range(12, 20),
                        WarningRange = new Range(8, 25),
                        CriticalRange = new Range(5, 30),
                        WarningColor = "#F39C12",
                        CriticalColor = "#E74C3C"
                    }
                },
                [MonitorType.SpO2] = new MonitorSetting
                {
                    State = MonitorViewState.Vital,
                    GraphLabel = "SpO₂",
                    Unit = "%",
                    StrokeColor = "#9B59B6",
                    FillColor = "#8E44AD",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting
                    {
                        IsEnabled = true,
                        NormalRange = new Range(95, 100),
                        WarningRange = new Range(90, 94),
                        CriticalRange = new Range(0, 89),
                        WarningColor = "#F39C12",
                        CriticalColor = "#E74C3C"
                    }
                },
                [MonitorType.PulseRate] = new MonitorSetting
                {
                    State = MonitorViewState.Both,
                    GraphLabel = "Pulse",
                    Unit = "bpm",
                    StrokeColor = "#1ABC9C",
                    FillColor = "#16A085",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting
                    {
                        IsEnabled = true,
                        NormalRange = new Range(60, 100),
                        WarningRange = new Range(50, 120),
                        CriticalRange = new Range(40, 150),
                        WarningColor = "#F39C12",
                        CriticalColor = "#E74C3C"
                    }
                }
            }
        },
        new MonitorProfile
        {
            Id = "profile-icu",
            ProfileName = "ICU Standard",
            MonitorSettings = new Dictionary<MonitorType, MonitorSetting>
            {
                [MonitorType.HeartRate] = new MonitorSetting
                {
                    State = MonitorViewState.Both,
                    GraphLabel = "HR",
                    Unit = "bpm",
                    StrokeColor = "#E74C3C",
                    FillColor = "#C0392B",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting
                    {
                        IsEnabled = true,
                        NormalRange = new Range(60, 100),
                        WarningRange = new Range(50, 120),
                        CriticalRange = new Range(40, 150),
                        WarningColor = "#F39C12",
                        CriticalColor = "#E74C3C"
                    }
                },
                [MonitorType.LeadI] = new MonitorSetting
                {
                    State = MonitorViewState.Graph,
                    GraphLabel = "Lead I",
                    Unit = "mV",
                    StrokeColor = "#2ECC71",
                    FillColor = "#27AE60",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting { IsEnabled = false }
                },
                [MonitorType.LeadII] = new MonitorSetting
                {
                    State = MonitorViewState.Graph,
                    GraphLabel = "Lead II",
                    Unit = "mV",
                    StrokeColor = "#2ECC71",
                    FillColor = "#27AE60",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting { IsEnabled = false }
                },
                [MonitorType.LeadIII] = new MonitorSetting
                {
                    State = MonitorViewState.Graph,
                    GraphLabel = "Lead III",
                    Unit = "mV",
                    StrokeColor = "#2ECC71",
                    FillColor = "#27AE60",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting { IsEnabled = false }
                },
                [MonitorType.LeadAVR] = new MonitorSetting
                {
                    State = MonitorViewState.Graph,
                    GraphLabel = "aVR",
                    Unit = "mV",
                    StrokeColor = "#2ECC71",
                    FillColor = "#27AE60",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting { IsEnabled = false }
                },
                [MonitorType.LeadAVL] = new MonitorSetting
                {
                    State = MonitorViewState.Graph,
                    GraphLabel = "aVL",
                    Unit = "mV",
                    StrokeColor = "#2ECC71",
                    FillColor = "#27AE60",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting { IsEnabled = false }
                },
                [MonitorType.BloodPressure] = new MonitorSetting
                {
                    State = MonitorViewState.Vital,
                    GraphLabel = "BP",
                    Unit = "mmHg",
                    StrokeColor = "#3498DB",
                    FillColor = "#2980B9",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting
                    {
                        IsEnabled = true,
                        NormalRange = new Range(90, 140),
                        WarningRange = new Range(80, 160),
                        CriticalRange = new Range(70, 180),
                        WarningColor = "#F39C12",
                        CriticalColor = "#E74C3C"
                    }
                },
                [MonitorType.RespiratoryRate] = new MonitorSetting
                {
                    State = MonitorViewState.Both,
                    GraphLabel = "Resp",
                    Unit = "/min",
                    StrokeColor = "#3498DB",
                    FillColor = "#2980B9",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting
                    {
                        IsEnabled = true,
                        NormalRange = new Range(12, 20),
                        WarningRange = new Range(8, 25),
                        CriticalRange = new Range(5, 30),
                        WarningColor = "#F39C12",
                        CriticalColor = "#E74C3C"
                    }
                },
                [MonitorType.SpO2] = new MonitorSetting
                {
                    State = MonitorViewState.Vital,
                    GraphLabel = "SpO₂",
                    Unit = "%",
                    StrokeColor = "#9B59B6",
                    FillColor = "#8E44AD",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting
                    {
                        IsEnabled = true,
                        NormalRange = new Range(95, 100),
                        WarningRange = new Range(90, 94),
                        CriticalRange = new Range(0, 89),
                        WarningColor = "#F39C12",
                        CriticalColor = "#E74C3C"
                    }
                },
                [MonitorType.PulseRate] = new MonitorSetting
                {
                    State = MonitorViewState.Both,
                    GraphLabel = "Pulse",
                    Unit = "bpm",
                    StrokeColor = "#1ABC9C",
                    FillColor = "#16A085",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting
                    {
                        IsEnabled = true,
                        NormalRange = new Range(60, 100),
                        WarningRange = new Range(50, 120),
                        CriticalRange = new Range(40, 150),
                        WarningColor = "#F39C12",
                        CriticalColor = "#E74C3C"
                    }
                }
            }
        },
        new MonitorProfile
        {
            Id = "profile-ward",
            ProfileName = "Ward Standard",
            MonitorSettings = new Dictionary<MonitorType, MonitorSetting>
            {
                [MonitorType.HeartRate] = new MonitorSetting
                {
                    State = MonitorViewState.Both,
                    GraphLabel = "HR",
                    Unit = "bpm",
                    StrokeColor = "#E74C3C",
                    FillColor = "#C0392B",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting
                    {
                        IsEnabled = true,
                        NormalRange = new Range(60, 100),
                        WarningRange = new Range(50, 120),
                        CriticalRange = new Range(40, 150),
                        WarningColor = "#F39C12",
                        CriticalColor = "#E74C3C"
                    }
                },
                [MonitorType.LeadI] = new MonitorSetting
                {
                    State = MonitorViewState.Graph,
                    GraphLabel = "Lead I",
                    Unit = "mV",
                    StrokeColor = "#2ECC71",
                    FillColor = "#27AE60",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting { IsEnabled = false }
                },
                [MonitorType.LeadII] = new MonitorSetting
                {
                    State = MonitorViewState.Graph,
                    GraphLabel = "Lead II",
                    Unit = "mV",
                    StrokeColor = "#2ECC71",
                    FillColor = "#27AE60",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting { IsEnabled = false }
                },
                [MonitorType.LeadIII] = new MonitorSetting
                {
                    State = MonitorViewState.Graph,
                    GraphLabel = "Lead III",
                    Unit = "mV",
                    StrokeColor = "#2ECC71",
                    FillColor = "#27AE60",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting { IsEnabled = false }
                },
                [MonitorType.LeadAVR] = new MonitorSetting
                {
                    State = MonitorViewState.Graph,
                    GraphLabel = "aVR",
                    Unit = "mV",
                    StrokeColor = "#2ECC71",
                    FillColor = "#27AE60",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting { IsEnabled = false }
                },
                [MonitorType.LeadAVL] = new MonitorSetting
                {
                    State = MonitorViewState.Graph,
                    GraphLabel = "aVL",
                    Unit = "mV",
                    StrokeColor = "#2ECC71",
                    FillColor = "#27AE60",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting { IsEnabled = false }
                },
                [MonitorType.BloodPressure] = new MonitorSetting
                {
                    State = MonitorViewState.Vital,
                    GraphLabel = "BP",
                    Unit = "mmHg",
                    StrokeColor = "#3498DB",
                    FillColor = "#2980B9",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting
                    {
                        IsEnabled = true,
                        NormalRange = new Range(90, 140),
                        WarningRange = new Range(80, 160),
                        CriticalRange = new Range(70, 180),
                        WarningColor = "#F39C12",
                        CriticalColor = "#E74C3C"
                    }
                },
                [MonitorType.RespiratoryRate] = new MonitorSetting
                {
                    State = MonitorViewState.Both,
                    GraphLabel = "Resp",
                    Unit = "/min",
                    StrokeColor = "#3498DB",
                    FillColor = "#2980B9",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting
                    {
                        IsEnabled = true,
                        NormalRange = new Range(12, 20),
                        WarningRange = new Range(8, 25),
                        CriticalRange = new Range(5, 30),
                        WarningColor = "#F39C12",
                        CriticalColor = "#E74C3C"
                    }
                },
                [MonitorType.SpO2] = new MonitorSetting
                {
                    State = MonitorViewState.Vital,
                    GraphLabel = "SpO₂",
                    Unit = "%",
                    StrokeColor = "#9B59B6",
                    FillColor = "#8E44AD",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting
                    {
                        IsEnabled = true,
                        NormalRange = new Range(95, 100),
                        WarningRange = new Range(90, 94),
                        CriticalRange = new Range(0, 89),
                        WarningColor = "#F39C12",
                        CriticalColor = "#E74C3C"
                    }
                },
                [MonitorType.PulseRate] = new MonitorSetting
                {
                    State = MonitorViewState.Both,
                    GraphLabel = "Pulse",
                    Unit = "bpm",
                    StrokeColor = "#1ABC9C",
                    FillColor = "#16A085",
                    BorderColor = "#34495E",
                    Alarm = new AlarmSetting
                    {
                        IsEnabled = true,
                        NormalRange = new Range(60, 100),
                        WarningRange = new Range(50, 120),
                        CriticalRange = new Range(40, 150),
                        WarningColor = "#F39C12",
                        CriticalColor = "#E74C3C"
                    }
                }
            }
        }
    };
    private readonly List<EpisodeOfCare> _episodesOfCare = new()
    {
        // ICU 101
        new EpisodeOfCare { PatientId = "patient-1", RoomId = "icu-101", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-5), DateTimeOffset.Now.AddDays(2)) },
        new EpisodeOfCare { PatientId = "patient-2", RoomId = "icu-101", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-3), DateTimeOffset.Now.AddDays(3)) },
        new EpisodeOfCare { PatientId = "patient-3", RoomId = "icu-101", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-7), DateTimeOffset.Now.AddDays(1)) },
        new EpisodeOfCare { PatientId = "patient-4", RoomId = "icu-101", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-2), DateTimeOffset.Now.AddDays(5)) },

        // ICU 102
        new EpisodeOfCare { PatientId = "patient-5", RoomId = "icu-102", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-4), DateTimeOffset.Now.AddDays(4)) },
        new EpisodeOfCare { PatientId = "patient-6", RoomId = "icu-102", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-6), DateTimeOffset.Now.AddDays(2)) },
        new EpisodeOfCare { PatientId = "patient-7", RoomId = "icu-102", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-1), DateTimeOffset.Now.AddDays(6)) },
        new EpisodeOfCare { PatientId = "patient-8", RoomId = "icu-102", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-8), DateTimeOffset.Now.AddDays(1)) },

        // ICU 103
        new EpisodeOfCare { PatientId = "patient-9", RoomId = "icu-103", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-3), DateTimeOffset.Now.AddDays(4)) },
        new EpisodeOfCare { PatientId = "patient-10", RoomId = "icu-103", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-5), DateTimeOffset.Now.AddDays(3)) },
        new EpisodeOfCare { PatientId = "patient-11", RoomId = "icu-103", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-2), DateTimeOffset.Now.AddDays(5)) },
        new EpisodeOfCare { PatientId = "patient-12", RoomId = "icu-103", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-4), DateTimeOffset.Now.AddDays(3)) },

        // ICU 104
        new EpisodeOfCare { PatientId = "patient-13", RoomId = "icu-104", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-7), DateTimeOffset.Now.AddDays(2)) },
        new EpisodeOfCare { PatientId = "patient-14", RoomId = "icu-104", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-1), DateTimeOffset.Now.AddDays(7)) },
        new EpisodeOfCare { PatientId = "patient-15", RoomId = "icu-104", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-6), DateTimeOffset.Now.AddDays(2)) },
        new EpisodeOfCare { PatientId = "patient-16", RoomId = "icu-104", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-3), DateTimeOffset.Now.AddDays(4)) },

        // ICU 105
        new EpisodeOfCare { PatientId = "patient-17", RoomId = "icu-105", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-4), DateTimeOffset.Now.AddDays(3)) },
        new EpisodeOfCare { PatientId = "patient-18", RoomId = "icu-105", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-2), DateTimeOffset.Now.AddDays(6)) },
        new EpisodeOfCare { PatientId = "patient-19", RoomId = "icu-105", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-9), DateTimeOffset.Now.AddDays(1)) },
        new EpisodeOfCare { PatientId = "patient-20", RoomId = "icu-105", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-5), DateTimeOffset.Now.AddDays(3)) },

        // ICU 106
        new EpisodeOfCare { PatientId = "patient-21", RoomId = "icu-106", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-3), DateTimeOffset.Now.AddDays(5)) },
        new EpisodeOfCare { PatientId = "patient-22", RoomId = "icu-106", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-6), DateTimeOffset.Now.AddDays(2)) },
        new EpisodeOfCare { PatientId = "patient-23", RoomId = "icu-106", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-1), DateTimeOffset.Now.AddDays(8)) },
        new EpisodeOfCare { PatientId = "patient-24", RoomId = "icu-106", MonitorProfileId = "profile-icu", Period = new Period(DateTimeOffset.Now.AddDays(-4), DateTimeOffset.Now.AddDays(4)) },

        // Ward 201
        new EpisodeOfCare { PatientId = "patient-25", RoomId = "ward-201", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-2), DateTimeOffset.Now.AddDays(5)) },
        new EpisodeOfCare { PatientId = "patient-26", RoomId = "ward-201", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-5), DateTimeOffset.Now.AddDays(3)) },
        new EpisodeOfCare { PatientId = "patient-27", RoomId = "ward-201", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-3), DateTimeOffset.Now.AddDays(4)) },
        new EpisodeOfCare { PatientId = "patient-28", RoomId = "ward-201", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-7), DateTimeOffset.Now.AddDays(2)) },

        // Ward 202
        new EpisodeOfCare { PatientId = "patient-29", RoomId = "ward-202", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-1), DateTimeOffset.Now.AddDays(6)) },
        new EpisodeOfCare { PatientId = "patient-30", RoomId = "ward-202", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-4), DateTimeOffset.Now.AddDays(4)) },
        new EpisodeOfCare { PatientId = "patient-31", RoomId = "ward-202", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-6), DateTimeOffset.Now.AddDays(2)) },
        new EpisodeOfCare { PatientId = "patient-32", RoomId = "ward-202", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-2), DateTimeOffset.Now.AddDays(5)) },

        // Ward 203
        new EpisodeOfCare { PatientId = "patient-33", RoomId = "ward-203", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-8), DateTimeOffset.Now.AddDays(1)) },
        new EpisodeOfCare { PatientId = "patient-34", RoomId = "ward-203", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-3), DateTimeOffset.Now.AddDays(5)) },
        new EpisodeOfCare { PatientId = "patient-35", RoomId = "ward-203", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-5), DateTimeOffset.Now.AddDays(3)) },
        new EpisodeOfCare { PatientId = "patient-36", RoomId = "ward-203", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-1), DateTimeOffset.Now.AddDays(7)) },

        // Ward 204
        new EpisodeOfCare { PatientId = "patient-37", RoomId = "ward-204", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-4), DateTimeOffset.Now.AddDays(4)) },
        new EpisodeOfCare { PatientId = "patient-38", RoomId = "ward-204", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-2), DateTimeOffset.Now.AddDays(6)) },
        new EpisodeOfCare { PatientId = "patient-39", RoomId = "ward-204", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-7), DateTimeOffset.Now.AddDays(2)) },
        new EpisodeOfCare { PatientId = "patient-40", RoomId = "ward-204", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-3), DateTimeOffset.Now.AddDays(5)) },

        // Ward 205
        new EpisodeOfCare { PatientId = "patient-41", RoomId = "ward-205", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-6), DateTimeOffset.Now.AddDays(2)) },
        new EpisodeOfCare { PatientId = "patient-42", RoomId = "ward-205", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-1), DateTimeOffset.Now.AddDays(8)) },
        new EpisodeOfCare { PatientId = "patient-43", RoomId = "ward-205", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-4), DateTimeOffset.Now.AddDays(4)) },
        new EpisodeOfCare { PatientId = "patient-44", RoomId = "ward-205", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-2), DateTimeOffset.Now.AddDays(6)) },

        // Ward 206
        new EpisodeOfCare { PatientId = "patient-45", RoomId = "ward-206", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-5), DateTimeOffset.Now.AddDays(3)) },
        new EpisodeOfCare { PatientId = "patient-46", RoomId = "ward-206", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-3), DateTimeOffset.Now.AddDays(5)) },
        new EpisodeOfCare { PatientId = "patient-47", RoomId = "ward-206", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-9), DateTimeOffset.Now.AddDays(1)) },
        new EpisodeOfCare { PatientId = "patient-48", RoomId = "ward-206", MonitorProfileId = "profile-ward", Period = new Period(DateTimeOffset.Now.AddDays(-1), DateTimeOffset.Now.AddDays(7)) }
    };

    private readonly List<Patient> _patients = new()
    {
        // ICU 101 - 4 patients
        new Patient("patient-1", "John", "Smith", new DateTime(1965, 3, 15), "Dr. Anderson"),
        new Patient("patient-2", "Jane", "Doe", new DateTime(1978, 7, 22), "Dr. Martinez"),
        new Patient("patient-3", "Bob", "Johnson", new DateTime(1982, 11, 8), "Dr. Thompson"),
        new Patient("patient-4", "Alice", "Williams", new DateTime(1970, 5, 30), "Dr. Anderson"),

        // ICU 102 - 4 patients
        new Patient("patient-5", "Michael", "Brown", new DateTime(1955, 9, 12), "Dr. Chen"),
        new Patient("patient-6", "Sarah", "Davis", new DateTime(1990, 2, 18), "Dr. Martinez"),
        new Patient("patient-7", "David", "Miller", new DateTime(1968, 4, 25), "Dr. Thompson"),
        new Patient("patient-8", "Emma", "Wilson", new DateTime(1985, 8, 14), "Dr. Anderson"),

        // ICU 103 - 4 patients
        new Patient("patient-9", "James", "Moore", new DateTime(1972, 12, 3), "Dr. Chen"),
        new Patient("patient-10", "Olivia", "Taylor", new DateTime(1980, 6, 19), "Dr. Martinez"),
        new Patient("patient-11", "William", "Anderson", new DateTime(1963, 10, 7), "Dr. Thompson"),
        new Patient("patient-12", "Sophia", "Thomas", new DateTime(1988, 1, 28), "Dr. Anderson"),

        // ICU 104 - 4 patients
        new Patient("patient-13", "Benjamin", "Jackson", new DateTime(1975, 5, 11), "Dr. Chen"),
        new Patient("patient-14", "Isabella", "White", new DateTime(1992, 9, 16), "Dr. Martinez"),
        new Patient("patient-15", "Lucas", "Harris", new DateTime(1958, 3, 22), "Dr. Thompson"),
        new Patient("patient-16", "Mia", "Martin", new DateTime(1983, 7, 9), "Dr. Anderson"),

        // ICU 105 - 4 patients
        new Patient("patient-17", "Henry", "Garcia", new DateTime(1969, 11, 30), "Dr. Chen"),
        new Patient("patient-18", "Charlotte", "Rodriguez", new DateTime(1987, 2, 5), "Dr. Martinez"),
        new Patient("patient-19", "Alexander", "Martinez", new DateTime(1961, 6, 18), "Dr. Thompson"),
        new Patient("patient-20", "Amelia", "Robinson", new DateTime(1979, 10, 24), "Dr. Anderson"),

        // ICU 106 - 4 patients
        new Patient("patient-21", "Daniel", "Clark", new DateTime(1973, 4, 12), "Dr. Chen"),
        new Patient("patient-22", "Harper", "Lewis", new DateTime(1991, 8, 7), "Dr. Martinez"),
        new Patient("patient-23", "Matthew", "Lee", new DateTime(1966, 12, 15), "Dr. Thompson"),
        new Patient("patient-24", "Evelyn", "Walker", new DateTime(1984, 3, 29), "Dr. Anderson"),

        // Ward 201 - 4 patients
        new Patient("patient-25", "Joseph", "Hall", new DateTime(1977, 7, 21), "Dr. Chen"),
        new Patient("patient-26", "Abigail", "Allen", new DateTime(1989, 11, 4), "Dr. Martinez"),
        new Patient("patient-27", "Samuel", "Young", new DateTime(1964, 5, 17), "Dr. Thompson"),
        new Patient("patient-28", "Emily", "King", new DateTime(1981, 9, 26), "Dr. Anderson"),

        // Ward 202 - 4 patients
        new Patient("patient-29", "David", "Wright", new DateTime(1971, 1, 8), "Dr. Chen"),
        new Patient("patient-30", "Elizabeth", "Lopez", new DateTime(1993, 5, 13), "Dr. Martinez"),
        new Patient("patient-31", "Jackson", "Hill", new DateTime(1959, 9, 20), "Dr. Thompson"),
        new Patient("patient-32", "Sofia", "Scott", new DateTime(1986, 2, 2), "Dr. Anderson"),

        // Ward 203 - 4 patients
        new Patient("patient-33", "Sebastian", "Green", new DateTime(1974, 6, 14), "Dr. Chen"),
        new Patient("patient-34", "Avery", "Adams", new DateTime(1990, 10, 19), "Dr. Martinez"),
        new Patient("patient-35", "Jack", "Baker", new DateTime(1962, 2, 27), "Dr. Thompson"),
        new Patient("patient-36", "Scarlett", "Nelson", new DateTime(1988, 7, 6), "Dr. Anderson"),

        // Ward 204 - 4 patients
        new Patient("patient-37", "Owen", "Carter", new DateTime(1976, 11, 9), "Dr. Chen"),
        new Patient("patient-38", "Victoria", "Mitchell", new DateTime(1985, 3, 16), "Dr. Martinez"),
        new Patient("patient-39", "Wyatt", "Perez", new DateTime(1967, 7, 23), "Dr. Thompson"),
        new Patient("patient-40", "Grace", "Roberts", new DateTime(1992, 12, 1), "Dr. Anderson"),

        // Ward 205 - 4 patients
        new Patient("patient-41", "Luke", "Turner", new DateTime(1980, 4, 10), "Dr. Chen"),
        new Patient("patient-42", "Chloe", "Phillips", new DateTime(1987, 8, 18), "Dr. Martinez"),
        new Patient("patient-43", "Isaac", "Campbell", new DateTime(1963, 1, 25), "Dr. Thompson"),
        new Patient("patient-44", "Lily", "Parker", new DateTime(1991, 5, 5), "Dr. Anderson"),

        // Ward 206 - 4 patients
        new Patient("patient-45", "Ryan", "Evans", new DateTime(1978, 9, 12), "Dr. Chen"),
        new Patient("patient-46", "Zoey", "Edwards", new DateTime(1986, 2, 20), "Dr. Martinez"),
        new Patient("patient-47", "Nathan", "Collins", new DateTime(1960, 6, 28), "Dr. Thompson"),
        new Patient("patient-48", "Hannah", "Stewart", new DateTime(1994, 10, 3), "Dr. Anderson")
    };

    public InMemoryPatientRepository()
    {
        // Initialize navigation properties for EpisodeOfCare
        foreach (var episode in _episodesOfCare)
        {
            episode.Patient = _patients.First(p => p.Id == episode.PatientId);
            episode.Room = _rooms.First(r => r.Id == episode.RoomId);
            episode.MonitorProfile = _monitorProfiles.First(mp => mp.Id == episode.MonitorProfileId);

            // Create a deep copy of the MonitorSettings from the MonitorProfile
            episode.MonitorSettings = new Dictionary<MonitorType, MonitorSetting>();
            foreach (var kvp in episode.MonitorProfile.MonitorSettings)
            {
                episode.MonitorSettings[kvp.Key] = new MonitorSetting
                {
                    State = kvp.Value.State,
                    GraphLabel = kvp.Value.GraphLabel,
                    Unit = kvp.Value.Unit,
                    StrokeColor = kvp.Value.StrokeColor,
                    FillColor = kvp.Value.FillColor,
                    BorderColor = kvp.Value.BorderColor,
                    Alarm = new AlarmSetting
                    {
                        IsEnabled = kvp.Value.Alarm.IsEnabled,
                        NormalRange = new Range(kvp.Value.Alarm.NormalRange.Min, kvp.Value.Alarm.NormalRange.Max),
                        WarningRange = new Range(kvp.Value.Alarm.WarningRange.Min, kvp.Value.Alarm.WarningRange.Max),
                        CriticalRange = new Range(kvp.Value.Alarm.CriticalRange.Min, kvp.Value.Alarm.CriticalRange.Max),
                        WarningColor = kvp.Value.Alarm.WarningColor,
                        CriticalColor = kvp.Value.Alarm.CriticalColor
                    }
                };
            }
        }
    }

    public Task<IEnumerable<Patient>> GetAllPatientsAsync()
    {
        return Task.FromResult<IEnumerable<Patient>>(_patients);
    }

    public Task<Patient?> GetPatientByIdAsync(string patientId)
    {
        var patient = _patients.FirstOrDefault(p => p.Id == patientId);
        return Task.FromResult(patient);
    }

    public Task<IEnumerable<Patient>> GetPatientsByRoomIdAsync(string roomId)
    {
        var patientIds = _episodesOfCare.Where(e => e.RoomId == roomId).Select(e => e.PatientId);
        var patients = _patients.Where(p => patientIds.Contains(p.Id));
        return Task.FromResult<IEnumerable<Patient>>(patients);
    }

    public Task<IEnumerable<Room>> GetAllRoomsAsync()
    {
        return Task.FromResult<IEnumerable<Room>>(_rooms);
    }

    public Task<Room?> GetRoomByIdAsync(string roomId)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == roomId);
        return Task.FromResult(room);
    }

    public Task<IEnumerable<MonitorProfile>> GetAllMonitorProfilesAsync()
    {
        return Task.FromResult<IEnumerable<MonitorProfile>>(_monitorProfiles);
    }

    public Task<MonitorProfile?> GetMonitorProfileByIdAsync(string profileId)
    {
        var profile = _monitorProfiles.FirstOrDefault(p => p.Id == profileId);
        return Task.FromResult(profile);
    }

    public Task<IEnumerable<EpisodeOfCare>> GetAllEpisodesOfCareAsync()
    {
        return Task.FromResult<IEnumerable<EpisodeOfCare>>(_episodesOfCare);
    }

    public Task<EpisodeOfCare?> GetEpisodeOfCareByRoomIdAsync(string roomId)
    {
        var episode = _episodesOfCare.FirstOrDefault(e => e.RoomId == roomId);
        return Task.FromResult(episode);
    }
}
