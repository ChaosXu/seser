using System;

namespace Chaosxu.Seser.Servcie
{
	public class ServiceConfig
	{
		public static ServiceConfig Instance = new ServiceConfig();

		private SessionService session  = new SessionService();

		private ServiceConfig ()
		{
		}
			
		public SessionService Session {
			get{ return session;}
		}
	}
}

