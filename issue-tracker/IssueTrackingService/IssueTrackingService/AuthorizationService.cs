using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Data.Entity;
namespace IssueTrackingService
{
    public class AuthorizationService : OAuthAuthorizationServerProvider
    {
        // LoginTable logintable;
        //IssueTracker12Entities logindata = new IssueTracker12Entities();
        //LoginTable loginvalue = null;
        //public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        //{
        //    context.Validated();
        //}
        //public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        //{
        //    //IEnumerable<LoginTable> logintable = (from log in logindata.LoginTables.
        //    //                                      Where(x => x.UserName == context.UserName 
        //    //                                      & x.Password == context.Password 
        //    //                                      & x.IsActive==1) select log);
        //    //foreach (var value in logintable)
        //    //{
        //    //    loginvalue = value;
        //    //}

        //    //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
        //    //if (loginvalue != null)
        //    //{
        //    //    //if (loginvalue.Role == "admin")
        //    //    //{
        //    //        if (context.UserName == loginvalue.UserName && context.Password == loginvalue.Password)
        //    //        {
        //    //            identity.AddClaim(new Claim(ClaimTypes.Role, loginvalue.Role));
        //    //            identity.AddClaim(new Claim(loginvalue.UserName, loginvalue.Role));
        //    //            identity.AddClaim(new Claim(ClaimTypes.Name, loginvalue.UserName));
        //    //            context.Validated(identity);
        //    //        }
        //        //}
        //        //else
        //        //{
        //        //    if (context.UserName == loginvalue.UserName && context.Password == loginvalue.Password)
        //        //    {
        //        //        identity.AddClaim(new Claim(ClaimTypes.Role, loginvalue.Role));
        //        //        //identity.AddClaim(new Claim("username", "user"));
        //        //        identity.AddClaim(new Claim(ClaimTypes.Name, loginvalue.UserName));
        //        //        context.Validated(identity);
        //        //    }
        //        //}
        //    }
        //    else
        //    {
        //        context.SetError("error");
        //    }
        //}
    }
}