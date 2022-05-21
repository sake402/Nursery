using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Sudoku.Core.Models;

namespace Sudoku.Core.Services
{
    public class GameDatabase
    {
        string GetPath()
        {
            //string databaseFile;
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "database.json");
            //File.Copy("database.json", databaseFile);
            return path;
        }
        List<GamePlayer> players;
        public IEnumerable<GamePlayer> GetAllUsers()
        {
            if (!File.Exists(GetPath()))
            {
                return players ??= new List<GamePlayer>();
            }
            if (players == null)
            {
                var data = File.ReadAllText(GetPath());
                var ps = JsonSerializer.Deserialize<IEnumerable<GamePlayer>>(data);
                players = new List<GamePlayer>();
                players.AddRange(ps);
            }
            return players;
        }

        public GamePlayer GetUserByName(string name)
        {
            return GetAllUsers().SingleOrDefault(s => s.Name == name);
        }

        public GamePlayer CreatePlayer(string name)
        {
            var player = new GamePlayer() { Name = name };
            players.Add(player);
            SaveChanges();
            return player;
        }

        public void SaveChanges()
        {
            var json = JsonSerializer.Serialize(players);
            File.WriteAllText(GetPath(), json);
        }

        public GamePlayer GetHighestScorer()
        {
            GamePlayer player = null;
            double highScore = 0;
            foreach (var user in GetAllUsers())
            {
                if (user.HighestScore > highScore)
                {
                    highScore = user.HighestScore;
                    player = user;
                }
            }
            return player;
        }
        public double GetHighestScore(int level)
        {
            double highScore = 0;
            foreach (var user in GetAllUsers())
            {
                var score = user.GetScoreForLevel(level) ?? 0;
                if (score > highScore)
                {
                    highScore = score;
                }
            }
            return highScore;
        }
    }
}
