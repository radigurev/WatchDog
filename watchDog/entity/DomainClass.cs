using System;
namespace watchDog.Entity

{
	public class DomainClass
	{
		
		public string Domain { get; set; }
		public bool IsWebsiteDown { get; set; }

        public DomainClass(string domain, bool isWebsiteDown)
		{
			this.Domain = domain;
			this.IsWebsiteDown = isWebsiteDown;

				}
		}
	
}

