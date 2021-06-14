using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Han_manGame
{
    public class GameWord
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }

        [Unique]
        public string WordText { get; set; }
    }
}