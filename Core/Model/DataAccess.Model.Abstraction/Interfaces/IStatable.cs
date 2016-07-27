namespace DataAccess.Core.Model.Abstraction.Interfaces
{
    public interface IStatable
    {
        bool IsActive { get; set; }
        bool IsDeleted { get; set; }
    }
}