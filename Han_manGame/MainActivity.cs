using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;

namespace Han_manGame
{
    [Activity(Label = "User Home", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        Button btnStart, btnStats, btnExit;
        TextView textview;
        string name;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.main_layout);
            name = Intent.GetStringExtra("name");
            btnStart = FindViewById<Button>(Resource.Id.btnStart);
            btnStats = FindViewById<Button>(Resource.Id.btnStats);
            btnExit = FindViewById<Button>(Resource.Id.btnExit);
            textview = FindViewById<TextView>(Resource.Id.textview);

            textview.Text = "Welcome " + name + "!!!";
            btnStart.Click += BtnStart_Click;
            btnExit.Click += BtnExit_Click;
            btnStats.Click += BtnStats_Click;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void BtnStats_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(StatActivity));
            StartActivity(intent);
        }


        private void BtnExit_Click(object sender, System.EventArgs e)
        {
            Finish();
        }

        private void BtnStart_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(GameActivity));
            intent.PutExtra("name", name);
            StartActivity(intent);
        }
    }
}