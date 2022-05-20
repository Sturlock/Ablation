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

        //BoxCollider box = hit.collider as BoxCollider;
        //if (box == null) Debug.LogWarning("[LoadZone] Collider is not a BoxCollider");


        

        Vector3 localPoint = gameObject.transform.localPosition.Direction(other.gameObject.transform.localPosition);
        Vector3 localDir = localPoint.normalized;
        Debug.Log("[LoadZone] Direction" + localDir);
        if (other.CompareTag("Player"))
        {
            if(_loadZone != null)
            {
                if (!_loadZone.activeSelf && localDir.z > 0.01f)
                {
                    UnloadArea(_unloadZone);
                    LoadArea(_loadZone);
                    return;
                }
                if (_loadZone.activeSelf && localDir.z < 0.01f)
                {
                    UnloadArea(_loadZone);
                    LoadArea(_unloadZone);
                    return;
                }
            }
        }
    }
}