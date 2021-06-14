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
    [Activity(Label = "Hangman Game")]
    public class GameActivity : AppCompatActivity
    {
        GameWordList gameWordList;
        DataSource dataSource;
        TextView word;
        ImageView imageHang;
        Spinner spinnerLetter;
        LetterSpinnerAdapter adapter;
        Button btnPlay, btnReset, btnBack,btnSelect;
        List<string> letters;
        int currentImage;
        string currentWord;
        string guess;
        string name;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game_layout);
            gameWordList = new GameWordList(this);
            dataSource = new DataSource(this);
            name = Intent.GetStringExtra("name");

            word = FindViewById<TextView>(Resource.Id.word);
            imageHang = FindViewById<ImageView>(Resource.Id.hang);
            spinnerLetter = FindViewById<Spinner>(Resource.Id.spinnerLetter);
            btnPlay = FindViewById<Button>(Resource.Id.play);
            btnReset = FindViewById<Button>(Resource.Id.reset);
            btnBack = FindViewById<Button>(Resource.Id.back);
            btnSelect = FindViewById<Button>(Resource.Id.btnSelect);

            btnPlay.Click += BtnPlay_Click;
            btnReset.Click += BtnReset_Click;
            btnBack.Click += BtnBack_Click;
            btnSelect.Click += BtnSelect_Click;

            Play();
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            Finish();
        }

        public void Play()
        {
            string letter_string = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            letters = new List<string>();
            for (int index = 0; index < letter_string.Length; index++)
            {
                letters.Add(letter_string[index] + "");
            }
            adapter = new LetterSpinnerAdapter(this, letters);
            spinnerLetter.Adapter = adapter;
            currentWord = gameWordList.RandomWord();
            guess = "";
            for (int index = 0; index < currentWord.Length; index++)
            {
                guess += "_";
            }
            word.Text = ConvertToResult();
            btnPlay.Enabled = false;
            btnBack.Enabled = true;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "You Reset the Game, So Lost The Game", ToastLength.Long).Show();
            dataSource.UpdateUser(name, false);
            currentImage = 0;
            btnPlay.Enabled = false;
            btnBack.Enabled = true;
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            int letterIndex = spinnerLetter.SelectedItemPosition;
            string ltr = letters[letterIndex];
            char letter = ltr[0];
            letters.RemoveAt(letterIndex);
            adapter.NotifyDataSetChanged();
            bool right = false;
            string result = "";
            for (int index = 0; index < currentWord.Length; index++)
            {
                if (currentWord[index] == letter)
                {
                    right = true;
                    result += letter;
                }
                else
                {
                    result += guess[index];
                }
            }
            guess = result;
            word.Text = ConvertToResult();
            Toast.MakeText(this, currentWord, ToastLength.Long).Show();
            if (right)
            {
                if (guess.Equals(currentWord))
                {
                    currentImage = 0;
                    Toast.MakeText(this, "You Win The Game. Good Job Done!!!", ToastLength.Long).Show();
                    dataSource.UpdateUser(name, true);
                    word.Text = "";
                    btnPlay.Enabled = true;
                    btnReset.Enabled = false;
                    imageHang.SetImageResource(Resource.Drawable.win);
                }
            }
            else
            {
                currentImage++;
                if (currentImage == 7)
                {
                    currentImage = 0;
                    Toast.MakeText(this, "You Lost the Game, So Try Again", ToastLength.Long).Show();
                    dataSource.UpdateUser(name, false);
                    word.Text = "";
                    btnPlay.Enabled = true;
                    btnReset.Enabled = false;
                }
                else
                {
                    ChangeImage();
                }

            }
        }
        public void ChangeImage()
        {
            if (currentImage >= 0 && currentImage <= 6)
            {
                int image_id = Resource.Drawable.hang0;
                switch (currentImage)
                {
                    case 1:
                        image_id = Resource.Drawable.hang1;
                        break;
                    case 2:
                        image_id = Resource.Drawable.hang2;
                        break;
                    case 3:
                        image_id = Resource.Drawable.hang3;
                        break;
                    case 4:
                        image_id = Resource.Drawable.hang4;
                        break;
                    case 5:
                        image_id = Resource.Drawable.hang5;
                        break;
                    case 6:
                        image_id = Resource.Drawable.hang6;
                        break;
                    default:
                        image_id = Resource.Drawable.hang0;
                        break;
                }
                imageHang.SetImageResource(image_id);
            }

        }

        private string ConvertToResult()
        {
            string result = "";
            for (int index = 0; index < guess.Length; index++)
            {
                result += guess[index] + " ";
            }
            return result;
        }
    }
}