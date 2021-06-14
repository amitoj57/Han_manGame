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
    [Activity(Label = "Game Statistics")]
    public class StatActivity : AppCompatActivity
    {
        ListView list;
        Button btnBack;
        string type;
        DataSource dataSource;
        StatAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.stat_layout);

            dataSource = new DataSource(this);

            list = FindViewById<ListView>(Resource.Id.list);
            btnBack = FindViewById<Button>(Resource.Id.back);

            btnBack.Click += BtnBack_Click;

            adapter = new StatAdapter(this, dataSource.FetchAllUsers());
            list.Adapter = adapter;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}