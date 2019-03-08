namespace AlmaIt.Dotnet.Heroes.Client.Helpers
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Help class for HTML display of models.
    /// </summary>
    public static class HtmlHelper
    {
        /// <summary>Get the display name of a property.</summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="propertyExpression">Expression of property.</param>
        /// <returns>Returns the display name.</returns>
        public static string DisplayName<T>(this Expression<Func<T, object>> propertyExpression)
        {
            var memberInfo = propertyExpression.Body.GetPropertyInformation();
            if (memberInfo == null)
            {
                throw new ArgumentException("No property reference expression was found.", nameof(propertyExpression));
            }

            var attr = memberInfo.GetAttribute<DisplayNameAttribute>();
            return attr == null ? memberInfo.Name : attr.DisplayName;
        }

        private static T GetAttribute<T>(this ICustomAttributeProvider member)
            where T : Attribute
            => (T)member.GetCustomAttributes(typeof(T), false).SingleOrDefault();

        private static MemberInfo GetPropertyInformation(this Expression propertyExpression)
        {
            Debug.Assert(propertyExpression != null, "propertyExpression != null");
            var memberExpr = propertyExpression as MemberExpression;
            if (memberExpr == null && propertyExpression is UnaryExpression unaryExpr && unaryExpr.NodeType == ExpressionType.Convert)
            {
                memberExpr = unaryExpr.Operand as MemberExpression;
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                return memberExpr.Member;
            }

            return null;
        }
    }
}
