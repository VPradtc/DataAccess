using DataAccess.CoreDto.Model.Kendo.Filtering.Bindings;
using System.Collections.Generic;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Factory.Abstraction
{
    public interface IOperatorBindingFactory
    {
        IEnumerable<OperatorBinding> CreateBindings();
    }
}
