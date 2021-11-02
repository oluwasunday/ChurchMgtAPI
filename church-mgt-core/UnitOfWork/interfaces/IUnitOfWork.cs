using church_mgt_core.repositories.abstractions;
using System.Threading.Tasks;

namespace church_mgt_core.UnitOfWork.interfaces

{
    public interface IUnitOfWork
    {
        ICommentRepository Comment { get; }

        Task CompleteAsync();
    }
}