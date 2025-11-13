using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Services.Permissions.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Permissions;

public class PermissionService(
    IUnitOfWork unitOfWork,
    IValidator<PermissionCreateModel> createValidator,
    IValidator<PermissionUpdateModel> updateValidator) : IPermissionService
{
    
    public async Task CreateAsync(PermissionCreateModel model)
    {
        await createValidator.EnsureValidatedAsync(model);

        var existingPermission = await unitOfWork.Permissions
            .SelectAsync(p => p.Name.ToLower() == model.Name.ToLower()
                           && p.Module.ToLower() == model.Module.ToLower()
                           && p.Action.ToLower() == model.Action.ToLower());

        if (existingPermission != null)
            throw new AlreadyExistException($"Permission '{model.Name}' with action '{model.Action}' in module '{model.Module}' already exists");

        var permission = new Permission
        {
            Name = model.Name.Trim(),
            Module = model.Module.Trim(),
            Action = model.Action.Trim(),
            Description = model.Description?.Trim()
        };

        unitOfWork.Permissions.Insert(permission);
        await unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(int id, PermissionUpdateModel model)
    {
        await updateValidator.EnsureValidatedAsync(model);

        var existPermission = await unitOfWork.Permissions
            .SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException("Permission is not found");

        var duplicate = await unitOfWork.Permissions
            .SelectAsync(p => p.Id != id
                           && p.Name.ToLower() == model.Name.ToLower()
                           && p.Module.ToLower() == model.Module.ToLower()
                           && p.Action.ToLower() == model.Action.ToLower());

        if (duplicate != null)
            throw new AlreadyExistException($"Permission '{model.Name}' with action '{model.Action}' in module '{model.Module}' already exists");

        existPermission.Name = model.Name.Trim();
        existPermission.Module = model.Module.Trim();
        existPermission.Action = model.Action.Trim();
        existPermission.Description = model.Description?.Trim();

        unitOfWork.Permissions.Update(existPermission);
        await unitOfWork.SaveAsync();
    }
   
    public async Task DeleteAsync(int id)
    {
        var existPermission = await unitOfWork.Permissions
            .SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException("Permission is not found");

        unitOfWork.Permissions.MarkAsDeleted(existPermission);
        await unitOfWork.SaveAsync();
    }
   
    public async Task<PermissionViewModel> GetAsync(long id)
    {
        var permission = await unitOfWork.Permissions
            .SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException("Permission is not found");

        return new PermissionViewModel
        {
            Id = permission.Id,
            Name = permission.Name,
            Module = permission.Module,
            Action = permission.Action,
            Description = permission.Description,
        };
    }
    public async Task<List<PermissionViewModel>> GetAllAsync()
    {
        var permissions = await unitOfWork.Permissions
        .SelectAllAsQueryable() 
        .OrderBy(p => p.Module)
        .ThenBy(p => p.Name)
        .Select(p => new PermissionViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Module = p.Module,
            Action = p.Action,
            Description = p.Description,
        })
        .ToListAsync();

        if (!permissions.Any())
            throw new NotFoundException("No permissions found");

        return permissions;
    }
}