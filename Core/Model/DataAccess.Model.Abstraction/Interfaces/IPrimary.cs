using System;

namespace DataAccess.Core.Model.Abstraction.Interfaces
{
    public interface IPrimary<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}
