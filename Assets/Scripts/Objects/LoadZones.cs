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
        Vector3 localDir = transform.localPosition.Direction(other.gameObject.transform.localPosition);
        float localPoint = Vector3.Dot(transform.forward, localDir);
        
        
        if (other.CompareTag("Player"))
        {
            if(_loadZone != null)
            {
                if (!_loadZone.activeSelf && localPoint > 0.01f)
                {
                    Debug.Log("[LoadZone] Direction = " + localPoint);
                    UnloadArea(_unloadZone);
                    LoadArea(_loadZone);
                    return;
                }
                if (_loadZone.activeSelf && localPoint < 0.01f)
                {
                    Debug.Log("[LoadZone] Direction = " + localPoint);
                    UnloadArea(_loadZone);
                    LoadArea(_unloadZone);
                    return;
                }
            }
        }
    }
}