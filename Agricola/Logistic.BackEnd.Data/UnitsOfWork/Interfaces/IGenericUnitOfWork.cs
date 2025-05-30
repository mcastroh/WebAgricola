﻿using Logistic.Shared.DTOs;
using Logistic.Shared.Responses;

namespace Logistic.BackEnd.Data.UnitsOfWork.Interfaces;

public interface IGenericUnitOfWork<T> where T : class
{
    Task<ActionResponse<IEnumerable<T>>> GetAsync();

    Task<ActionResponse<T>> GetAsync(int id);

    Task<ActionResponse<T>> AddAsync(T entity);

    Task<ActionResponse<T>> UpdateAsync(T entity);

    Task<ActionResponse<T>> DeleteAsync(int id);

    Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalRecordsAsync();
}