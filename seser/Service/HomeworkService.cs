using System;
using System.Threading.Tasks;
using System.Text;
using System.Net;

namespace Chaosxu.Seser.Servcie
{
	public class HomeworkService:BaseService
	{
		static string URL_HOMEWORK = "http://icampus.ses.pudong-edu.sh.cn/primary/homeworks/homework_list";

		CookieContainer cookeiContainer;

		public HomeworkService (CookieContainer cookieContainer)
			: base (cookieContainer)
		{			
		}

		public async Task<string> GetByTime (DateTime time)
		{
			Encoding encoding = Encoding.UTF8;
			byte[] payload = encoding.GetBytes (String.Format ("homework%5Bdate%5D={0:yyyy-M-d}", time));

			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (URL_HOMEWORK);
			request.CookieContainer = CookieContainer;
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = payload.Length;
			var stream = request.GetRequestStream ();
			stream.Write (payload, 0, payload.Length);

			using (WebResponse response = await request.GetResponseAsync ()) {
				HttpWebResponse httpResponse = (HttpWebResponse)response;
				var length = (int)httpResponse.ContentLength;
				var buf = new byte[length];
				httpResponse.GetResponseStream ().Read (buf, 0, length);
				return encoding.GetString (buf);
			}

		}
	}

}

