using HDNXUdemyModel.Constant;

namespace HDNXUdemyModel.Base
{
    public class OptionFilters
    {
        public string? FieldName { get; set; }

        public string? Operator { get; set; }

        public string? Value { get; set; }

        public string ConvertOperator(string fieldName, string operators, string value)
        {
            switch (operators)
            {
                case EOperators.BEGINWITH:
                    return $"{fieldName}.StartsWith(\"{value}\")";

                case EOperators.CONTAINS:
                    return $"{fieldName}.Contains(\"{value}\")";

                case EOperators.ENDWITH:
                    return $"{fieldName}.EndWith(\"{value}\")";

                case EOperators.EQUAL:
                    return $"{fieldName} = \"{value}\"";

                case EOperators.LARGER:
                    return $"{fieldName} >= \"{value}\"".TrimEnd('^');

                case EOperators.SMALLER:
                    return $"{fieldName} < \"{value}\"".TrimEnd('^');

                case EOperators.MUTISELECT:
                    var listValue = value.Split(",").ToList();
                    var result = new List<string>();
                    listValue.ForEach(item =>
                    {
                        string query = $"{fieldName}=\"{item}\"";
                        result.Add(query);
                    });
                    var resultQuery = string.Join(" || ", result);
                    return $"({result})";
            }
            return string.Empty;
        }
    }
}