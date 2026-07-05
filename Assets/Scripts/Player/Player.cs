using UnityEngine;

[RequireComponent(typeof(PlayerInputRouter))]
public class Player : MonoBehaviour
{
    [SerializeField] private Material _material;

    private PlayerInputRouter _router;

    public PlayerInputRouter Router => _router;

    public Material Material => _material;

    private void Awake()
    {
        _router = GetComponent<PlayerInputRouter>();
    }
}
