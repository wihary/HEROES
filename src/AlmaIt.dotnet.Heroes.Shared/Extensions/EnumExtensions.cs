﻿namespace AlmaIt.Dotnet.Heroes.Shared.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Extension class of enums.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>To the description dictionary.</summary>
        /// <typeparam name="T">Type de l'énumération.</typeparam>
        /// <returns>Retourne un dictionnaire { key = name, value = description } pour une enum.</returns>
        public static Dictionary<string, string> GetDescriptionDictionary<T>()
            where T : Enum
        {
            var type = typeof(T);
            var names = Enum.GetNames(type);

            string Selector(string name)
            {
                if (!(type.GetTypeInfo().GetField(name).GetCustomAttribute(typeof(DisplayAttribute)) is DisplayAttribute attribute))
                {
                    return null;
                }

                return attribute.Description ?? string.Empty;
            }

            return names.ToDictionary(name => name, Selector);
        }

        /// <summary>Méthode d'obtention de la description d'une valeur d'une énumération.</summary>
        /// <param name="value">The value. </param>
        /// <typeparam name="T">Type de l'énumération.</typeparam>
        /// <returns>The <see cref="string" />. </returns>
        public static string GetEnumDescription<T>(this T value)
            where T : Enum
        {
            var type = typeof(T);
            var name = Enum.GetNames(type).FirstOrDefault(f => string.Equals(f, value.ToString(), StringComparison.CurrentCultureIgnoreCase));

            var field = type.GetTypeInfo().GetField(name);

            return field.GetCustomAttribute(typeof(DisplayAttribute)) is DisplayAttribute customAttribute ? customAttribute.Description ?? string.Empty : name;
        }

        /// <summary>Returns true if enum matches any of the given values.</summary>
        /// <param name="value">Value to match.</param>
        /// <param name="values">Values to match against.</param>
        /// <returns>Return true if matched.</returns>
        public static bool In(this Enum value, params Enum[] values) => values.Any(v => v.Equals(value));

        /// <summary>Méthode d'extension de récupération de la valeur entière d'une énumération.</summary>
        /// <param name="enumValue">Valeur de l'énumération.</param>
        /// <returns>Retourne la valeur entière de l'énumération.</returns>
        public static int ToInt(this Enum enumValue) => Convert.ToInt32(enumValue);
    }
}
