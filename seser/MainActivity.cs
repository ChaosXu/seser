using Android.App;
using Android.Widget;
using Android.OS;
using Chaosxu.Seser.Servcie;
using System.Net;
using Android.Content;
using System;
using System.Text;
using System.IO;

namespace seser
{
	[Activity (Label = "CRP", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		string FILE_CREDENTIAL = "credential";
		EditText editUser;
		EditText editPwd;
		CheckBox chkAutoLogin;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Main);

			editUser = FindViewById<EditText> (Resource.Id.editUser);
			editPwd = FindViewById<EditText> (Resource.Id.editPwd);
			chkAutoLogin = FindViewById<CheckBox> (Resource.Id.chkAutoLogin);

			var btnLogin = FindViewById<Button> (Resource.Id.btnLogin);
			btnLogin.Click += Login_Click;				

			LoadCredential ();

			if (IsAutoLogin) {
				AutoLogin ();
			}
		}

		private async void Login_Click (object sender, EventArgs e)
		{
			
			try {
				await ServiceConfig.Instance.Session.Login (editUser.Text, editPwd.Text);
				if (chkAutoLogin.Checked)
					SaveCredential ();				
				var intent = new Intent (this, typeof(HomeworkActivity));
				StartActivity (intent);
			} catch (WebException ex) {
				var dialog = new AlertDialog.Builder (this);
				dialog.SetMessage (ex.Message);
				dialog.Show ();
			}			
		}

		void AutoLogin ()
		{
			Login_Click (null, null);
		}

		void LoadCredential ()
		{
			try {
				using (var stream = OpenFileInput (FILE_CREDENTIAL)) {
					var buf = new byte[512];
					var count = stream.Read (buf, 0, buf.Length);
					var encoding = Encoding.UTF8;
					var data = new byte[count];
					Array.Copy (buf, data, count);
					var cs = encoding.GetString (data);
					var css = cs.Split ('\r');
					editUser.Text = css [0];
					editPwd.Text = css [1];
					chkAutoLogin.Checked = Boolean.Parse (css [2]);
				}					
			} catch (Java.IO.FileNotFoundException ex) {
				//ignore	
			}
		}

		void SaveCredential ()
		{
			using (var stream = OpenFileOutput (FILE_CREDENTIAL, FileCreationMode.Private)) {
				var s = String.Format ("{0}\r{1}\r{2}", editUser.Text, editPwd.Text, chkAutoLogin.Checked);
				var encoding = Encoding.UTF8;
				var buf = encoding.GetBytes (s);
				stream.Write (buf, 0, buf.Length);
			}
		}

		private bool IsAutoLogin {
			get { 
				return !String.IsNullOrEmpty (editPwd.Text) &&
				!String.IsNullOrEmpty (editUser.Text) &&
				chkAutoLogin.Checked;
			}		
		}
	}
}


