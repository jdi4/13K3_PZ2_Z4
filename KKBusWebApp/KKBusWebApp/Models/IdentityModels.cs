using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace KKBusWebApp.Models
{
    public partial class OSOBY : IUser<int>
    {

        public int Id
        {
            get { return this.OSO_ID; }
        }

        public string UserName
        {
            get
            {
                return this.OSO_LOGIN;
            }
            set
            {
                this.OSO_LOGIN = value;
            }
        }
    }

    public partial class UserStore : 
        IUserRoleStore<ApplicationUser, int>, IQueryableUserStore<ApplicationUser, int>, IUserPasswordStore<ApplicationUser, int>, 
        IUserLoginStore<ApplicationUser, int>, IUserClaimStore<ApplicationUser, int>, IUserSecurityStampStore<ApplicationUser, int>,
        IUserEmailStore<ApplicationUser, int>, IUserPhoneNumberStore<ApplicationUser, int>, IUserTwoFactorStore<ApplicationUser, int>,
        IUserLockoutStore<ApplicationUser, int>
    {
        private readonly sql372873Entities db;

        public UserStore(sql372873Entities db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            this.db = db;
            
        }

    }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : OSOBY
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("kkbustestdbEntities")
        {
        }
    }
}