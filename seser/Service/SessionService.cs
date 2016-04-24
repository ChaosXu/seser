using System;
using System.Net;
using System.Threading.Tasks;
using System.Text;

namespace Chaosxu.Seser.Servcie
{
	public class SessionService
	{
		public SessionService ()
		{
		}

		public async Task<string> Login (string user, string pwd)
		{
			Encoding encoding = Encoding.UTF8;
			byte[] payload  =encoding.GetBytes(String.Format ("username={0}&password={1}&is_parent=on", user, pwd));

			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (URLs.Login);
			request.CookieContainer = new CookieContainer ();
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = payload.Length;
			var stream = request.GetRequestStream ();
			stream.Write (payload, 0, payload.Length);

			using (WebResponse response = await request.GetResponseAsync()) {
				HttpWebResponse httpResponse = (HttpWebResponse)response;
				return httpResponse.Cookies ["_session_id"].Value;
			}				
		}
	}
}

