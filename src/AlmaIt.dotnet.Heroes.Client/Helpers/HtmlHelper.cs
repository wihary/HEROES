namespace AlmaIt.dotnet.Heroes.Client.Helpers
{
    #region Usings

    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    #endregion

    public class HtmlHelper
    {
        /// <summary>
        /// Get the display name of a property.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="propertyExpression">Expression of property.</param>
        /// <returns>Returns the display name.</returns>
        public string DisplayName<T>(Expression<Func<T, object>> propertyExpression)
        {
            var memberInfo = GetPropertyInformation(propertyExpression.Body);
            if (memberInfo == null)
            {
                throw new ArgumentException("No property reference expression was found.", nameof(propertyExpression));
            }

            var attr = GetAttribute<DisplayNameAttribute>(memberInfo, false);
            return attr == null ? memberInfo.Name : attr.DisplayName;
        }

        private static T GetAttribute<T>(MemberInfo member, bool isRequired)
            where T : Attribute
        {
            var attribute = member.GetCustomAttributes(typeof(T), false).SingleOrDefault();

            if (attribute == null && isRequired)
            {
                throw new ArgumentException($"The {typeof(T).Name} attribute must be defined on member {member.Name}");
            }

            return (T)attribute;
        }

        private static MemberInfo GetPropertyInformation(Expression propertyExpression)
        {
            Debug.Assert(propertyExpression != null, "propertyExpression != null");
            var memberExpr = propertyExpression as MemberExpression;
            if (memberExpr == null)
            {
                if (propertyExpression is UnaryExpression unaryExpr && unaryExpr.NodeType == ExpressionType.Convert)
                {
                    memberExpr = unaryExpr.Operand as MemberExpression;
                }
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                return memberExpr.Member;
            }

            return null;
        }
    }
}
