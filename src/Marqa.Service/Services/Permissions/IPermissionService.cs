namespace Marqa.Service.Services.Permissions.Models;
public interface IPermissionService
{
    Task CreateAsync(PermissionCreateModel model);
    Task UpdateAsync(int id, PermissionUpdateModel model);
    Task DeleteAsync(int id);
    Task<PermissionViewModel> GetAsync(long id);
    Task<List<PermissionViewModel>> GetAllAsync();
    
}

