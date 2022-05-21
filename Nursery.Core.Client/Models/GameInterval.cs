using System;

namespace Sudoku.Core.Models
{
    public class GameInterval
    {
        public GameInterval()
        {
            Start = DateTime.Now;
        }

        public void Pause()
        {
            if (Paused == null)
                Paused = DateTime.Now;
            else
                throw new InvalidOperationException("Can't call pause more than once on the game interval");
        }
        public void Stop()
        {

        }

        public DateTime Start { get; }
        public DateTime? Paused { get; private set; }

        public TimeSpan Duration => (Paused ?? DateTime.Now) - Start;

        public override string ToString()
        {
            return $"{Start} - {Paused} ({Duration})";
        }
    }
}
