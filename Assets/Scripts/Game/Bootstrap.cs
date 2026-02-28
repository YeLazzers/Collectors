using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private int _targetFrameRate = 60;
    [SerializeField] private int _vSyncCount = 1;
    [SerializeField] private Game _game;

    private void Awake()
    {
        Application.targetFrameRate = _targetFrameRate;
        QualitySettings.vSyncCount = _vSyncCount;
    }

    private void Start()
    {
        _game.Initialize();
    }
}
