using System.ComponentModel.DataAnnotations;

namespace HDNXUdemyServices.CommonFunction
{
    public static class AttributeFunction
    {
        public static int? GetMaxLengthProperty(Type pModelClass, string pPropertyName)
                 => pModelClass.GetProperties()
                .Single(p => p.Name == pPropertyName)
                .GetCustomAttributes(typeof(StringLengthAttribute), true)
                .Cast<StringLengthAttribute>().FirstOrDefault()?.MaximumLength;
    }
}