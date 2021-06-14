using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Han_manGame
{
    public class DataSource
    {
        private SQLiteConnection connection;

        private string errorMessage;

        public string GetErrorMessage()
        {
            return errorMessage;
        }

        public DataSource(Context context)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            connection = new SQLiteConnection(Path.Combine(path, "game.db"));
            try
            {
                connection.CreateTable<Login>();
                connection.CreateTable<GameWord>();
                StreamReader br = new StreamReader(context.Assets.Open("sample.txt"));
                string line;
                while ((line = br.ReadLine()) != null)
                {
                    if (line.Length >= 5 && line.Length <= 20)
                    {
                        GameWord word = new GameWord();
                        word.WordText = line.Trim().ToUpper();
                        AddNewWord(word);
                    }
                }
            }
            catch (IOException ex)
            {

            }
        }

        public List<string> FetchAllGameWords()
        {
            List<string> wordStrings = new List<string>();
            List<GameWord> words = FetchAllWords();
            foreach (GameWord word in words)
            {
                wordStrings.Add(word.WordText);
            }
            return wordStrings;
        }

        public bool AddNewWord(GameWord word)
        {
            try
            {
                connection.Insert(word);
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public bool ValidUser(string name, string password)
        {
            List<Login> users = connection.Query<Login>("Select * from Login");
            foreach (Login user in users)
            {
                if (user.UserName.Equals(name) && user.Password.Equals(password))
                {
                    return true;
                }
            }
            return false;
        }

        public bool UpdateUser(string name, bool winning)
        {
            try
            {
                var users = connection.Table<Login>();
                var user = (from u in users
                            where u.UserName == name
                               select u).Single();
                if (winning)
                {
                    user.TotalWin += 1;
                }
                user.TotalGame += 1;
                connection.Update(user);
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public bool AddNewUser(Login user)
        {
            try
            {
                connection.Insert(user);
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public List<GameWord> FetchAllWords()
        {
            List<GameWord> words = connection.Query<GameWord>("Select * from GameWord");
            return words;
        }

       
        public List<Login> FetchAllUsers()
        {
            List<Login> users = connection.Query<Login>("Select * from Login Order by TotalWin Desc,TotalGame asc");
            return users;
        }

        
    }
}
 