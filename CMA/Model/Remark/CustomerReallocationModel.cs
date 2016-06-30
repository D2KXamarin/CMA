using System;

namespace CMA
{
	public class CustomerReallocationModel
	{
		public CustomerReallocationModel ()
		{
		}
	}

	public class CustomerReallocationRequestModel
	{
		public string CustomerEntityID {get; set;}
		public string PriActionStakeHolders {get; set;}
		public string SecActionStakeHolders {get; set;} 
		public string InfoActionStakeHolders {get; set;}
	}
}

