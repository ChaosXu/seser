using System;
using System.Net;

namespace Chaosxu.Seser.Servcie
{
	public abstract class BaseService
	{
		CookieContainer cookieContainer;

		public BaseService (CookieContainer cookieContainer)
		{
			this.cookieContainer = cookieContainer;
		}

		protected CookieContainer CookieContainer {
			get {
				return cookieContainer;
			}
		}
	}
}

