namespace Myrtex.Persistence.Extensions;

public static class CollectionExtensions
{
    private static Random random = new Random();

    public static T GetRandomElement<T>(this ICollection<T>? collection)
    {
        if (collection == null || collection.Count == 0)
        {
            throw new InvalidOperationException("The collection is null or empty.");
        }

        int randomIndex = random.Next(collection.Count);
        return collection.ElementAt(randomIndex);
    }
}
