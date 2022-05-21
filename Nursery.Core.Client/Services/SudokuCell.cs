namespace Sudoku.Core.Services
{
    public class SudokuCell
    {
        public int? Value { get; set; }
        public bool Fixed { get; set; }
        public override string ToString()
        {
            if (Value == null)
                return "_";
            else
                return Value.ToString();
        }
    }
}
