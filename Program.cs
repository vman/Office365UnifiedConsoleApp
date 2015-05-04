using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Threading.Tasks;

namespace O365.Unified.App
{
    class Program
    {   
        //The id of your Azure tenant: 
        const string tenantID = "yourtenant.onmicrosoft.com";

        //Service root in the following format: "https://graph.microsoft.com/{version}/{tenantID}"
        const string serviceRoot = "https://graph.microsoft.com/beta/" + tenantID;
        
        const string redirectUri = "http://localhost/consoleapp";
        
        //Azure AD Authority to get the Authentication Context.
        const string authority = "https://login.microsoftonline.com/common";
        
        //Resource for which to get the access token. 
        const string resource = "https://graph.microsoft.com";
        
        //Client ID of your app 
        const string clientID = "cbd4a8fd-1401-4cd4-a7a6-ea510934651c";

        static void Main(string[] args)
        {
            GraphService client = new GraphService(new Uri(serviceRoot),() => AcquireTokenForUser());
            
            IUser user = client.Me.ExecuteAsync().Result;

            Console.WriteLine(user.displayName);

            Console.WriteLine("press any key to exit");
            
            Console.ReadKey();
        }

        private static Task<string> AcquireTokenForUser()
        {
            var authenticationContext = new AuthenticationContext(authority, false);
            
            var userAuthnResult = authenticationContext.AcquireToken(resource, clientID, new Uri(redirectUri), PromptBehavior.Auto);

            var TokenForUser = userAuthnResult.AccessToken;

            return Task.FromResult(TokenForUser);
        }
    }
}
