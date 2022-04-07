using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EducationSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EducationSystem.Helpers
{
    public class ApplicationManager : UserManager<ApplicationUser>
    {
        public ApplicationManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }
        //public static ApplicationManager Create(IdentityFactoryOptions<ApplicationManager> options,
        //    IOwinContext context)
        //{
        //    var db = context.Get<EducationSystemDB>();
        //    ApplicationManager manager = new ApplicationManager(new UserStore<ApplicationUser>(db));
        //    return manager;
        //}
    }
}