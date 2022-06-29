using System;
namespace watchDog.Entity

{
	public class DomainClass
	{
		
		public string domain;
		public bool isWebsiteDown;

		public DomainClass(string domain, bool isWebsiteDown)
		{
			this.domain = domain;
			this.isWebsiteDown = isWebsiteDown;

				}
		}
	
}

