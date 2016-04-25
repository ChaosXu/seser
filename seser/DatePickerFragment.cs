
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace seser
{
	public class DatePickerFragment : DialogFragment,DatePickerDialog.IOnDateSetListener
	{
		public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper ();

		Action<DateTime> dateSelectionedHandler = delegate {
		};

		public static DatePickerFragment NewInstance (Action<DateTime> onDateSelected)
		{
			DatePickerFragment frag = new DatePickerFragment ();
			frag.dateSelectionedHandler = onDateSelected;
			return frag;
		}

		public override Dialog OnCreateDialog (Bundle savedInstanceState)
		{
			DateTime currently = DateTime.Now;
			DatePickerDialog dialog = new DatePickerDialog (Activity, this, currently.Year, currently.Month, currently.Day);
			dialog.DatePicker.DateTime = currently;
			return dialog;
		}

		public void OnDateSet (DatePicker view, int year, int monthOfYear, int dayOfMonth)
		{
			DateTime selectedDate = new DateTime (year, monthOfYear + 1, dayOfMonth);		
			dateSelectionedHandler (selectedDate);
		}
	}
}

