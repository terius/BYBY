using BYBY.Infrastructure.Domain;
using BYBY.Infrastructure.UnitOfWork;
using BYBY.Repository.Entities;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace BYBY.Services.Account
{
    public class RoleStore : IRoleStore<TBRole>
    {
        readonly IRepository<TBRole, string> _roleRepository;
        readonly IUnitOfWork _unitOfWork;
      //  readonly int defaultTenantId = ApplicationSettingsFactory.GetApplicationSettings().ProductTenantId;
        public RoleStore(IRepository<TBRole, string> roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(TBRole role)
        {
          //  role.TenantId = defaultTenantId;
            await _roleRepository.InsertAsync(role);
            _unitOfWork.Commit();
        }
        public async Task DeleteAsync(TBRole role)
        {
            await _roleRepository.DeleteAsync(role);
            _unitOfWork.Commit();
        }
        public async Task<TBRole> FindByIdAsync(string roleId)
        {
            return await _roleRepository.GetAsync(roleId);
        }
        public async Task<TBRole> FindByNameAsync(string roleName)
        {
            return await _roleRepository.FindSingleAsync(d => d.Name == roleName);
        }
        public async Task UpdateAsync(TBRole role)
        {
            await _roleRepository.UpdateAsync(role);
            _unitOfWork.Commit();
        }


        #region IDisposable

        public virtual void Dispose()
        {
            //No need to dispose since using IOC.
        }



        #endregion
    }
}
