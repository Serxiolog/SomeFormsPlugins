using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Font = DocumentFormat.OpenXml.Spreadsheet.Font;

namespace Laba
{
    public partial class TableExcel : Component
    {
        public TableExcel()
        {
            InitializeComponent();
        }

        public TableExcel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private SpreadsheetDocument? _spreadsheetDocument;

        private SharedStringTablePart? _shareStringPart;

        private Worksheet? _worksheet;

        private static void CreateStyles(WorkbookPart workbookpart)
        {
            var sp = workbookpart.AddNewPart<WorkbookStylesPart>();
            sp.Stylesheet = new Stylesheet();

            var fonts = new Fonts() { Count = 2U, KnownFonts = true };

            var fontUsual = new Font();
            fontUsual.Append(new FontSize() { Val = 12D });
            fontUsual.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Theme = 1U });
            fontUsual.Append(new FontName() { Val = "Times New Roman" });
            fontUsual.Append(new FontFamilyNumbering() { Val = 2 });
            fontUsual.Append(new FontScheme() { Val = FontSchemeValues.Minor });

            var fontTitle = new Font();
            fontTitle.Append(new Bold());
            fontTitle.Append(new FontSize() { Val = 14D });
            fontTitle.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Theme = 1U });
            fontTitle.Append(new FontName() { Val = "Times New Roman" });
            fontTitle.Append(new FontFamilyNumbering() { Val = 2 });
            fontTitle.Append(new FontScheme() { Val = FontSchemeValues.Minor });

            fonts.Append(fontUsual);
            fonts.Append(fontTitle);

            var fills = new Fills() { Count = 2U };

            var fill1 = new Fill();
            fill1.Append(new PatternFill() { PatternType = PatternValues.None });

            var fill2 = new Fill();
            fill2.Append(new PatternFill() { PatternType = PatternValues.Gray125 });

            fills.Append(fill1);
            fills.Append(fill2);

            var borders = new Borders() { Count = 2U };

            var borderNoBorder = new Border();
            borderNoBorder.Append(new LeftBorder());
            borderNoBorder.Append(new RightBorder());
            borderNoBorder.Append(new TopBorder());
            borderNoBorder.Append(new BottomBorder());
            borderNoBorder.Append(new DiagonalBorder());

            var borderThin = new Border();

            var leftBorder = new LeftBorder() { Style = BorderStyleValues.Thin };
            leftBorder.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Indexed = 64U });

            var rightBorder = new RightBorder() { Style = BorderStyleValues.Thin };
            rightBorder.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Indexed = 64U });

            var topBorder = new TopBorder() { Style = BorderStyleValues.Thin };
            topBorder.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Indexed = 64U });

            var bottomBorder = new BottomBorder() { Style = BorderStyleValues.Thin };
            bottomBorder.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Indexed = 64U });

            borderThin.Append(leftBorder);
            borderThin.Append(rightBorder);
            borderThin.Append(topBorder);
            borderThin.Append(bottomBorder);
            borderThin.Append(new DiagonalBorder());

            borders.Append(borderNoBorder);
            borders.Append(borderThin);

            var cellStyleFormats = new CellStyleFormats() { Count = 1U };
            var cellFormatStyle = new CellFormat() { NumberFormatId = 0U, FontId = 0U, FillId = 0U, BorderId = 0U };

            cellStyleFormats.Append(cellFormatStyle);

            var cellFormats = new CellFormats() { Count = 3U };
            var cellFormatFont = new CellFormat() { NumberFormatId = 0U, FontId = 0U, FillId = 0U, BorderId = 0U, FormatId = 0U, ApplyFont = true };
            var cellFormatFontCenter = new CellFormat() { NumberFormatId = 0U, FontId = 0U, FillId = 0U, BorderId = 1U, FormatId = 0U, Alignment = new Alignment() { Vertical = VerticalAlignmentValues.Center, WrapText = true, Horizontal = HorizontalAlignmentValues.Center }, ApplyFont = true};
            var cellFormatTitle = new CellFormat() { NumberFormatId = 0U, FontId = 1U, FillId = 0U, BorderId = 0U, FormatId = 0U, Alignment = new Alignment() { Vertical = VerticalAlignmentValues.Center, WrapText = true, Horizontal = HorizontalAlignmentValues.Center }, ApplyFont = true };
            var cellFormatFontTitle = new CellFormat() { NumberFormatId = 0U, FontId = 0U, FillId = 0U, BorderId = 1U, FormatId = 0U, ApplyFont = true };

            cellFormats.Append(cellFormatFont);
            cellFormats.Append(cellFormatFontCenter);
            cellFormats.Append(cellFormatTitle);
            cellFormats.Append(cellFormatFontTitle);

            var cellStyles = new CellStyles() { Count = 1U };

            cellStyles.Append(new CellStyle() { Name = "Normal", FormatId = 0U, BuiltinId = 0U });

            var differentialFormats = new DocumentFormat.OpenXml.Office2013.Excel.DifferentialFormats() { Count = 0U };

            var tableStyles = new TableStyles() { Count = 0U, DefaultTableStyle = "TableStyleMedium2", DefaultPivotStyle = "PivotStyleLight16" };

            var stylesheetExtensionList = new StylesheetExtensionList();

            var stylesheetExtension1 = new StylesheetExtension() { Uri = "{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}" };
            stylesheetExtension1.AddNamespaceDeclaration("x14", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            stylesheetExtension1.Append(new SlicerStyles() { DefaultSlicerStyle = "SlicerStyleLight1" });

            var stylesheetExtension2 = new StylesheetExtension() { Uri = "{9260A510-F301-46a8-8635-F512D64BE5F5}" };
            stylesheetExtension2.AddNamespaceDeclaration("x15", "http://schemas.microsoft.com/office/spreadsheetml/2010/11/main");
            stylesheetExtension2.Append(new TimelineStyles() { DefaultTimelineStyle = "TimeSlicerStyleLight1" });

            stylesheetExtensionList.Append(stylesheetExtension1);
            stylesheetExtensionList.Append(stylesheetExtension2);

            sp.Stylesheet.Append(fonts);
            sp.Stylesheet.Append(fills);
            sp.Stylesheet.Append(borders);
            sp.Stylesheet.Append(cellStyleFormats);
            sp.Stylesheet.Append(cellFormats);
            sp.Stylesheet.Append(cellStyles);
            sp.Stylesheet.Append(differentialFormats);
            sp.Stylesheet.Append(tableStyles);
            sp.Stylesheet.Append(stylesheetExtensionList);
        }

        private void CreateExcel(string path)
        {
            _spreadsheetDocument = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook);
            // Создаем книгу (в ней хранятся листы)
            var workbookpart = _spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            CreateStyles(workbookpart);

            // Получаем/создаем хранилище текстов для книги
            _shareStringPart = _spreadsheetDocument.WorkbookPart!.GetPartsOfType<SharedStringTablePart>().Any()
                ? _spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First()
                : _spreadsheetDocument.WorkbookPart.AddNewPart<SharedStringTablePart>();

            // Создаем SharedStringTable, если его нет
            if (_shareStringPart.SharedStringTable == null)
            {
                _shareStringPart.SharedStringTable = new SharedStringTable();
            }

            // Создаем лист в книгу
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Добавляем лист в книгу
            var sheets = _spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
            var sheet = new Sheet()
            {
                Id = _spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Лист"
            };
            sheets.Append(sheet);

            _worksheet = worksheetPart.Worksheet;
        }
        private void SaveFile()
        {
            if (_spreadsheetDocument == null)
            {
                return;
            }
            _spreadsheetDocument.WorkbookPart!.Workbook.Save();
            _spreadsheetDocument.Dispose();
        }
        public void SaveExcel<T>(string path, string title, List<T> objects, List<ExcelElements> unite, List<int> size)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(title) || objects.Count == 0 || unite.Count == 0 || size.Count == 0)
            {
                throw new ArgumentException("Null or empty arguments");
            }
            CreateExcel(path);

            var testItem = objects[0];
            foreach (var fields in unite)
            {
                if (fields.titles.Count == 1)
                {
                    if (!testItem.GetType().GetProperties().Select(y => y.ToString().Split(" ")[1]).ToList().Contains(fields.titles[0].Item2))
                    {
                        throw new Exception("Нет такого свойства");
                    }
                }
                else
                {
                    for (int i = 0; i < fields.titles.Count; i++)
                    {
                        if (!testItem.GetType().GetProperties().Select(y => y.ToString().Split(" ")[1]).ToList().Contains(fields.titles[i].Item2))
                        {
                            throw new Exception("Нет такого свойства");
                        }
                    }
                }
            }

            InsertCellInWorksheet(1, 1, title, 2);
            int ind = 1;
            for (int i = 0; i < unite.Count; i++)
            {
                if (unite[i].titles.Count == 1)
                {
                    InsertCellInWorksheet(ind, 2, unite[i].bigTitle, 1, size[ind - 1]);
                    InsertCellInWorksheet(ind, 3, "", 1);
                    MergeCells($"{GetColumnName(ind)}2", $"{GetColumnName(ind)}3");
                    ind++;
                }
                else
                {
                    InsertCellInWorksheet(ind, 2, unite[i].bigTitle, 1);
                    MergeCells($"{GetColumnName(ind)}2", $"{GetColumnName(ind + unite[i].titles.Count - 1)}2");
                    for (uint j = 0; j < unite[i].titles.Count; j++)
                    {
                        InsertCellInWorksheet(ind + (int)j, 3, unite[i].titles[(int)j].Item1, 1, size[ind + (int)j - 1]);
                        if (j != 0)
                        {
                            InsertCellInWorksheet(ind + (int)j, 2, "", 3);
                        }
                    }
                    ind += unite[i].titles.Count;
                }
            }
            ind = 4;
            foreach (var i in objects)
            {
                int letter = 1;
                foreach (var lst in unite)
                {
                    if (lst.titles.Count == 1)
                    {
                        var item = i.GetType()?.GetProperty(lst.titles[0].Item2)?.GetValue(i);
                        InsertCellInWorksheet(letter, (uint)ind, item?.ToString() ?? " ", 3);
                        letter++;
                    }
                    else
                    {
                        for (int j = 0; j < lst.titles.Count; j++)
                        {
                            var item = i.GetType()?.GetProperty(lst.titles[j].Item2)?.GetValue(i);
                            InsertCellInWorksheet(letter, (uint)ind, item?.ToString() ?? " ", 3);
                            letter++;
                        }
                    }
                }
                ind++;
            }
            
            SaveFile();
        }

        private void InsertCellInWorksheet(int ColumnIndex, uint RowIndex, string Text, uint Style, int size = -1)
        {
            string ColumnName = GetColumnName(ColumnIndex);
            string CellReference = $"{ColumnName}{RowIndex}";
            if (_worksheet == null || _shareStringPart == null)
            {
                return;
            }
            var sheetData = _worksheet.GetFirstChild<SheetData>();
            if (sheetData == null)
            {
                return;
            }

            // Ищем строку, либо добавляем ее
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex! == RowIndex).Any())
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex! == RowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = RowIndex };
                sheetData.Append(row);
            }

            if (size != -1)
            {
                SetColumnWidth((uint)ColumnIndex, size);
            }

            // Ищем нужную ячейку  
            Cell cell;
            if (row.Elements<Cell>().Where(c => c.CellReference!.Value == CellReference).Any())
            {
                cell = row.Elements<Cell>().Where(c => c.CellReference!.Value == CellReference).First();
            }
            else
            {
                // Все ячейки должны быть последовательно друг за другом расположены
                // нужно определить, после какой вставлять
                Cell? refCell = null;
                foreach (Cell rowCell in row.Elements<Cell>())
                {
                    if (string.Compare(rowCell.CellReference!.Value, CellReference, true) > 0)
                    {
                        refCell = rowCell;
                        break;
                    }
                }

                var newCell = new Cell() { CellReference = CellReference };
                row.InsertBefore(newCell, refCell);

                cell = newCell;
            }

            // вставляем новый текст
            _shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new Text(Text)));
            _shareStringPart.SharedStringTable.Save();

            cell.CellValue = new CellValue((_shareStringPart.SharedStringTable.Elements<SharedStringItem>().Count() - 1).ToString());
            cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            cell.StyleIndex = Style;

        }
        private void SetColumnWidth(uint columnIndex, double width)
        {
            var columns = _worksheet!.GetFirstChild<Columns>();
            if (columns == null)
            {
                columns = new Columns();
                _worksheet.InsertAt(columns, 0);
            }

            var existingColumn = columns.Elements<Column>()
                                        .FirstOrDefault(c => c.Min == columnIndex && c.Max == columnIndex);

            if (existingColumn != null)
            {
                existingColumn.Width = width;
                existingColumn.CustomWidth = true;
            }
            else
            {
                var column = new Column()
                {
                    Min = columnIndex,
                    Max = columnIndex,
                    Width = width,
                    CustomWidth = true
                };
                columns.Append(column);
            }
        }
        private string GetColumnName(int columnNumber)
        {
            string columnName = "";

            while (columnNumber > 0)
            {
                int remainder = (columnNumber - 1) % 26;
                columnName = (char)('A' + remainder) + columnName;
                columnNumber = (columnNumber - 1) / 26;
            }

            return columnName;
        }
        private void MergeCells(string from, string to)
        {
            string Merge = $"{from}:{to}";
            if (_worksheet == null)
            {
                return;
            }
            MergeCells mergeCells;

            if (_worksheet.Elements<MergeCells>().Any())
            {
                mergeCells = _worksheet.Elements<MergeCells>().First();
            }
            else
            {
                mergeCells = new MergeCells();

                if (_worksheet.Elements<CustomSheetView>().Any())
                {
                    _worksheet.InsertAfter(mergeCells, _worksheet.Elements<CustomSheetView>().First());
                }
                else
                {
                    _worksheet.InsertAfter(mergeCells, _worksheet.Elements<SheetData>().First());
                }
            }

            var mergeCell = new MergeCell()
            {
                Reference = new StringValue(Merge)
            };
            mergeCells.Append(mergeCell);
        }
    }
}
