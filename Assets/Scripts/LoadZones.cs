using UnityEngine;

public class LoadZones : MonoBehaviour
{
    [Header("Zones To Load")]
    public GameObject _unloadZone;

    public GameObject _loadZone;

    private void UnloadArea(GameObject zoneObject)
    {
        if (zoneObject != null)
        zoneObject.SetActive(false);
    }

    private void LoadArea(GameObject zoneObject)
    {
        if(zoneObject != null)
        zoneObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(_loadZone != null)
            {
                if (!_loadZone.activeSelf)
                {
                    UnloadArea(_unloadZone);
                    LoadArea(_loadZone);
                    return;
                }
                if (_loadZone.activeSelf)
                {
                    UnloadArea(_loadZone);
                    LoadArea(_unloadZone);
                    return;
                }
            }
        }
    }
}