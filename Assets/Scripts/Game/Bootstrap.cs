using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private int _targetFrameRate = 60;
    [SerializeField] private int _vSyncCount = 1;

    void Awake()
    {
        Application.targetFrameRate = _targetFrameRate;
        QualitySettings.vSyncCount = _vSyncCount;
    }
}