using DataAccess.Core.Extensions.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataAccess.CoreDto.Model.Kendo
{
    public class KendoGridFilters
    {
        public List<KendoFilterItem> Filters { get; set; }
        public string Logic { get; set; }

        private static PropertyInfo GetNestedPropertyInfo(Type entityType, string propertyName)
        {
            var propertyParts = propertyName.Split('.');

            var ownPropertyName = propertyParts[0];
            var nestedPropertyName = propertyParts[1];

            var targetType = entityType.GetProperty(ownPropertyName).PropertyType;

            var result = targetType.GetProperty(nestedPropertyName);

            return result;
        }

        private static PropertyInfo GetPropertyInfo(Type entityType, string propertyName)
        {
            var isNestedProperty = propertyName.Contains(".");

            if (isNestedProperty)
            {
                return GetNestedPropertyInfo(entityType, propertyName);
            }

            return entityType.GetProperty(propertyName);
        }

        public static string BuildWhereClause<TEntity>(int index, KendoFilterItem filter,
            List<object> parameters)
        {
            var entityType = (typeof(TEntity));

            var propertyName = filter.Field.ToTitleCase();

            var property = GetPropertyInfo(entityType, propertyName);

            switch (filter.Operator.ToLower())
            {
                case "eq":
                case "neq":
                case "gte":
                case "gt":
                case "lte":
                case "lt":
                    if (typeof(DateTime).IsAssignableFrom(property.PropertyType))
                    {
                        parameters.Add(DateTime.Parse(filter.Value).Date);
                        return string.Format("EntityFunctions.TruncateTime({0}){1}@{2}",
                            filter.Field,
                            ToLinqOperator(filter.Operator),
                            index);
                    }
                    if (typeof(int).IsAssignableFrom(property.PropertyType))
                    {
                        parameters.Add(int.Parse(filter.Value));
                        return string.Format("{0}{1}@{2}",
                            filter.Field,
                            ToLinqOperator(filter.Operator),
                            index);
                    }
                    if (typeof(decimal).IsAssignableFrom(property.PropertyType))
                    {
                        parameters.Add(int.Parse(filter.Value));
                        return string.Format("{0}{1}@{2}",
                            filter.Field,
                            ToLinqOperator(filter.Operator),
                            index);
                    }
                    if (typeof(bool).IsAssignableFrom(property.PropertyType))
                    {
                        parameters.Add(bool.Parse(filter.Value));
                        return string.Format("{0}{1}@{2}",
                            filter.Field,
                            ToLinqOperator(filter.Operator),
                            index);
                    }
                    if (typeof(Guid).IsAssignableFrom(property.PropertyType))
                    {
                        parameters.Add(Guid.Parse(filter.Value));
                        return string.Format("{0}{1}@{2}",
                            filter.Field,
                            ToLinqOperator(filter.Operator),
                            index);
                    }
                    if (typeof(Enum).IsAssignableFrom(property.PropertyType))
                    {
                        parameters.Add(Enum.ToObject(property.PropertyType, int.Parse(filter.Value)));

                        return string.Format("{0}{1}@{2}",
                            filter.Field,
                            ToLinqOperator(filter.Operator),
                            index);
                    }
                    parameters.Add(filter.Value);
                    return string.Format("{0}{1}@{2}",
                        filter.Field,
                        ToLinqOperator(filter.Operator),
                        index);
                case "startswith":
                    parameters.Add(filter.Value);
                    return string.Format("{0}.StartsWith(@{1})",
                        filter.Field,
                        index);
                case "endswith":
                    parameters.Add(filter.Value);
                    return string.Format("{0}.EndsWith(@{1})",
                        filter.Field,
                        index);
                case "contains":
                    parameters.Add(filter.Value);
                    return string.Format("{0}.Contains(@{1})",
                        filter.Field,
                        index);
                case "doesnotcontain":
                    parameters.Add(filter.Value);
                    return string.Format("!{0}.Contains(@{1})",
                        filter.Field,
                        index);
                default:
                    throw new NotSupportedException($"Operator {filter.Operator} is not supported.");
            }
        }

        public static string CreateFilterCombinationOperator(string @operator)
        {
            switch (@operator.ToLower())
            {
                case "or":
                    return " || ";
                case "and":
                    return " && ";
                default:
                    throw new NotSupportedException($"Operator {@operator} is not supported.");
            }
        }

        public static string ToLinqOperator(string @operator)
        {
            switch (@operator.ToLower())
            {
                case "eq":
                    return " == ";
                case "neq":
                    return " != ";
                case "gte":
                    return " >= ";
                case "gt":
                    return " > ";
                case "lte":
                    return " <= ";
                case "lt":
                    return " < ";
                default:
                    throw new NotSupportedException($"Operator {@operator} is not supported.");
            }
        }
    }
}