using Soenneker.Dtos.IdNamePair;
using Soenneker.Entities.Named.Abstract;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Soenneker.Extensions.Enumerable.Entities.Named;

/// <summary>
/// A collection of helpful Enumerable NamedEntity extension methods
/// </summary>
public static class EnumerableNamedEntitiesExtension
{
    [Pure]
    public static List<IdNamePair> ToIdNamePairs<T>(this IEnumerable<T>? source)
        where T : INamedEntity
    {
        if (source is null)
            return [];

        // Fast-path: T[] (no interface dispatch per element, tight loop)
        if (source is T[] arr)
        {
            var result = new List<IdNamePair>(arr.Length);
            for (int i = 0; i < arr.Length; i++)
            {
                T e = arr[i];
                result.Add(new IdNamePair { Id = e.Id, Name = e.Name });
            }
            return result;
        }

        // Fast-path: IReadOnlyList<T> (covers List<T>, ImmutableArray-like, etc.)
        if (source is IReadOnlyList<T> rol)
        {
            var result = new List<IdNamePair>(rol.Count);
            for (int i = 0; i < rol.Count; i++)
            {
                T e = rol[i];
                result.Add(new IdNamePair { Id = e.Id, Name = e.Name });
            }
            return result;
        }

        // Next best: IList<T>
        if (source is IList<T> il)
        {
            var result = new List<IdNamePair>(il.Count);
            for (int i = 0; i < il.Count; i++)
            {
                T e = il[i];
                result.Add(new IdNamePair { Id = e.Id, Name = e.Name });
            }
            return result;
        }

        // Pre-size if we can without enumerating
        if (source.TryGetNonEnumeratedCount(out int count) && count > 0)
        {
            var result = new List<IdNamePair>(count);
            foreach (T e in source)
                result.Add(new IdNamePair { Id = e.Id, Name = e.Name });
            return result;
        }

        // Unknown count
        {
            var result = new List<IdNamePair>();
            foreach (T e in source)
                result.Add(new IdNamePair { Id = e.Id, Name = e.Name });
            return result;
        }
    }
}