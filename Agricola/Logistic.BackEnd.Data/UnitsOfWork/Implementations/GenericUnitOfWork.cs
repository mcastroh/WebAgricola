using Logistic.BackEnd.Data.Repositories.Interfaces;
using Logistic.BackEnd.Data.UnitsOfWork.Interfaces;
using Logistic.Shared.DTOs;
using Logistic.Shared.Responses;

namespace Logistic.BackEnd.Data.UnitsOfWork.Implementations;

public class GenericUnitOfWork<T> : IGenericUnitOfWork<T> where T : class
{
    private readonly IGenericRepository<T> _repository;

    public GenericUnitOfWork(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync() => await _repository.GetAsync();

    public virtual async Task<ActionResponse<T>> GetAsync(int id) => await _repository.GetAsync(id);

    public virtual async Task<ActionResponse<T>> AddAsync(T model) => await _repository.AddAsync(model);

    public virtual async Task<ActionResponse<T>> UpdateAsync(T model) => await _repository.UpdateAsync(model);

    public virtual async Task<ActionResponse<T>> DeleteAsync(int id) => await _repository.DeleteAsync(id);

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination) => await _repository.GetAsync(pagination);

    public virtual async Task<ActionResponse<int>> GetTotalRecordsAsync() => await _repository.GetTotalRecordsAsync();
}