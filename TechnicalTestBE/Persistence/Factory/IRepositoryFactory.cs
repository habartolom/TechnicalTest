using Persistence.Contracts;

namespace Persistence.Factory
{
    public interface IRepositoryFactory
    {
        IAuthorRepository Authors { get; }
        IBookRepository Books { get; }
        IConstantRepository Constants { get; }
        int Commit();
    }
}
