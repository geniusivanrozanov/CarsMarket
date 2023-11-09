using CarsCatalog.Application.DTOs;
using CarsCatalog.Domain.Entities;

namespace CarsCatalog.Application.Interfaces.Mappers;

public interface IMapper
{
    TResult Map<TResult, TSource>(TSource source);
    IQueryable<TResult> Project<TResult, TSource>(IQueryable<TSource> source);
}
