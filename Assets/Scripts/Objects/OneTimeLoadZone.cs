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

    private void LoadArea(GameObject zoneObject)
    {
        bool active = !zoneObject.activeInHierarchy;
        if (zoneObject != null)
            zoneObject.SetActive(active);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (doOnce)
        {
            if (other.CompareTag("Player"))
            {
                if (_loadZone != null)
                {
                        LoadArea(_loadZone);
                        doOnce = false;
                        return;
                }
            }
            
        }
        
    }
}