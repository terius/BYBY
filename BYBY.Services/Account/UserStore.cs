﻿using BYBY.Infrastructure.Domain;
using BYBY.Infrastructure.UnitOfWork;
using BYBY.Repository.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sceneray.CSCenter.AppService.SSUser
{
    public class UserStore : IUserPasswordStore<TBUser, int>, IUserRoleStore<TBUser,int>, IUserSecurityStampStore<TBUser,int>
    {
        //readonly IRepository<TBUser,int> _userRepository;
        //readonly IRepository<TBRole, int> _roleRepository;
        //readonly IRepository<AbpUserRoles, int> _userRoleRepository;
        readonly IUnitOfWork _unitOfWork;
      //  readonly int defaultTenantId = ApplicationSettingsFactory.GetApplicationSettings().ProductTenantId;
        public UserStore(
            IRepository<TBUser,int> userRepository,
            IUnitOfWork unitOfWork,
            IRepository<TBRole, int> roleRepository,
            IRepository<AbpUserRoles, int> userRoleRepository
            )
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }
        public async Task CreateAsync(TBUser user)
        {
            user.TenantId = defaultTenantId;
            await _userRepository.InsertAsync(user);
            _unitOfWork.Commit();

        }



        public async Task DeleteAsync(TBUser user)
        {
            await _userRepository.DeleteAsync(user);
            _unitOfWork.Commit();
        }


        public async Task<TBUser> FindByIdAsync(long userId)
        {

            return await Task.FromResult(_userRepository.FindBy(userId));
        }


        public async Task<TBUser> FindByNameAsync(string userName)
        {
            return await Task.FromResult(_userRepository.GetObjectSet().FirstOrDefault(d => d.UserName == userName && d.TenantId == defaultTenantId));

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



        public List<TBUser> FindAllUser()
        {
            return _userRepository.GetObjectSet().ToList();
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
            AbpUserRoles userRole = new AbpUserRoles();
            var roleInfo = _roleRepository.GetObjectSet().FirstOrDefault(d => d.Name == roleName && d.TenantId == defaultTenantId);
            userRole.RoleId = roleInfo.Id;
            userRole.UserId = user.Id;
            userRole.CreationTime = DateTime.Now;
            userRole.TenantId = defaultTenantId;
            _userRoleRepository.Add(userRole);
            _unitOfWork.Commit();
            await Task.FromResult(0);

        }
        public async Task<IList<string>> GetRolesAsync(TBUser user)
        {
            var roleIds = user.AbpUserRoles.Select(d => d.RoleId);
            IList<string> roles = _roleRepository.GetObjectSet().Where(d => roleIds.Contains(d.Id)).Select(d => d.Name).ToList();
            return await Task.FromResult(roles);
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
            var roleInfo = _roleRepository.GetObjectSet().FirstOrDefault(d => d.Name == roleName && d.TenantId == defaultTenantId);
            AbpUserRoles userRole = _userRoleRepository.GetObjectSet().FirstOrDefault(d => d.UserId == user.Id
            && d.RoleId == roleInfo.Id && d.TenantId == defaultTenantId);
            _userRoleRepository.Remove(userRole);
            _unitOfWork.Commit();
            await Task.FromResult(0);
        }

        #endregion

        #region IUserSecurityStampStore

        public Task SetSecurityStampAsync(TBUser user, string stamp)
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(TBUser user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

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
