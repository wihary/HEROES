namespace AlmaIt.Dotnet.Heroes.Shared.Helpers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;

    /// <summary>Static extension class that regroup method for IQueryable objects.</summary>
    public static class QueryableExtensions
    {
        /// <summary>
        ///     This extension method is used to sort Model object data presented as a queryable collection Use the logic a sortString building in order to sort on giving properties if
        ///     possible.
        /// </summary>
        /// <param name="source">Source list to sort.</param>
        /// <param name="sortBy">Field by sort is.</param>
        /// <typeparam name="T">Type of list elements.</typeparam>
        /// <returns>Return the sorted list.</returns>
        public static IQueryable<T> Sort<T>(this IQueryable<T> source, string sortBy)
        {
            // First of all we do a parameters validation on source and sort string
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(sortBy))
            {
                return source;
            }

            try
            {
                return source.OrderBy(BuildSortExpression(sortBy));
            }
            catch (Exception)
            {
                // sortBy include field not part of the model
                return source;
            }
        }

        /// <summary>This private function ensure that the ordering part of the expression is of the correct format.</summary>
        /// <param name="sortPart">Sorting expression can contains multiple fields and order, a part he a fragment of the full expression.</param>
        /// <returns>Return the direction of sorting.</returns>
        private static string AdjustDirection(string sortPart)
        {
            // First of all check if there is an ordering direction to handle, if not default is applied by Sort method
            if (!sortPart.Contains(' '))
            {
                return sortPart;
            }

            // In case an ordering direction is specified we try to match it with known direction string
            // If for any reason we does not understand specified direction, expression is simplified to sort field only with defautl direction
            var field = sortPart.Split(' ')[0];
            switch (sortPart.Split(' ')[1])
            {
                case "asc":
                case "ascending":
                    return $"{field} ascending";

                case "desc":
                case "descending":
                    return $"{field} descending";

                default:
                    return field;
            }
        }

        /// <summary>This private function is dedicated to create a well formed ordering string expression.</summary>
        /// <param name="sortBy">Based sort expression send to sort extension method.</param>
        /// <returns>The ordering string expression.</returns>
        private static string BuildSortExpression(string sortBy)
        {
            var sortExpression = sortBy.Split(',').Aggregate(string.Empty, (current, item) => $"{current}{AdjustDirection(item)},");
            return sortExpression.Substring(0, sortExpression.Length - 1);
        }
    }
}
