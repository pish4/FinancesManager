using Microsoft.AspNet.Identity.Owin;
using FinancesManager.DataProvider.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace FinancesManager.Controllers
{
    public class BaseApiController : ApiController
    {
        private ApplicationUser _member;

        public ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        public ApplicationUser UserRecord
        {
            get
            {
                var user = UserManager.FindByNameAsync(User.Identity.Name);
                return user.Result;
            }
        }

       
    }
}