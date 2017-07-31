using System;
using Xamarin.Forms;
using AjentiExplorer.ViewModels;
using Syncfusion.SfChart.XForms;

namespace AjentiExplorer.Views
{
    public class IntervalUsage
    {
        public double Usage { get; set; }
        public int Interval { get; set; }
        public double Hour
        {
            get
            {
                return this.Interval / 2.0;
            }
        }
    };

    public class LocationDashboardsPage : BaseContentPage
    {
        private LocationDashboardsViewModel viewModel;

        public LocationDashboardsPage(LocationDashboardsViewModel viewModel)
        {
            BindingContext = this.viewModel = viewModel;

            SetBinding(TitleProperty, new Binding("Title"));

            SfChart chart = new SfChart();
            chart.Title.Text = "9 Grays Road, Fern Tree";
            chart.Title.TextColor = Settings.LightGray;
            chart.ChartBehaviors.Add(new ChartZoomPanBehavior());
            chart.BackgroundColor = Settings.Dark5;

            //Initializing Primary Axis
            var primaryAxis = new NumericalAxis();
            primaryAxis.Title.Text = DateTime.Today.AddDays(-1).ToString("D") + " 24 Hours";
            primaryAxis.Title.TextColor = Color.White;
            primaryAxis.LabelStyle.TextColor = Color.White;
            primaryAxis.Interval = 6;
            chart.PrimaryAxis = primaryAxis;

            //Initializing Secondary Axis
            var secondaryAxis = new NumericalAxis();
            secondaryAxis.Title.Text = "Usage (kWH)";
            secondaryAxis.Title.TextColor = Color.White;
            secondaryAxis.Minimum = 0;
            secondaryAxis.LabelStyle.TextColor = Color.White;
            chart.SecondaryAxis = secondaryAxis;

            // Data
            var data = new IntervalUsage[]
            {
                        new IntervalUsage { Usage=1, Interval=0 },
                        new IntervalUsage { Usage=1, Interval=1 },
                        new IntervalUsage { Usage=2, Interval=2 },
                        new IntervalUsage { Usage=3, Interval=3 },
                        new IntervalUsage { Usage=2, Interval=4 },
                        new IntervalUsage { Usage=1, Interval=5 },
                        new IntervalUsage { Usage=10, Interval=6 },
                        new IntervalUsage { Usage=12, Interval=7 },
                        new IntervalUsage { Usage=19, Interval=8 },
                        new IntervalUsage { Usage=21, Interval=9 },
                        new IntervalUsage { Usage=31, Interval=10 },
                        new IntervalUsage { Usage=41, Interval=11 },
                        new IntervalUsage { Usage=45, Interval=12 },
                        new IntervalUsage { Usage=55, Interval=13 },
                        new IntervalUsage { Usage=65, Interval=14 },
                        new IntervalUsage { Usage=66, Interval=15 },
                        new IntervalUsage { Usage=67, Interval=16 },
                        new IntervalUsage { Usage=70, Interval=17 },
                        new IntervalUsage { Usage=60, Interval=18 },
                        new IntervalUsage { Usage=80, Interval=19 },
                        new IntervalUsage { Usage=65, Interval=20 },
                        new IntervalUsage { Usage=85, Interval=21 },
                        new IntervalUsage { Usage=60, Interval=22 },
                        new IntervalUsage { Usage=70, Interval=23 },
                        new IntervalUsage { Usage=90, Interval=24 },
                        new IntervalUsage { Usage=95, Interval=25 },
                        new IntervalUsage { Usage=40, Interval=26 },
                        new IntervalUsage { Usage=45, Interval=27 },
                        new IntervalUsage { Usage=50, Interval=28 },
                        new IntervalUsage { Usage=45, Interval=29 },
                        new IntervalUsage { Usage=43, Interval=30 },
                        new IntervalUsage { Usage=43, Interval=31 },
                        new IntervalUsage { Usage=38, Interval=32 },
                        new IntervalUsage { Usage=47, Interval=33 },
                        new IntervalUsage { Usage=23, Interval=34 },
                        new IntervalUsage { Usage=25, Interval=35 },
                        new IntervalUsage { Usage=27, Interval=36 },
                        new IntervalUsage { Usage=24, Interval=37 },
                        new IntervalUsage { Usage=21, Interval=38 },
                        new IntervalUsage { Usage=18, Interval=39 },
                        new IntervalUsage { Usage=5, Interval=40 },
                        new IntervalUsage { Usage=3, Interval=41 },
                        new IntervalUsage { Usage=2, Interval=42 },
                        new IntervalUsage { Usage=1, Interval=43 },
                        new IntervalUsage { Usage=1, Interval=44 },
                        new IntervalUsage { Usage=1, Interval=45 },
                        new IntervalUsage { Usage=1, Interval=46 },
                        new IntervalUsage { Usage=1, Interval=47 },
            };
            // Spline
            SplineSeries splineSeries = new SplineSeries()
            {
                ItemsSource = data,
                XBindingPath = "Hour",
                YBindingPath = "Usage"
            };
            chart.Series.Add(splineSeries);

            this.Content = chart;
        }
    }
}