using UnityEngine;

namespace Objects
{
    public enum ItemType
    {
        Throwable,
        KeyCard,
        AudioLog,
    }

    [CreateAssetMenu(fileName = "ItemConfig", menuName = "Ablation/ItemConfig", order = 0)]
    public class Generic_Item : ScriptableObject
    {
        public string itemName = "Generic Item";
        public string itemDescription = "Generic Item Description";
        public float itemWeight = 0f;
        public ItemType itemType = ItemType.Throwable;
    }
}