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
    [Activity(Label = "Login Screen")]
    public class LoginActivity : AppCompatActivity
    {
        Button btnLogin, btnRegister;
        EditText etName, etPassword;
        DataSource dataSource;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_layout);

            dataSource = new DataSource(this);
            etName = FindViewById<EditText>(Resource.Id.etName);
            etPassword = FindViewById<EditText>(Resource.Id.etPassword);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);

            btnLogin.Click += BtnLogin_Click;
            btnRegister.Click += BtnRegister_Click;


        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string name = etName.Text.Trim();
            string password = etPassword.Text;
            string message = "";
            if (name.Length == 0 || password.Length == 0)
            {
                message = "Please Fill All Boxes";
            }
            else
            {
                Login user = new Login();
                user.UserName = name;
                user.Password = password;
                if (dataSource.AddNewUser(user))
                {
                    message = "User is Saved";
                    Intent intent = new Intent(this, typeof(MainActivity));
                    intent.PutExtra("name", name);
                    StartActivity(intent);
                    Finish();
                }
                else
                {
                    message = "User Storage Failure. Reason: " + dataSource.GetErrorMessage();
                }
            }
            Toast.MakeText(this, message, ToastLength.Long).Show();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string name = etName.Text.Trim();
            string password = etPassword.Text;
            string message = "";
            if (name.Length == 0 || password.Length == 0)
            {
                message = "Please Fill All Boxes";
            }
            else
            {
                if (name.Equals("game") && password.Equals("game@1234"))
                {
                    message = "Welcome To Admin!!!";
                    Intent intent = new Intent(this, typeof(AdminHomeActivity));
                    StartActivity(intent);
                    Finish();
                }
                else if (dataSource.ValidUser(name, password))
                {
                    message = "Welcome To Hangman Game Zone!!!";
                    Intent intent = new Intent(this, typeof(MainActivity));
                    intent.PutExtra("name", name);
                    StartActivity(intent);
                    Finish();
                }
                else
                {
                    message = "Invalid Name and Password Given";
                }

            }
            Toast.MakeText(this, message, ToastLength.Long).Show();
        }
    }
}