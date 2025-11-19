using System.Collections.Generic;

public class ResourceStorage
{
    private Dictionary<ResourceType, int> _resources = new();

    public int Get(ResourceType type) => _resources.GetValueOrDefault(type, 0);

    public void Add(ResourceType type, int amount)
    {
        _resources[type] = Get(type) + amount;
    }

    public void Add(Resource resource)
    {
        _resources[resource.Type] = Get(resource.Type) + resource.Amount;
    }

    public bool TrySpend(ResourceType type, int amount)
    {
        if (Get(type) < amount)
            return false;

        _resources[type] -= amount;
        return true;
    }
}

public enum ResourceType
{
    Gem,
}
