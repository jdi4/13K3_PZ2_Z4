using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
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

        public string PasswordHash
        {
            get
            {
                return this.OSO_HASLO;
            }
            set
            {
                this.OSO_HASLO = value;
            }
        }
    }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : OSOBY
    {

    }

    public partial class UserStore : 
        IQueryableUserStore<OSOBY, int>, IUserPasswordStore<OSOBY, int>, IUserLoginStore<OSOBY, int>,
        IUserRoleStore<OSOBY, int>,
        IUserEmailStore<OSOBY, int>, IUserPhoneNumberStore<OSOBY, int>

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

        public IQueryable<OSOBY> Users
        {
            get { return this.db.OSOBY; }
        }

        public Task CreateAsync(OSOBY user)
        {
            user.OSO_DATA_URODZENIA = DateTime.Now;
            user.OSO_IMIE = "grzegorz";
            user.OSO_NAZWISKO = "brzeeeeeczyczyczy";
            user.OSO_PESEL = "87965198";
            user.OSO_TELEFON = "34534534";
            this.db.OSOBY.Add(user);
            return this.db.SaveChangesAsync();
        }

        public Task DeleteAsync(OSOBY user)
        {
            this.db.OSOBY.Remove(user);
            return this.db.SaveChangesAsync();
        }

        public Task<OSOBY> FindByIdAsync(int userId)
        {
            return this.db.OSOBY.FirstOrDefaultAsync(u => u.OSO_ID.Equals(userId));
        }

        public Task<OSOBY> FindByNameAsync(string userName)
        {
            return this.db.OSOBY
                .FirstOrDefaultAsync(u => u.OSO_LOGIN == userName);
        }

        public Task UpdateAsync(OSOBY user)
        {
            this.db.Entry<OSOBY>(user).State = EntityState.Modified;
            return this.db.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.db != null)
            {
                this.db.Dispose();
            }
        }

        // IUserPasswordStore<OSOBY, int>

        public Task<string> GetPasswordHashAsync(OSOBY user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(OSOBY user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetPasswordHashAsync(OSOBY user, string passwordHash)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        // IUserRoleStore<OSOBY, int>

        public Task AddToRoleAsync(OSOBY user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(OSOBY user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.KLIENCI.Join(
                this.db.KLIENCI,
                kli => kli.KLI_ID,
                usr => usr.OSO_ID,
                (usr, kli) => kli.KLI_ID)
                != null)
            {
                return Task.FromResult<IList<string>>(new List<string> { "CLIENT" });
            }
            else
            {
                return Task.FromResult<IList<string>>(user.PRACOWNICY.Join(
                    this.db.PRACOWNICY,
                    pra => pra.PRA_ID,
                    usr => usr.OSO_ID,
                    (usr, pra) => pra.PRA_UPRAWNIENIA
                    ).ToList()
                    );
            }

            //return Task.FromResult<IList<string>>(user.Roles.Join(this.db.UserRoles, ur => ur.Id, r => r.Id, (ur, r) => r.Name).ToList());
            //return Task.FromResult<IList<string>>(new List<string>());
        }

        public Task<bool> IsInRoleAsync(OSOBY user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(OSOBY user, string roleName)
        {
            throw new NotImplementedException();
        }

        // IUserEmailStore<OSOBY, int>

        public Task<OSOBY> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(OSOBY user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(OSOBY user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(OSOBY user, string email)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(OSOBY user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        // IUserPhoneNumberStore<OSOBY, int>

        public Task<string> GetPhoneNumberAsync(OSOBY user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(OSOBY user)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberAsync(OSOBY user, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberConfirmedAsync(OSOBY user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public Task AddLoginAsync(OSOBY user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<OSOBY> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(OSOBY user)
        {
            return Task.FromResult<IList<UserLoginInfo>>(new List<UserLoginInfo> { new UserLoginInfo("local", user.OSO_LOGIN) });
        }

        public Task RemoveLoginAsync(OSOBY user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }
    }

    public class ApplicationRoles : IdentityRole
    {

    }


    //public class ApplicationDbContext : IdentityDbContext<OSOBY>
    //{
    //    public ApplicationDbContext()
    //        : base("kkbustestdbEntities")
    //    {
    //    }
    //}
}