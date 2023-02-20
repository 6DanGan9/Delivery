using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exxcel = Microsoft.Office.Interop.Excel;

namespace Excel
{
    internal class ExcelHelper : IDisposable
    {
        private static char[] chars = new char[26] {'A', 'B', 'C', 'D','E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
        private Application _excel;
        private Workbook _workbook;

        public ExcelHelper() { _excel = new Exxcel.Application(); }


        internal void Open(string filePath)
        {
            _workbook = _excel.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, filePath));
        }

        internal void Set(int column, int row, Object data)
        {
            ((Exxcel.Worksheet)_excel.ActiveSheet).Cells[row, ColumnToString(column)] = data;
        }

        internal string Get(int row, int column)
        {
            var data = ((Worksheet)_excel.ActiveSheet).Cells[row, ColumnToString(column)].Text.ToString();
            return data;
        }

        public void Dispose()
        {
        }

        public void Close()
        {
            _workbook.Close();
            System.Diagnostics.Process.GetProcessesByName("EXCEL").Last().Kill();
        }

        private string ColumnToString(int column)
        {
            string Id = null;
            Stack<char> charss = new();
            while (column > 26)
            {
                column--;
                int numberChar = column % 26;
                column = (column - numberChar) / 26;
                charss.Push(chars[numberChar]);
            }
            charss.Push(chars[column - 1]);
            while (charss.Count > 0)
                Id += charss.Pop();
            return Id;
        }
    }
}
