using BYBY.Infrastructure.Domain;
using BYBY.Infrastructure.UnitOfWork;
using BYBY.Repository.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BYBY.Services.Account
{
    public class UserStore : IUserPasswordStore<TBUser, int>, IUserRoleStore<TBUser, int>
    {
        readonly IRepository<TBUser, int> _userRepository;
        readonly IRepository<TBRole, int> _roleRepository;
        readonly IRepository<TBUserRole, int> _userRoleRepository;
        readonly IUnitOfWork _unitOfWork;
        //  readonly int defaultTenantId = ApplicationSettingsFactory.GetApplicationSettings().ProductTenantId;
        public UserStore(
            IRepository<TBUser, int> userRepository,
            IUnitOfWork unitOfWork,
            IRepository<TBRole, int> roleRepository,
            IRepository<TBUserRole, int> userRoleRepository
            )
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }
        public async Task CreateAsync(TBUser user)
        {
            // user.TenantId = defaultTenantId;
            await _userRepository.InsertAsync(user);
            _unitOfWork.Commit();

        }



        public async Task DeleteAsync(TBUser user)
        {
            await _userRepository.DeleteAsync(user);
            _unitOfWork.Commit();
        }


        public async Task<TBUser> FindByIdAsync(int userId)
        {

            return await _userRepository.GetAsync(userId);
        }


        public async Task<TBUser> FindByNameAsync(string userName)
        {
            return await _userRepository.FindSingleAsync(d => d.UserName == userName);

        }


        public async Task UpdateAsync(TBUser user)
        {
            await _userRepository.UpdateAsync(user);
            int rs = _unitOfWork.Commit();
            //await Task.Run(() =>
            //{
            //    _userRepository.Save(user);
            //    int rs = _unitOfWork.Commit();
            //});
            //   await _userRepository.UpdateAsync(user);

        }



        public async Task<IEnumerable<TBUser>> FindAllUser()
        {
            return await _userRepository.FindAllAsync();
        }



        #region IDisposable

        public virtual void Dispose()
        {
            //No need to dispose since using IOC.
        }



        #endregion


        #region IUserPasswordStore

        public Task SetPasswordHashAsync(TBUser user, string passwordHash)
        {
            user.Password = passwordHash;
            return Task.FromResult(0);
        }



        public Task<string> GetPasswordHashAsync(TBUser user)
        {
            return Task.FromResult(user.Password);
        }



        public Task<bool> HasPasswordAsync(TBUser user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.Password));
        }


        #endregion

        #region IUserRoleStore
        public async Task AddToRoleAsync(TBUser user, string roleName)
        {
            TBUserRole userRole = new TBUserRole();
            var roleInfo = await _roleRepository.FindSingleAsync(d => d.Name == roleName);
            userRole.RoleId = roleInfo.Id;
            userRole.UserId = user.Id;

            await _userRoleRepository.InsertAsync(userRole);
            _unitOfWork.Commit();
            await Task.FromResult(0);

        }
        public async Task<IList<string>> GetRolesAsync(TBUser user)
        {
            var roleIds = user.UserRoles.Select(d => d.RoleId);
            var roles = await _roleRepository.FindAsync(d => roleIds.Contains(d.Id));
            var roleNameList = roles.Select(d => d.Name).ToList();
            return roleNameList;
        }
        public async Task<bool> IsInRoleAsync(TBUser user, string roleName)
        {
            var roles = await GetRolesAsync(user);
            foreach (var item in roles)
            {
                if (item.Equals(roleName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return await Task.FromResult(true);
                }
            }
            return await Task.FromResult(false);
        }
        public async Task RemoveFromRoleAsync(TBUser user, string roleName)
        {
            var roleInfo = await _roleRepository.FindSingleAsync(d => d.Name == roleName);
            TBUserRole userRole = await _userRoleRepository.FindSingleAsync(d => d.UserId == user.Id && d.RoleId == roleInfo.Id);
            await _userRoleRepository.DeleteAsync(userRole);
            _unitOfWork.Commit();
        }

        #endregion

        #region IUserSecurityStampStore

        //public Task SetSecurityStampAsync(TBUser user, string stamp)
        //{
        //    user.SecurityStamp = stamp;
        //    return Task.FromResult(0);
        //}

        //public Task<string> GetSecurityStampAsync(TBUser user)
        //{
        //    return Task.FromResult(user.SecurityStamp);
        //}

        #endregion

        ///// <summary>
        ///// 修改密码（已弃用）
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <param name="hashPassword"></param>
        ///// <returns></returns>
        //public async Task ChangePassword(string userName, string hashPassword)
        //{
        //    var user = await FindByNameAsync(userName);
        //    await SetPasswordHashAsync(user, hashPassword);
        //}
    }




}
