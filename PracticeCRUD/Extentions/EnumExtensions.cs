using System.ComponentModel;
using System.Reflection;

namespace PracticeCRUD.Extentions
{
    public static class EnumExtensions
    {
        public static string Description(this Enum enumeration)
        {
            return enumeration.GetAttributeValue<DescriptionAttribute, string>(x => x.Description);
        }

        private static Expected GetAttributeValue<T, Expected>(this Enum enumeration, Func<T, Expected> expression) where T : Attribute
        {
            var item = enumeration.GetType().GetMember(enumeration.ToString()).Where(member => member.MemberType == MemberTypes.Field)
                .FirstOrDefault();
            Expected result = default(Expected);
            if (item != null)
            {
                T attribute = item
                .GetCustomAttributes(typeof(T), false)
                .Cast<T>()
                .SingleOrDefault();
                if (attribute != null)
                {
                    result = expression(attribute);
                }
            }
            return result;
        }
    }
}
