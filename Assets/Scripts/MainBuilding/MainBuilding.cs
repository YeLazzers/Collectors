using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuilding : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private float _scanInterval = 5f;
    [SerializeField] private float _landingRadius = 1f;

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _landingRadius);
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

    public void TakeResource(Resource resource)
    {
        resource.Collect(transform, () =>
        {
            _resourceStorage.Add(resource);
        });
    }

    public Resource GetNextResource()
    {
        return (Resource)_scannedResources.Dequeue();
    }

    public Vector3 GetLandingPoint(Vector3 originPos)
    {
        Vector3 dir = (transform.position - originPos).normalized;
        return transform.position - dir * _landingRadius;
    }
}