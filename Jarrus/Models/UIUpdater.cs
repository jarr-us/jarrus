using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Jarrus.Models
{
    public class UIUpdater
    {
        delegate void SetTextCallback(Form f, Control ctrl, string text);
        delegate void SetProgressCallback(Form f, ProgressBar ctrl, double progress);
        delegate void SetChartCallback(Form f, Chart chart, List<KeyValuePair<double, double>> values, double xMin, double xMax, double yMax);

        public static void SetText(Form form, Control ctrl, string text)
        {            
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                if (form.IsDisposed) { return; }
                form.Invoke(d, new object[] { form, ctrl, text });
            }
            else
            {
                ctrl.Text = text;
            }
        }

        public static void SetProgressBar(Form form, ProgressBar ctrl, double progress)
        {
            if (ctrl.InvokeRequired)
            {
                SetProgressCallback d = new SetProgressCallback(SetProgressBar);
                if (form.IsDisposed) { return; }
                form.Invoke(d, new object[] { form, ctrl, progress });
            }
            else
            {
                ctrl.Value = (int)progress;
            }
        }

        public static void SetChart(Form form, Chart chart, List<KeyValuePair<double, double>> values, double xMin, double xMax, double yMax)
        {
            if (values == null) { return; }

            if (chart.InvokeRequired)
            {
                SetChartCallback d = new SetChartCallback(SetChart);
                if (form.IsDisposed) { return; }
                form.Invoke(d, new object[] { form, chart, values, xMin, xMax, yMax });
            }
            else
            {
                chart.ChartAreas[0].AxisX.Minimum = xMin;
                chart.ChartAreas[0].AxisX.Maximum = xMax;
                
                chart.ChartAreas[0].AxisY.Maximum = yMax;

                chart.Series["Score"].Points.Clear();

                foreach (var value in values)
                {
                    chart.Series["Score"].Points.AddXY(value.Key, value.Value);
                }
            }
        }
    }
}