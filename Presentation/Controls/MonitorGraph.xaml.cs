using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFPatientMonitor.Presentation.Controls;

public partial class MonitorGraph : UserControl
{
    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register(
            nameof(Data),
            typeof(INotifyCollectionChanged),
            typeof(MonitorGraph),
            new PropertyMetadata(null, OnDataChanged));

    public INotifyCollectionChanged? Data
    {
        get => (INotifyCollectionChanged?)GetValue(DataProperty);
        set => SetValue(DataProperty, value);
    }

    public static readonly DependencyProperty StrokeColorProperty =
        DependencyProperty.Register(
            nameof(StrokeColor),
            typeof(Color),
            typeof(MonitorGraph),
            new PropertyMetadata(Color.FromRgb(46, 204, 113), OnStrokeColorChanged));

    public Color StrokeColor
    {
        get => (Color)GetValue(StrokeColorProperty);
        set => SetValue(StrokeColorProperty, value);
    }

    private readonly Polyline _graphLine;

    public MonitorGraph()
    {
        InitializeComponent();

        _graphLine = new Polyline
        {
            Stroke = new SolidColorBrush(StrokeColor),
            StrokeThickness = 2,
            Fill = new SolidColorBrush(Color.FromArgb(30, 46, 204, 113))
        };

        GraphCanvas.Children.Add(_graphLine);
        SizeChanged += OnSizeChanged;
    }

    private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (MonitorGraph)d;

        if (e.OldValue is INotifyCollectionChanged oldCollection)
        {
            oldCollection.CollectionChanged -= control.OnCollectionChanged;
        }

        if (e.NewValue is INotifyCollectionChanged newCollection)
        {
            newCollection.CollectionChanged += control.OnCollectionChanged;
            control.RedrawGraph();
        }
    }

    private static void OnStrokeColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (MonitorGraph)d;
        var newColor = (Color)e.NewValue;
        control._graphLine.Stroke = new SolidColorBrush(newColor);
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        RedrawGraph();
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        RedrawGraph();
    }

    private void RedrawGraph()
    {
        _graphLine.Points.Clear();

        if (Data is not System.Collections.IList data || data.Count == 0)
            return;

        var width = GraphCanvas.ActualWidth;
        var height = GraphCanvas.ActualHeight;

        if (width <= 0 || height <= 0)
            return;

        var values = new List<int>();
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i] is int value)
            {
                values.Add(value);
            }
        }

        if (values.Count == 0)
            return;

        double minValue = values.Min();
        double maxValue = values.Max();
        var range = maxValue - minValue;

        if (range < 20)
        {
            var center = (minValue + maxValue) / 2.0;
            minValue = center - 15;
            maxValue = center + 15;
            range = maxValue - minValue;
        }

        var padding = range * 0.1;
        minValue -= padding;
        maxValue += padding;
        range = maxValue - minValue;

        var pointSpacing = width / Math.Max(values.Count - 1, 1);

        for (int i = 0; i < values.Count; i++)
        {
            var value = values[i];
            var x = i * pointSpacing;
            var normalizedValue = (value - minValue) / range;
            var y = height - (normalizedValue * height);

            _graphLine.Points.Add(new Point(x, y));
        }

        if (_graphLine.Points.Count > 0)
        {
            var lastPoint = _graphLine.Points[^1];
            _graphLine.Points.Add(new Point(lastPoint.X, height));
            _graphLine.Points.Add(new Point(0, height));
        }
    }
}
