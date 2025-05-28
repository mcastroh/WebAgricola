using Logistic.BackEnd.Data.Data;
using Logistic.BackEnd.Data.Helpers;
using Logistic.BackEnd.Data.Repositories.Interfaces;
using Logistic.Shared.DTOs;
using Logistic.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Logistic.BackEnd.Data.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly LogisticContext _context;
    private readonly DbSet<T> _entity;

    public GenericRepository(LogisticContext context)
    {
        _context = context;
        _entity = context.Set<T>();
    }

    public virtual async Task<ActionResponse<T>> AddAsync(T entity)
    {
        _context.Add(entity);

        try
        {
            await _context.SaveChangesAsync();

            return new ActionResponse<T>
            {
                IsSuccess = true,
                Result = entity
            };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionActionResponse();
        }
        catch (Exception exception)
        {
            return ExceptionActionResponse(exception);
        }
    }

    public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
    {
        var row = await _entity.FindAsync(id);

        if (row == null)
        {
            return new ActionResponse<T>
            {
                IsSuccess = false,
                Message = "ERR001"
            };
        }

        _entity.Remove(row);

        try
        {
            await _context.SaveChangesAsync();

            return new ActionResponse<T>
            {
                IsSuccess = true,
            };
        }
        catch
        {
            return new ActionResponse<T>
            {
                IsSuccess = false,
                Message = "ERR002"
            };
        }
    }

    public virtual async Task<ActionResponse<T>> GetAsync(int id)
    {
        var row = await _entity.FindAsync(id);

        if (row == null)
        {
            return new ActionResponse<T>
            {
                IsSuccess = false,
                Message = "ERR001"
            };
        }

        return new ActionResponse<T>
        {
            IsSuccess = true,
            Result = row
        };
    }

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync()
    {
        return new ActionResponse<IEnumerable<T>>
        {
            IsSuccess = true,
            Result = await _entity.ToListAsync()
        };
    }

    public virtual async Task<ActionResponse<T>> UpdateAsync(T entity)
    {
        _context.Update(entity);

        try
        {
            await _context.SaveChangesAsync();

            return new ActionResponse<T>
            {
                IsSuccess = true,
                Result = entity
            };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionActionResponse();
        }
        catch (Exception exception)
        {
            return ExceptionActionResponse(exception);
        }
    }

    private ActionResponse<T> ExceptionActionResponse(Exception exception)
    {
        return new ActionResponse<T>
        {
            IsSuccess = false,
            Message = exception.Message
        };
    }

    private ActionResponse<T> DbUpdateExceptionActionResponse()
    {
        return new ActionResponse<T>
        {
            IsSuccess = false,
            Message = "ERR003"
        };
    }

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _entity.AsQueryable();

        return new ActionResponse<IEnumerable<T>>
        {
            IsSuccess = true,
            Result = await queryable.Paginate(pagination).ToListAsync()
        };
    }

    public virtual async Task<ActionResponse<int>> GetTotalRecordsAsync()
    {
        var queryable = _entity.AsQueryable();
        double count = await queryable.CountAsync();

        return new ActionResponse<int>
        {
            IsSuccess = true,
            Result = (int)count
        };
    }
}