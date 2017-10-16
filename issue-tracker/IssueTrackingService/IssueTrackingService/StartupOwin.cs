using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;

[assembly: OwinStartup(typeof(IssueTrackingService.StartupOwin))]

namespace IssueTrackingService
{
    public class StartupOwin
    {
    //    public void Configuration(IAppBuilder app)
    //    {
    //        LoginTable n = new LoginTable();
    //        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
    //        app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
    //        AuthorizationService asseces = new AuthorizationService();
    //        OAuthAuthorizationServerOptions option = new OAuthAuthorizationServerOptions
    //        {
    //            AllowInsecureHttp = true,
    //            TokenEndpointPath = new PathString("/token"),
    //            AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(5),
    //            Provider = asseces
    //    };
    //        app.UseOAuthAuthorizationServer(option);
    //        app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

    //        HttpConfiguration config = new HttpConfiguration();
    //        WebApiConfig.Register(config);
    //}
}
}
