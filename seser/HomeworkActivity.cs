
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Chaosxu.Seser.Servcie;
using System.Threading.Tasks;
using Android.Webkit;

namespace seser
{
	[Activity (Label = "作业")]			
	public class HomeworkActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Homework);

			var wvHomework = FindViewById<WebView> (Resource.Id.wvHomework);
			wvHomework.Settings.JavaScriptEnabled = false;
			wvHomework.Settings.DefaultTextEncodingName = "UTF-8";
			var btnDate = FindViewById<Button> (Resource.Id.btnDate);
			btnDate.Click += SelectDate;
		}

		void SelectDate (object sender, EventArgs e)
		{
			DatePickerFragment frag = DatePickerFragment.NewInstance (delegate(DateTime time) {
				var btnDate = FindViewById<Button> (Resource.Id.btnDate);
				btnDate.Text = String.Format ("{0:yyyy-M-d}", time);
				ShowHomework (time);
			});
			frag.Show (FragmentManager, DatePickerFragment.TAG);
		}

		async void ShowHomework (DateTime time)
		{
			try {
				string homework = await ServiceConfig.Instance.HomworkService.GetByTime (time);
				var wvHomework = FindViewById<WebView> (Resource.Id.wvHomework);
				wvHomework.LoadData (homework, "text/html; charset=UTF-8", null);
			} catch (Exception ex) {
				var dialog = new AlertDialog.Builder (this);
				dialog.SetMessage (ex.Message);
				dialog.Show ();
			}
		}
	}
}

