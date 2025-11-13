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
    /// <summary>
    /// Creates a new permission in the system
    /// </summary>
    /// <param name="model">The permission creation model containing name, module, action and description</param>
    /// <returns>A task representing the asynchronous operation</returns>
    /// <exception cref="AlreadyExistException">Thrown when a permission with the same name, module and action already exists</exception>
    /// <exception cref="ValidationException">Thrown when the model validation fails</exception>
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


    /// <summary>
    /// Updates an existing permission by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the permission to update</param>
    /// <param name="model">The permission update model containing updated values</param>
    /// <returns>A task representing the asynchronous operation</returns>
    /// <exception cref="NotFoundException">Thrown when the permission with the specified ID is not found</exception>
    /// <exception cref="AlreadyExistException">Thrown when another permission with the same name, module and action already exists</exception>
    /// <exception cref="ValidationException">Thrown when the model validation fails</exception>
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
    /// <summary>
    /// Soft deletes a permission by marking it as deleted
    /// </summary>
    /// <param name="id">The unique identifier of the permission to delete</param>
    /// <returns>A task representing the asynchronous operation</returns>
    /// <exception cref="NotFoundException">Thrown when the permission with the specified ID is not found</exception>
    public async Task DeleteAsync(int id)
    {
        var existPermission = await unitOfWork.Permissions
            .SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException("Permission is not found");

        unitOfWork.Permissions.MarkAsDeleted(existPermission);
        await unitOfWork.SaveAsync();
    }
    /// <summary>
    /// Retrieves a single permission by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the permission to retrieve</param>
    /// <returns>A task representing the asynchronous operation with the permission view model</returns>
    /// <exception cref="NotFoundException">Thrown when the permission with the specified ID is not found</exception>
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
    /// <summary>
    /// Retrieves all permissions from the system ordered by module and name
    /// </summary>
    /// <returns>A task representing the asynchronous operation with a list of permission view models</returns>
    /// <exception cref="NotFoundException">Thrown when no permissions are found in the system</exception>
    public async Task<List<PermissionViewModel>> GetAllAsync()
    {
        var permissions = await unitOfWork.Permissions
        .SelectAllAsQueryable(p => true ) 
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