using System;
using System.Collections.Generic;
using System.ComponentModel;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml;

namespace Laba
{
    public partial class LinearExcel : Component
    {
        public LinearExcel()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            InitializeComponent();
        }

        public LinearExcel(IContainer container)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            container.Add(this);

            InitializeComponent();
        }

        public void CreateExcel(string fileName, string documentTitle, string chartTitle, LegendPositionEnum legendPosition, List<DiagInfo> dataSeries)
        {
            // Проверка на заполненность входных данных
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(documentTitle) || string.IsNullOrEmpty(chartTitle) || dataSeries == null || dataSeries.Count == 0)
            {
                throw new ArgumentException("Все входные данные должны быть заполнены.");
            }

            // Создание нового Excel-документа
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("ChartSheet");

                // Заполнение данных в таблице
                int row = 1;
                foreach (var series in dataSeries)
                {
                    worksheet.Cells[row, 1].Value = series.Title;
                    for (int i = 0; i < series.nums.Count; i++)
                    {
                        worksheet.Cells[row, i + 2].Value = series.nums[i].Item2;
                    }
                    row++;
                }

                // Добавление диаграммы
                var chart = worksheet.Drawings.AddChart("LineChart", eChartType.Line);
                chart.Title.Text = chartTitle;

                // Настройка легенды
                switch (legendPosition)
                {
                    case LegendPositionEnum.Down:
                        chart.Legend.Position = eLegendPosition.Bottom;
                        break;
                    case LegendPositionEnum.Up:
                        chart.Legend.Position = eLegendPosition.Top;
                        break;
                    case LegendPositionEnum.Left:
                        chart.Legend.Position = eLegendPosition.Left;
                        break;
                    case LegendPositionEnum.Right:
                        chart.Legend.Position = eLegendPosition.Right;
                        break;
                }

                // Установка данных для диаграммы
                for (int i = 0; i < dataSeries.Count; i++)
                {
                    var seriesData = dataSeries[i];
                    var yValues = seriesData.nums.Select(p => p.Item2).ToList();

                    var yRange = worksheet.Cells[i + 1, 2, i + 1, 1 + yValues.Count];

                    var seriesChart = chart.Series.Add(yRange);
                    seriesChart.Header = seriesData.Title;

                    

                }

                // Установка размеров диаграммы
                chart.SetPosition(5, 0, 5, 0);
                chart.SetSize(800, 400);

                // Сохранение документа
                FileInfo file = new FileInfo(fileName);
                package.SaveAs(file);
            }
        }
    }
}
