using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Soenneker.Dtos.IdNamePair;
using Soenneker.Entities.Named.Abstract;

namespace Soenneker.Extensions.Enumerable.Entities.Named;

/// <summary>
/// A collection of helpful Enumerable NamedEntity extension methods
/// </summary>
public static class EnumerableNamedEntitiesExtension
{
    /// <summary>
    /// Projects a sequence of <see cref="INamedEntity"/> instances into a list of <see cref="IdNamePair"/> objects.
    /// </summary>
    /// <typeparam name="T">The enumerable type implementing <see cref="IEnumerable{T}"/> where <c>T</c> is <see cref="INamedEntity"/>.</typeparam>
    /// <param name="value">The enumerable collection of named entities to transform.</param>
    /// <returns>
    /// A list of <see cref="IdNamePair"/> objects containing the <c>Id</c> and <c>Name</c> values from each entity.
    /// Returns an empty list if <paramref name="value"/> is <c>null</c>.
    /// </returns>
    /// <remarks>
    /// This method is optimized for performance:
    /// <list type="bullet">
    ///   <item><description>Preallocates list capacity using <see cref="ICollection{T}.Count"/> when available.</description></item>
    ///   <item><description>Uses index-based iteration for <see cref="IList{T}"/> to reduce enumerator overhead.</description></item>
    ///   <item><description>Avoids LINQ and deferred execution for zero allocation beyond the result list.</description></item>
    /// </list>
    /// </remarks>
    [Pure]
    public static List<IdNamePair> ToIdNamePairs<T>(this T value) where T : IEnumerable<INamedEntity>
    {
        if (value is null)
            return [];

        switch (value)
        {
            case IList<INamedEntity> list:
            {
                var result = new List<IdNamePair>(list.Count);
                for (var i = 0; i < list.Count; i++)
                {
                    INamedEntity e = list[i];
                    result.Add(new IdNamePair {Id = e.Id, Name = e.Name});
                }

                return result;
            }

            case ICollection<INamedEntity> collection:
            {
                var result = new List<IdNamePair>(collection.Count);
                foreach (INamedEntity e in collection)
                {
                    result.Add(new IdNamePair {Id = e.Id, Name = e.Name});
                }

                return result;
            }

            default:
            {
                var result = new List<IdNamePair>();
                using IEnumerator<INamedEntity> enumerator = value.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    INamedEntity e = enumerator.Current;
                    result.Add(new IdNamePair {Id = e.Id, Name = e.Name});
                }

                return result;
            }
        }
    }
}