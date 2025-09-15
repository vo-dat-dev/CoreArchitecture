using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CoreArchitecture.Routing
{
	public class ApiPrefixConvention : IApplicationModelConvention
	{
		private readonly AttributeRouteModel _routePrefix;

		public ApiPrefixConvention(string prefix)
		{
			var normalized = (prefix ?? string.Empty).Trim('/');
			_routePrefix = new AttributeRouteModel(new RouteAttribute(normalized));
		}

		public void Apply(ApplicationModel application)
		{
			foreach (var controller in application.Controllers)
			{
				foreach (var selector in controller.Selectors)
				{
					if (selector.AttributeRouteModel != null)
					{
						selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(
							new AttributeRouteModel(_routePrefix),
							selector.AttributeRouteModel
						);
					}
					else
					{
						selector.AttributeRouteModel = new AttributeRouteModel(_routePrefix);
					}
				}
			}
		}
	}
}


