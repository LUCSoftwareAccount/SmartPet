using System.Collections.Generic;
using System.Web;

namespace SmartPet.Tests
{
	public class FakeHttpSession : HttpSessionStateBase
	{
		private readonly Dictionary<string, object> _session = new Dictionary<string, object>();

		public override object this[string name]
		{
			get
			{
				return _session.ContainsKey(name) ? _session[name] : null;
			}
			set
			{
				_session[name] = value;
			}
		}
	}

	public class FakeHttpContext : HttpContextBase
	{
		private readonly HttpSessionStateBase _session = new FakeHttpSession();

		public override HttpSessionStateBase Session
		{
			get { return _session; }
		}
	}
}
