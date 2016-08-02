using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Business.DomainModel;
using Intersoft.Data.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Business.WebApi.Controllers
{
    [EntityController(AllowMultipartMedia = true)]
    public class IdentityController : ApiController
    {
        #region Fields

        private IdentityContext _context;
        private UserManager<User> _manager;

        #endregion

        #region Properties

        public IdentityContext Context
        {
            get { return this._context ?? (this._context = this.CreateContext()); }
        }

        public UserManager<User> Manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = new UserManager<User>(new UserStore<User>(this.Context));
                    _manager.UserValidator = new UserValidator<User>(_manager)
                    {
                        AllowOnlyAlphanumericUserNames = false
                    };
                }

                return _manager;
            }
        }

        #endregion

        #region Methods

        public IdentityContext CreateContext()
        {
            return new IdentityContext();
        }

        #endregion

        #region Actions

        [HttpGet]
        [Authorize]
        public virtual User GetUserProfile(string userName)
        {
            User user = this.Context.Users.Where(o => o.UserName == userName).Include("UserDetail").Include("Logins").Include("Claims").Include("Roles").Include("Roles.Role").FirstOrDefault();

            if (user != null)
                return user;

            throw new Exception("Username doesn't exist");
        }

        [HttpPost]
        [Authorize]
        public virtual User GetUserProfile(IdentityUserLogin identity)
        {
            IdentityUserLogin userLogin = this.Context.UserLogins.FirstOrDefault(o => o.LoginProvider == identity.LoginProvider && o.ProviderKey == identity.ProviderKey);
            if (userLogin != null)
            {
                User identityUser = this.Manager.FindById(userLogin.UserId);
                if (identityUser != null)
                {
                    User user = this.Context.Users.Where(o => o.Id == identityUser.Id).Include("UserDetail").Include("Logins").Include("Claims").Include("Roles").Include("Roles.Role").FirstOrDefault();

                    return user;
                }
            }

            throw new Exception("Username doesn't exist");
        }

        [HttpGet]
        [Authorize]
        public virtual List<IdentityRole> GetRoles()
        {
            return this.Context.Roles.ToList();
        }

        [HttpGet]
        public virtual HttpResponseMessage Login(string userName, string password)
        {
            try
            {
                User user = this.GetUserByUserName(userName);
                if (user != null)
                {
                    if (this.Manager.Find(userName, password) != null)
                        return this.OnLogin(user);

                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect password.");
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, "User does not exist.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.ToString());
            }
        }

        [HttpPost]
        public virtual HttpResponseMessage Login(IdentityUserLogin identity)
        {
            try
            {
                IdentityUserLogin userLogin = this.Context.UserLogins.FirstOrDefault(o => o.LoginProvider == identity.LoginProvider && o.ProviderKey == identity.ProviderKey);
                if (userLogin != null)
                {
                    User user = this.Manager.FindById(userLogin.UserId);
                    if (user != null)
                        return this.OnLogin(user);

                    return Request.CreateResponse(HttpStatusCode.BadRequest, "User does not exist.");
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, "User does not exist.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.ToString());
            }
        }

        [HttpGet]
        [Authorize]
        public virtual HttpResponseMessage Logout(string userName)
        {
            try
            {
                User user = this.GetUserByUserName(userName);
                if (user != null)
                    return this.OnLogout(user);

                return Request.CreateResponse(HttpStatusCode.BadRequest, "User does not exist.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.ToString());
            }
        }

        [HttpPost]
        public virtual HttpResponseMessage Register(MultipartData registrationPartData)
        {
            try
            {
                RegistrationData registrationData = registrationPartData.Data.ToObject<RegistrationData>();
                FileData imageFile = null;

                if (registrationPartData.Files != null && registrationPartData.Files.Any())
                    imageFile = registrationPartData.Files.FirstOrDefault();

                if (registrationData.UserLogins != null)
                    return this.RegisterUserLogin(registrationData);

                if (!string.IsNullOrEmpty(registrationData.UserName))
                    return this.RegisterUser(registrationData, imageFile);

                return Request.CreateResponse(HttpStatusCode.BadRequest, "Incomplete registration data");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.ToString());
            }
        }

        [HttpGet]
        [Authorize]
        public virtual HttpResponseMessage ChangePassword(string userName, string currentPassword, string newPassword)
        {
            try
            {
                User user = this.Manager.Find(userName, currentPassword);
                if (user != null)
                {
                    IdentityResult result = this.Manager.ChangePassword(user.Id, currentPassword, newPassword);
                    if (result.Succeeded)
                        return Request.CreateResponse(HttpStatusCode.OK);

                    return Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, "Your current password is incorrect.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.ToString());
            }
        }

        [HttpPost]
        [Authorize]
        public virtual User UpdateUserProfile(MultipartData multipartData)
        {
            User user = multipartData.Data.ToObject<User>();
            IdentityContext context = this.CreateContext();
            User currentUser = context.Users.FirstOrDefault(o => o.Id == user.Id);

            if (currentUser != null)
            {
                List<IdentityUserLogin> userLogins = new List<IdentityUserLogin>();
                List<IdentityUserClaim> userClaims = new List<IdentityUserClaim>();
                List<IdentityUserRole> userRoles = new List<IdentityUserRole>();

                if (user.Logins != null)
                    userLogins = context.UserLogins.Where(o => o.UserId == user.Id).ToList();

                if (user.Claims != null)
                    userClaims = context.UserClaims.Where(o => o.User.Id == user.Id).ToList();

                if (user.Roles != null)
                    userRoles = context.UserRoles.Where(o => o.UserId == user.Id).ToList();

                // force password back to current
                user.PasswordHash = currentUser.PasswordHash;

                this.Context.Entry(user).State = System.Data.Entity.EntityState.Modified;

                if (user.UserDetail != null)
                    this.Context.Entry(user.UserDetail).State = System.Data.Entity.EntityState.Modified;

                if (user.Logins != null)
                {
                    foreach (IdentityUserLogin userLogin in user.Logins)
                    {
                        IdentityUserLogin login = userLogins.FirstOrDefault(o => o.LoginProvider == userLogin.LoginProvider && o.ProviderKey == userLogin.ProviderKey);
                        if (login != null)
                        {
                            userLogins.Remove(login);
                            this.Context.Entry(userLogin).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                            this.Context.Entry(userLogin).State = System.Data.Entity.EntityState.Added;
                    }

                    foreach (IdentityUserLogin userLogin in userLogins)
                    {
                        IdentityUserLogin login = this.Context.UserLogins.FirstOrDefault(o => o.UserId == user.Id && o.LoginProvider == userLogin.LoginProvider && o.ProviderKey == userLogin.ProviderKey);
                        this.Context.UserLogins.Remove(login);
                    }
                }

                if (user.Claims != null)
                {
                    foreach (IdentityUserClaim userClaim in user.Claims)
                    {
                        IdentityUserClaim claim = userClaims.FirstOrDefault(o => o.ClaimType == userClaim.ClaimType && o.ClaimValue == userClaim.ClaimValue);
                        if (claim != null)
                        {
                            userClaims.Remove(claim);
                            this.Context.Entry(userClaim).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                            this.Context.Entry(userClaim).State = System.Data.Entity.EntityState.Added;
                    }

                    foreach (IdentityUserClaim userClaim in userClaims)
                    {
                        IdentityUserClaim claim = this.Context.UserClaims.FirstOrDefault(o => o.User.Id == user.Id && o.ClaimType == userClaim.ClaimType && o.ClaimValue == userClaim.ClaimValue);
                        this.Context.UserClaims.Remove(claim);
                    }
                }


                if (user.Roles != null)
                {
                    foreach (IdentityUserRole userRole in user.Roles)
                    {
                        IdentityUserRole role = userRoles.FirstOrDefault(o => o.RoleId == userRole.RoleId);
                        if (role != null)
                            userRoles.Remove(role);
                        else
                            this.Context.Entry(userRole).State = System.Data.Entity.EntityState.Added;
                    }

                    foreach (IdentityUserRole userRole in userRoles)
                    {
                        IdentityUserRole role = this.Context.UserRoles.FirstOrDefault(o => o.User.Id == user.Id && o.RoleId == userRole.RoleId);
                        this.Context.UserRoles.Remove(role);
                    }
                }

                if (multipartData.Files != null && multipartData.Files.Any())
                    multipartData.Files.FirstOrDefault().Save(HttpContext.Current.Server.MapPath("~/userfiles/" + user.UserDetail.ImageUrl));

                this.Context.SaveChanges();
            }

            return user;
        }

        #endregion

        #region Protected

        protected virtual string GenerateUserId()
        {
            return Guid.NewGuid().ToString();
        }

        protected virtual HttpResponseMessage OnLogin(User user)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        protected virtual HttpResponseMessage OnLogout(User user)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        protected virtual string ResolveName(string userName)
        {
            User user = this.Context.Users.FirstOrDefault(o => o.UserName == userName);
            while (user != null)
            {
                userName += "1";
                user = this.Context.Users.FirstOrDefault(o => o.UserName == userName);
            }

            return userName;
        }

        #endregion

        #region Private

        private User GetUserByUserName(string userName)
        {
            return this.Manager.FindByName(userName);
        }

        private User GetUserByEmail(string email)
        {
            return this.Context.Users.FirstOrDefault(o => o.Email == email);
        }

        private bool IsEmailExist(string email)
        {
            return this.GetUserByEmail(email) != null;
        }

        private bool IsUserExist(string userName)
        {
            return this.GetUserByUserName(userName) != null;
        }

        private HttpResponseMessage RegisterUser(RegistrationData registrationData, FileData imageFile = null)
        {
            if (this.IsEmailExist(registrationData.Email))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Account already exists for your email address.");

            if (this.IsUserExist(registrationData.UserName))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Username already exists.");

            string userId = this.GenerateUserId();

            User user = new User
            {
                Id = userId,
                UserName = registrationData.UserName,
                PasswordHash = registrationData.PasswordHash,
                Email = registrationData.Email,
                UserDetail = new UserDetail
                {
                    UserId = userId,
                    FirstName = registrationData.FirstName,
                    LastName = registrationData.LastName,
                    BirthDate = registrationData.BirthDate,
                    ImageUrl = registrationData.ImageUrl
                }
            };

            if (registrationData.UserRoles != null)
            {
                foreach (string role in registrationData.UserRoles)
                {
                    IdentityUserRole userRole = new IdentityUserRole
                    {
                        RoleId = role,
                        UserId = userId
                    };

                    user.Roles.Add(userRole);
                }
            }

            if (registrationData.UserLogins != null)
            {
                foreach (KeyValuePair<string, string> keyPair in registrationData.UserLogins)
                {
                    IdentityUserLogin userLogin = new IdentityUserLogin
                    {
                        LoginProvider = keyPair.Key,
                        ProviderKey = keyPair.Value
                    };

                    user.Logins.Add(userLogin);
                }
            }

            if (registrationData.UserClaims != null)
            {
                foreach (KeyValuePair<string, string> keyPair in registrationData.UserClaims)
                {
                    IdentityUserClaim userClaim = new IdentityUserClaim
                    {
                        ClaimType = keyPair.Key,
                        ClaimValue = keyPair.Value
                    };

                    user.Claims.Add(userClaim);
                }
            }

            IdentityResult result = this.Manager.Create(user, registrationData.PasswordHash);
            if (result.Succeeded && imageFile != null)
                imageFile.Save(HttpContext.Current.Server.MapPath("~/userfiles/" + user.UserDetail.ImageUrl));

            if (result.Succeeded)
                return Request.CreateResponse(HttpStatusCode.Created);

            return Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors);
        }

        private HttpResponseMessage RegisterUserLogin(RegistrationData registrationData)
        {
            foreach (KeyValuePair<string, string> keyPair in registrationData.UserLogins)
            {
                IdentityUserLogin userLogin = this.Context.UserLogins.FirstOrDefault(o => o.LoginProvider == keyPair.Key && o.ProviderKey == keyPair.Value);
                if (userLogin != null)
                    return Request.CreateResponse(HttpStatusCode.OK, "Account already exists.");
            }

            string userId = this.GenerateUserId();

            User user = new User
            {
                UserName = this.ResolveName(registrationData.UserName),
                Id = userId,
                Email = registrationData.Email,
                UserDetail = new UserDetail
                {
                    FirstName = registrationData.FirstName,
                    LastName = registrationData.LastName,
                    ImageUrl = registrationData.ImageUrl
                }
            };

            if (registrationData.UserLogins != null)
            {
                foreach (KeyValuePair<string, string> keyPair in registrationData.UserLogins)
                {
                    IdentityUserLogin userLogin = new IdentityUserLogin
                    {
                        LoginProvider = keyPair.Key,
                        ProviderKey = keyPair.Value
                    };

                    user.Logins.Add(userLogin);
                }
            }

            IdentityResult result = this.Manager.Create(user);
            if (result.Succeeded)
                return Request.CreateResponse(HttpStatusCode.Created);
            return Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors);
        }

        #endregion
    }
}