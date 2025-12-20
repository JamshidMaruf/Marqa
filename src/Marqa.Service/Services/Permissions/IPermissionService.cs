﻿using Marqa.Service.Exceptions;
 using Marqa.Service.Services;
 using Marqa.Service.Services.Permissions.Models;

 namespace Marqa.Service.Services.Permissions;
public interface IPermissionService : IScopedService
{
    /// <summary>
    /// Creates a new permission in the system
    /// </summary>
    /// <param name="model">The permission creation model containing name, module, action and description</param>
    /// <returns>A task representing the asynchronous operation</returns>
    /// <exception cref="AlreadyExistException">Thrown when a permission with the same name, module and action already exists</exception>
    /// <exception cref="ValidationException">Thrown when the model validation fails</exception>
    Task CreateAsync(PermissionCreateModel model);

    /// <summary>
    /// Updates an existing permission by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the permission to update</param>
    /// <param name="model">The permission update model containing updated values</param>
    /// <returns>A task representing the asynchronous operation</returns>
    /// <exception cref="NotFoundException">Thrown when the permission with the specified ID is not found</exception>
    /// <exception cref="AlreadyExistException">Thrown when another permission with the same name, module and action already exists</exception>
    /// <exception cref="ValidationException">Thrown when the model validation fails</exception>
    Task UpdateAsync(int id, PermissionUpdateModel model);

    /// <summary>
    /// Soft deletes a permission by marking it as deleted
    /// </summary>
    /// <param name="id">The unique identifier of the permission to delete</param>
    /// <returns>A task representing the asynchronous operation</returns>
    /// <exception cref="NotFoundException">Thrown when the permission with the specified ID is not found</exception>
    Task DeleteAsync(int id);

    /// <summary>
    /// Retrieves a single permission by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the permission to retrieve</param>
    /// <returns>A task representing the asynchronous operation with the permission view model</returns>
    /// <exception cref="NotFoundException">Thrown when the permission with the specified ID is not found</exception>
    Task<PermissionViewModel> GetAsync(long id);

    /// <summary>
    /// Retrieves all permissions from the system ordered by module and name
    /// </summary>
    /// <returns>A task representing the asynchronous operation with a list of permission view models</returns>
    /// <exception cref="NotFoundException">Thrown when no permissions are found in the system</exception>
    Task<List<PermissionViewModel>> GetAllAsync();
}

