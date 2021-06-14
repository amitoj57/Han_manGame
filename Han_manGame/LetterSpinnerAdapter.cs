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
   public class LetterSpinnerAdapter : BaseAdapter<string>
    {
        private List<string> letters;
        private Context context;
        public LetterSpinnerAdapter(Context c, List<string> letters)
        {
            
            this.letters = letters;
            this.context = c;
        }

        public override int Count
        {
            get { return letters.Count(); }

        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override string this[int position]
        {
            get { return letters[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.spinner_row_layout, null, false);
            }

            TextView txt1 = row.FindViewById<TextView>(Resource.Id.text1);
            txt1.Text = letters[position];
            return row;
        }

    }
}