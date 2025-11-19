using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuilding : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private float _scanInterval = 5f;

    private WaitForSeconds _scanWait;
    private Queue<IScannable> _scannedResources = new Queue<IScannable>();
    private ResourceStorage _resourceStorage = new ResourceStorage();

    private void Awake()
    {
        _scanWait = new WaitForSeconds(_scanInterval);
    }

    private void OnEnable()
    {
        StartCoroutine(Scanning());

        if (_scanner != null)
            _scanner.ScannableDetected += OnResourceScanned;
    }

    private void OnDisable()
    {
        if (_scanner != null)
            _scanner.ScannableDetected -= OnResourceScanned;
    }

    public void DeliverResource(Resource resource)
    {
        resource.Pick(transform, () =>
        {
            Debug.Log($"Resource {resource.name} delivered to Main Building");
            _resourceStorage.Add(resource);
        });
    }

    private IEnumerator Scanning()
    {
        while (enabled)
        {
            yield return _scanWait;
            _scanner.Scan();
        }
    }

    private void OnResourceScanned(IScannable scannable)
    {
        if (scannable != null && !_scannedResources.Contains(scannable))
        {
            scannable.Scan();
            _scannedResources.Enqueue(scannable);
        }
    }
}