namespace CarsCatalog.Application.Interfaces.Repositories;

public interface IRepositoryUnitOfWork
{
    IBrandRepository Brands { get; }
}
