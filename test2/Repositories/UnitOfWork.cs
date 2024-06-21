using test2.Context;

namespace test2.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private CharacterContext _characterContext;

    public UnitOfWork(CharacterContext characterContext)
    {
        _characterContext = characterContext;
    }
    public async Task BeginTransactionAsync()
    {
        await _characterContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _characterContext.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _characterContext.Database.RollbackTransactionAsync();
    }
}