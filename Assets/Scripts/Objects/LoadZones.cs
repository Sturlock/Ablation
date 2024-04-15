using UnityEngine;
using Utils;

namespace Objects
{
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
				if (localPoint > 0.01f)
				{
					Debug.Log("[LoadZone] Direction = " + localPoint);
					if(_unloadZone != null)
						UnloadArea(_unloadZone);
					if(_loadZone != null)
						LoadArea(_loadZone);
					return;
				}
				if (localPoint < -0.01f)
				{
					Debug.Log("[LoadZone] Direction = " + localPoint);
					if (_loadZone != null)
						UnloadArea(_loadZone);

					if (_unloadZone != null)
						LoadArea(_unloadZone);
					return;
				}
			}
		}
	}
}