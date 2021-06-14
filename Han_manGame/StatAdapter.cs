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
    public class StatAdapter : BaseAdapter<Login>
    {
        private Activity context;
        private List<Login> users;

        public StatAdapter(Activity context, List<Login> users)
        {
            this.users = users;
            this.context = context;
        }

        public override int Count
        {
            get { return users.Count; }

        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Login this[int position]
        {
            get { return users[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.stat_row_layout, null, false);
            }

            TextView textName = row.FindViewById<TextView>(Resource.Id.textName);
            TextView textWonRatio = row.FindViewById<TextView>(Resource.Id.textWonRatio);
            TextView textGameStat = row.FindViewById<TextView>(Resource.Id.textGameStat);

            textName.Text = "User: " + users[position].UserName;
            float ratio = 0;
            if(users[position].TotalGame != 0)
            {
                ratio = (users[position].TotalWin * 1.0F) / users[position].TotalGame * 100;                
            }
            ratio = (float)Math.Round(ratio * 100f) / 100f;
            textWonRatio.Text = "Win Ratio: " + ratio + "%";
            textGameStat.Text = "Win: " + users[position].TotalWin + " Total Game: " + users[position].TotalGame;

            return row;
        }
    }
}