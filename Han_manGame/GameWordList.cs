using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Han_manGame
{
    public class GameWordList
    {
        List<string> gameWords;
        DataSource dataSource;
        
        public GameWordList(Context context)
        {
            try
            {
                dataSource = new DataSource(context);
                gameWords = new List<string>();
                gameWords = dataSource.FetchAllGameWords();
            }
            catch (Exception ex) 
            { 
            }
        }

        public string RandomWord()
        {
            Random random = new Random();
            int index = random.Next(gameWords.Count());
            string word = gameWords[index];
            if (word.Count() <= 15)
                return word;
            else
                return RandomWord();
        }
    }
}