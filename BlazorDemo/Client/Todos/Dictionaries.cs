using BlazorDemo.Client.Shared.Types;
using BlazorDemo.Shared.Todos;

namespace BlazorDemo.Client.Todos;

public static class Dictionaries
{
    public static readonly IReadOnlyCollection<DictionaryItem<Priority>> Priorities = new List<DictionaryItem<Priority>>
    {
        new (Priority.Low, "Low"),
        new (Priority.Normal, "Normal"),
        new (Priority.High, "High"),
        new (Priority.Critical, "Critical"),
    };
}
