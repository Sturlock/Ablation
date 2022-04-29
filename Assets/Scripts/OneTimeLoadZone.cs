using UnityEngine;

public class OneTimeLoadZone : MonoBehaviour
{
    public bool doOnce = true;

    [Header("Load Zone")]
    public Collider _zone;

    [Header("Zones To Load")]
    public GameObject _loadZone;
    public void Awake()
    {
        doOnce = true;
    }

    private void UnloadArea(GameObject zoneObject)
    {
        if (zoneObject != null)
            zoneObject.SetActive(false);
    }

    private void LoadArea(GameObject zoneObject)
    {
        if (zoneObject != null)
            zoneObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (doOnce)
        {
            if (other.CompareTag("Player"))
            {
                if (_loadZone != null)
                {
                    if (!_loadZone.activeSelf)
                    {
                        LoadArea(_loadZone);
                        doOnce = false;
                        return;
                    }
                }
            }
            
        }
        
    }
}