using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Sudoku.Core.Models;
using Microsoft.AspNetCore.Components;

namespace Sudoku.Core.Services
{
    public partial class SudokuEngine
    {
        ConcurrentQueue<GameInterval> gameIntervals = new ConcurrentQueue<GameInterval>();
        public IEnumerable<GameInterval> Intervals => gameIntervals;
        public event EventHandler<int> OnLevelCompleted;
        public event EventHandler<int> OnLevelTimeOut;
        GameDatabase database;
        public GameDatabase Database => database;
        public SudokuEngine(GameDatabase database)
        {
            Level = 1;
            this.database = database;
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1000);
                    if (TimeUsed >= MaxMin)
                    {
                        GameOver = true;
                        GameState = GameState.Stopped;
                        GameManager();
                        ClearTable();
                        OnLevelTimeOut?.Invoke(this, Level);
                    }
                }
            });
        }
        private TimeSpan GetMaxMin(int level)
        {
            var min = level * TimeSpan.FromMinutes(1);
            return min;
        }
        public TimeSpan MaxMin { get; private set; } = TimeSpan.FromMinutes(1);
        int level;
        public int Level
        {
            get => level;
            set
            {
                //if (level != value)
                //{
                    level = value;
                    MaxMin = GetMaxMin(value);
                    if (level != 1)
                        StartGame();
                    Cells.Clear();
                    for (int x = 0; x < CellsCount; x++)
                    {
                        var row = new List<SudokuCell>();
                        for (int y = 0; y < CellsCount; y++)
                        {
                            row.Add(new SudokuCell());
                        }
                        Cells.Add(row);
                    }
                    double preInit = 0.2;
                    int totalsCells = CellsCount * CellsCount;
                    int initCells = (int)(preInit * totalsCells);
                    if (initCells < 2)
                        initCells = 2;
                    Random random = new Random();
                    List<(int Row, int Col)> cells = new List<(int, int)>();
                    for (int i = 0; i < initCells; i++)
                    {
                        bool exist;
                        (int Row, int Col) cell;
                        do
                        {
                            cell = (random.Next(0, CellsCount), random.Next(0, CellsCount));
                            exist = cells.Any(c => c.Row == cell.Row && c.Col == cell.Col);
                        } while (exist);
                        cells.Add(cell);
                    }
                    foreach(var cell in cells)
                    {
                        do
                        {
                            Cells[cell.Row][cell.Col].Value = random.Next(1, CellsCount + 1);
                        } while (Validate() != null);
                        Cells[cell.Row][cell.Col].Fixed = true;
                    }
                //}
            }
        }
        public SudokuError ValidateRow(int row)
        {
            for (int x = 0; x < CellsCount; x++)
            {
                var cell = Cells[row][x];
                var value = cell.Value;
                if (value == null)
                    continue;
                for (int xx = 0; xx < CellsCount; xx++)
                {
                    if (x == xx)
                        continue;
                    var scanCell = Cells[row][xx];
                    var scanValue = scanCell.Value;
                    if (scanValue == null)
                        continue;
                    if (value == scanValue)
                    {
                        return new SudokuError()
                        {
                            RowIndex = new int[] { row },
                            ColumnIndex = new int[] { x, xx },
                            Message = ""
                        };
                    }
                }
            }
            return null;
        }
        public SudokuError ValidateColumn(int col)
        {
            for (int y = 0; y < CellsCount; y++)
            {
                var cell = Cells[y][col];
                var value = cell.Value;
                if (value == null)
                    continue;
                for (int yy = 0; yy < CellsCount; yy++)
                {
                    if (y == yy)
                        continue;
                    var scanCell = Cells[yy][col];
                    var scanValue = scanCell.Value;
                    if (scanValue == null)
                        continue;
                    if (value == scanValue)
                    {
                        return new SudokuError()
                        {
                            RowIndex = new int[] { y, yy },
                            ColumnIndex = new[] { col },
                        };
                    }
                }
            }
            return null;
        }
        public SudokuError Validate()
        {
            for (int row = 0; row < CellsCount; row++)
            {
                var error = ValidateRow(row);
                if (error != null)
                {
                    return error;
                }
            }
            for (int col = 0; col < CellsCount; col++)
            {
                var error = ValidateColumn(col);
                if (error != null)
                {
                    return error;
                }
            }

            return null;
        }

        public string Error { get; private set; }
        public int? this[int row, int col]
        {
            get
            {
                return Cells[row][col].Value;
            }
            set
            {
                if (value != 0)
                //if (Cells[row][col].Value != 0)
                {
                    Cells[row][col].Value = value;
                    var err = Validate();
                    if (err == null)
                    {
                        Cells[row][col].Value = value;
                        Error = null;
                    }
                    else
                    {
                        Cells[row][col].Value = null;
                        if (err.ColumnIndex.Length == 2)
                        {
                            var oldColumn = err.ColumnIndex.Except(new[] { col });
                            Error = $"This value is already used at Row {row} and column {oldColumn.First()}";
                        }
                        if (err.RowIndex.Length == 2)
                        {
                            var oldRow = err.RowIndex.Except(new[] { row });
                            Error = $"This value is already used at Row {oldRow.First()} and column {col}";
                        }
                    }
                }
                else
                {
                    Cells[row][col].Value = null;
                }
                bool completed = IsCompleted();
                if (completed)
                {
                    GameManager();
                    OnLevelCompleted?.Invoke(this, Level);
                }
            }
        }

        void GameManager()
        {
            var player = database.GetUserByName(CurrentPlayerName);
            player.Scores[Level.ToString()] = GetScore();
            database.GetHighestScore(Level);
            database.SaveChanges();
        }

        //public void PrintOnBoardOnConsole(bool newGame)
        //{
        //    for (int i = 0; i < CellsCount; i++)
        //    {
        //        string print = "|";
        //        var row = Cells[i];
        //        for (int ii = 0; ii < CellsCount; ii++)
        //        {
        //            var col = row[ii];
        //            if (newGame)
        //                col.Value = null;
        //            print += col.ToString() + "|";
        //        }
        //        Console.WriteLine(print);
        //    }
        //}
        public void StopGame()
        {
            GameState = GameState.Stopped;
            gameIntervals.Clear();
            ClearTable();
            Level = 1;
        }
        public void StartGame()
        {
            GameOver = false;
            gameIntervals.Clear();
            GameState = GameState.Playing;
            var interval = new GameInterval();
            gameIntervals.Enqueue(interval);
        }
        public void Reload()
        {
            Level = Level;
            gameIntervals.Clear();
            var interval = new GameInterval();
            gameIntervals.Enqueue(interval);
        }
        public bool IsCompleted()
        {
            for (int i = 0; i < CellsCount; i++)
            {
                for (int ii = 0; ii < CellsCount; ii++)
                {
                    if (Cells[i][ii].Value == null)
                        return false;
                }
            }
            return true;

        }
        public void ToggleState()
        {
            if (GameState == GameState.Playing) //from play to pause
            {
                Pause();
            }
            else //from pause to play
            {
                Play();
            }
        }
        public void Pause()
        {
            var lastInterval = gameIntervals.Last();
            lastInterval.Pause();
            GameState = GameState.Paused;
        }
        public void Play()
        {
            var newInterval = new GameInterval();
            gameIntervals.Enqueue(newInterval);
            GameState = GameState.Playing;
        }
        public double GetScore()
        {
            double maxScore = 65;
            var timeLeft = MaxMin - TimeUsed;
            double score = 0;
            if (GameState == GameState.Stopped)
            {
                if (timeLeft >= MaxMin)
                {
                    bool completed = IsCompleted();
                    if (completed)
                        score = 35;
                    else
                        score = 0;
                }
            }
            else
            {
                score = Math.Round(timeLeft.Value.TotalSeconds * maxScore / MaxMin.TotalSeconds) + 35;
            }
            return score;
        }
        //public List<GamePlayer> GetPlayersHistory()
        //{
        //    var players = database.GetAllUsers();
        //    var history = new List<GamePlayer>();
        //    foreach (var player in players)
        //    {
        //        history.Add(player);
        //    }
        //    return history;
        //}


        public TimeSpan? TimeUsed
        {
            get
            {
                TimeSpan total = TimeSpan.FromSeconds(0);

                if (GameState == GameState.Stopped)
                {
                    if (gameIntervals != null)
                    {
                        total = TimeSpan.Zero;
                        //return null;
                    }
                    else
                        return null;
                }
                else
                {
                    foreach (var interval in gameIntervals)
                    {
                        total += interval.Duration;
                    }
                    //if (total >= MaxMin)
                    //{
                    //GameOver = true;
                    //GameState = GameState.Stopped;
                    //ClearTable();
                    //}
                }
                return total;
            }
        }
        void ClearTable()
        {
            for (int i = 0; i < CellsCount; i++)
            {
                for (int ii = 0; ii < CellsCount; ii++)
                {
                    Cells[i][ii].Value = null;
                }
            }
        }
        //public void SaveNameAndScore()
        //{
        //    GamePlayer player = new GamePlayer();
        //    player.Name = CurrentPlayerName;
        //    if (player.Name != null)
        //    {
        //        player.Score = Score;
        //        if (!File.Exists(PlayList))
        //        {
        //            File.AppendAllText(PlayList, $"NAME,SCORE \r\n");
        //        }
        //        ValidateName(player.Name);
        //        File.AppendAllText(PlayList, $"{player.Name},{player.Score}\r\n");
        //    }
        //}
        public void ValidateName(string name)
        {
            var data = File.ReadAllText(PlayList);
            var content = data.Split("\r\n");
            var contentMinusHeader = content.Skip(1);
            List<string> lines = new List<string>();
            foreach (var line in contentMinusHeader)
            {
                if (!line.Contains(name) && !string.IsNullOrEmpty(line))
                    lines.Add(line);
            }
            File.WriteAllText(PlayList, $"{content[0]} \r\n");
            foreach (var line in lines)
            {
                File.AppendAllText(PlayList, $"{line}\r\n");
            }
        }
        //    public int GetHighScore()
        //{
        //    var data = File.ReadAllText(PlayList);
        //    var line = data.Split("\r\n").Skip(1);
        //    List<int> scores = new List<int>();
        //    foreach (var i in line)
        //    {
        //        if (i == "")
        //            continue;
        //        var _score = i.Split(',').ElementAt(1);
        //        int score = int.Parse(_score);
        //        scores.Add(score);
        //    }
        //    return scores.Max();
        //}
        string name;
        public string CurrentPlayerName
        {
            get => name;
            set
            {
                name = value?.ToUpper();
                if (name != null)
                {
                    var player = database.GetUserByName(name);
                    if (player == null)
                    {
                        database.CreatePlayer(name);
                    }
                }
            }
        }
        public int GenerateNumber()
        {
            Random random = new Random();
           int number = random.Next(1, CellsCount);
            return number;
        }

        public bool GenerateRandomBool()
        {
            Random random = new Random();
            return random.NextDouble() > 0.5;
        }
        public GamePlayer CurrentPlayer => database.GetUserByName(CurrentPlayerName);

        public bool GameOver { get; private set; }
        public List<List<SudokuCell>> Cells { get; } = new List<List<SudokuCell>>();
        int count;
        public int CellsCount =>Level+1;
        
        public GameState GameState { get; private set; }
        string PlayList { get; set; } = "Playlist.csv";
    }
}
