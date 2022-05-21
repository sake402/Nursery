namespace Sudoku.Core.Services
{
    public class SudokuError
    {
        public int[] RowIndex { get; set; }
        public int[] ColumnIndex { get; set; }
        public string Message { get; set; }
    }
}
