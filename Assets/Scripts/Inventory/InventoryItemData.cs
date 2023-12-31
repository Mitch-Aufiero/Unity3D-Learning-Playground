using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(menuName = "Inventory Item Data")]
    public class InventoryItemData : ScriptableObject
    {
        public string id;
        public string displayName;
        public Sprite icon;
        public GameObject prefab;
    }

}