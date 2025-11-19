using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Resources/New Resource", order = 51)]
public class ResourceConfig : ScriptableObject
{
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private int _value;

    public ResourceType ResourceType => _resourceType;
    public int Value => _value;
}