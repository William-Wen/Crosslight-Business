using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using Business.DomainModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Business.WebApi.Modules
{
    public class BasicAuthHttpModule : IHttpModule
    {
        #region Constants

        private const string Realm = "CrosslightBusinessApp WebApi";

        #endregion

        #region Methods

        private static bool AuthenticateDefaultUser(string credentials)
        {
            bool validated = false;

            int separator = credentials.IndexOf(":", StringComparison.InvariantCulture);
            string provider = credentials.Substring(0, separator);
            string key = credentials.Substring(separator + 1);

            User user = GetUser(provider, key);
            if (user != null)
            {
                UserIdentity identity = new UserIdentity(user.UserName)
                {
                    Email = user.Email
                };

                SetPrincipal(new UserPrincipal(identity, null));
                validated = true;
            }

            return validated;
        }

        private static bool AuthenticateUser(string credentials)
        {
            bool validated = false;
            try
            {
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                credentials = encoding.GetString(Convert.FromBase64String(credentials));

                string[] parts = credentials.Split(':');
                if (parts.Length >= 3)
                {
                    string serviceId = parts[0].ToLowerInvariant();
                    string credentialParts = credentials.Substring(serviceId.Length + 1);
                    switch (serviceId.ToLower())
                    {
                        case "webapi":
                            validated = AuthenticateWebApiUser(credentialParts);
                            break;
                        default:
                            validated = AuthenticateDefaultUser(credentialParts);
                            break;
                    }
                }
            }
            catch (FormatException)
            {
                // Credentials were not formatted correctly.
                validated = false;
            }
            return validated;
        }

        private static bool AuthenticateWebApiUser(string credentials)
        {
            bool validated = false;

            int separator = credentials.IndexOf(":", StringComparison.InvariantCulture);
            string name = credentials.Substring(0, separator);
            string password = credentials.Substring(separator + 1);

            User user = GetUser(name);
            if (user != null)
            {
                UserManager<User> manager = new UserManager<User>(new UserStore<User>(new IdentityContext()));
                validated = manager.Find(name, password) != null;
                if (validated)
                {
                    UserIdentity identity = new UserIdentity(name)
                    {
                        Email = user.Email
                    };

                    SetPrincipal(new UserPrincipal(identity, null));
                }
            }

            return validated;
        }

        public void Dispose()
        {
        }

        private static User GetUser(string username)
        {
            UserManager<User> manager = new UserManager<User>(new UserStore<User>(new IdentityContext()));
            return manager.FindByName(username);
        }

        private static User GetUser(string provider, string key)
        {
            IdentityContext context = new IdentityContext();
            IdentityUserLogin userLogin = context.UserLogins.FirstOrDefault(o => o.LoginProvider == provider && o.ProviderKey == key);

            if (userLogin != null)
            {
                UserManager<User> manager = new UserManager<User>(new UserStore<User>(new IdentityContext()));
                return manager.FindById(userLogin.UserId);
            }

            return null;
        }

        public void Init(HttpApplication context)
        {
            // Register event handlers
            context.AuthenticateRequest += OnApplicationAuthenticateRequest;
            context.EndRequest += OnApplicationEndRequest;
        }

        private static void OnApplicationAuthenticateRequest(object sender, EventArgs e)
        {
            HttpRequest request = HttpContext.Current.Request;
            string authHeader = request.Headers["Authorization"];
            if (authHeader != null)
            {
                AuthenticationHeaderValue authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                if (authHeaderVal.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) && authHeaderVal.Parameter != null)
                    AuthenticateUser(authHeaderVal.Parameter);
            }
        }

        // If the request was unauthorized, add the WWW-Authenticate header to the response.
        private static void OnApplicationEndRequest(object sender, EventArgs e)
        {
            HttpResponse response = HttpContext.Current.Response;
            if (response.StatusCode == 401)
                response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", Realm));
        }

        private static void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
                HttpContext.Current.User = principal;
        }

        #endregion
    }
}