using System;
using System.Net;

namespace Chaosxu.Seser.Servcie
{
	public class ServiceConfig
	{
		public static ServiceConfig Instance = new ServiceConfig();

		private CookieContainer cookieContainer = new CookieContainer();
		private SessionService sessionService  ;

		private HomeworkService homeworkService;

		private ServiceConfig ()
		{
			sessionService = new SessionService (cookieContainer);
			homeworkService = new HomeworkService (cookieContainer);
		}
			
		public SessionService Session {
			get{ return sessionService;}
		}

		public HomeworkService HomworkService {
			get{ return homeworkService; }
		}
	}
}

