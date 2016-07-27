namespace DataAccess.Validators.Core
{
    public interface IValidator<T>
    {
        bool Validate(T input);
    }
}
