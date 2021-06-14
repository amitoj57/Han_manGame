using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Han_manGame
{
    [Activity(Label = "Admin Home")]
    public class AdminHomeActivity : AppCompatActivity
    {
        Button btnSave, btnExit;
        EditText etWord;
        DataSource dataSource;
        ListView list;
        List<string> words;
        ArrayAdapter<string> adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.admin_layout);
            dataSource = new DataSource(this);
            words = dataSource.FetchAllGameWords();

            btnExit = FindViewById<Button>(Resource.Id.btnExit);
            btnSave = FindViewById<Button>(Resource.Id.btnSave);
            etWord = FindViewById<EditText>(Resource.Id.etWord);
            list = FindViewById<ListView>(Resource.Id.list);

            btnExit.Click += BtnExit_Click;
            btnSave.Click += BtnSave_Click;

            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, words);
            list.Adapter = adapter;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string word = etWord.Text.Trim().ToUpper();
            string message = "";
            if (word.Length == 0)
            {
                message = "Please Fill All Boxes";
            }
            else
            {
                GameWord gameWord = new GameWord();
                gameWord.WordText =word;
                if (dataSource.AddNewWord(gameWord))
                {
                    message = "New Word is Store";
                    words.Add(word);
                    adapter.NotifyDataSetChanged();
                }
                else
                {
                    message = "We have already this word in our List";
                }
            }
            Toast.MakeText(this, message, ToastLength.Long).Show();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}