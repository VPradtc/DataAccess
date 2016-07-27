namespace DataAccess.Core.Model.Abstraction.Interfaces
{
    public interface IStatableEntity : IStatable
    {
        void Activate();
        void Deactivate();
        void Delete();
    }
}