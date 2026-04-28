using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using System.Linq;
using System.Windows;

namespace FrequencyAnalysis
{
    public partial class Diagram : Window
    {
        public PlotModel PlotModel { get; set; }

        public Diagram(Dictionary<char, double> frequencies)
        {
            InitializeComponent();
            PlotModel = CreateBarChart(frequencies);
            DataContext = this;
        }

        private PlotModel CreateBarChart(Dictionary<char, double> frequencies)
        {
            var plotModel = new PlotModel { Title = "Частотный анализ букв" };

            // Сортируем данные
            var sortedFreqs = frequencies.OrderByDescending(x => x.Value).ToList();

            // Создаём серию и добавляем BarItem
            var barSeries = new BarSeries
            {
                Title = "Частота",
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0:.00}"
            };

            // Добавляем каждый элемент как BarItem
            foreach (var item in sortedFreqs)
            {
                barSeries.Items.Add(new BarItem { Value = item.Value });
            }

            // CategoryAxis на Left, LinearAxis на Bottom для горизонтальных баров[citation:5]
            var categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Left,
                Title = "Буквы",
                ItemsSource = sortedFreqs.Select(x => x.Key.ToString()).ToList()
            };

            var valueAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Частота",
                AbsoluteMinimum = 0
            };

            plotModel.Axes.Add(categoryAxis);
            plotModel.Axes.Add(valueAxis);
            plotModel.Series.Add(barSeries);

            return plotModel;
        }
    }
}