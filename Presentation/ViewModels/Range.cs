namespace WPFTest.Presentation.ViewModels;

public class Range : ViewModelBase
{
    private double _min;
    private double _max;

    public double Min
    {
        get => _min;
        set => SetProperty(ref _min, value);
    }

    public double Max
    {
        get => _max;
        set => SetProperty(ref _max, value);
    }

    public Range()
    {
    }

    public Range(double min, double max)
    {
        _min = min;
        _max = max;
    }
}
