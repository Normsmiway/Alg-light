
namespace Alg.Domain
{

    public class AlgUnitOfWork : UnitOfWork
    {
        public AlgUnitOfWork() : base(new AlgDb()) { }
    }
}
