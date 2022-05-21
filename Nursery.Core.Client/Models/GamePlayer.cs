using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Sudoku.Core.Models
{
    public class GamePlayer
    {
        public string Name { get; set; }
        public Dictionary<string, double> Scores { get; set; } = new Dictionary<string, double>();
        [JsonIgnore]
        public double HighestScore => Scores.Count > 0 ? Scores.Values.Max() : 0;
        public double? GetScoreForLevel(int level)
        {
            if (Scores.ContainsKey(level.ToString()))
            {
                return Scores[level.ToString()];
            }
            return null;
        }
    }
}
