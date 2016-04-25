using Android.App;
using Android.Widget;
using Android.OS;
using Chaosxu.Seser.Servcie;
using System.Net;
using Android.Content;

namespace seser
{
	[Activity (Label = "seser", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Main);

			var btnLogin = FindViewById<Button> (Resource.Id.btnLogin);
			btnLogin.Click += async (object sender, System.EventArgs e) => {
				var editUser = FindViewById<EditText> (Resource.Id.editUser);
				var editPwd = FindViewById<EditText> (Resource.Id.editPwd);

				try {
					await ServiceConfig.Instance.Session.Login (editUser.Text, editPwd.Text);
					var intent = new Intent (this, typeof(HomeworkActivity));
					StartActivity (intent);
				} catch (WebException ex) {
					var dialog = new AlertDialog.Builder (this);
					dialog.SetMessage (ex.Message);
					dialog.Show ();
				}
			};
				
		}
							
	}
}


