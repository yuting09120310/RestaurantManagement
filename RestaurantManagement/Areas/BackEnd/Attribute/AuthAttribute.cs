using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement.Areas.BackEnd.Attribute
{
	public class AuthAttribute : TypeFilterAttribute
	{
		public AuthAttribute() : base(typeof(AuthFilter))
		{

		}
	}
}
