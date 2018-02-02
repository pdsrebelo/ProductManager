using System.Configuration;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace ProductManager.WebApi
{
    public class Authorization : AuthorizeAttribute
    {
        private readonly string _authorizationKey = ConfigurationManager.AppSettings["AuthorizationKey"];

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
            else if (actionContext.Request.Headers.Authorization.Parameter != _authorizationKey)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}