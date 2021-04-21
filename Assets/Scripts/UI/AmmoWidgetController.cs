using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoWidgetController : MonoBehaviour
{
    public TMPro.TMP_Text ammoCountText;
    public TMPro.TMP_Text ammoInventoryText;

    public void RefreshAmmoCount(int ammoCount)
    {
        ammoCountText.text = ammoCount.ToString();
    }

    public void RefreshAmmoInventoryCount(int ammoInInventory)
    {
        ammoInventoryText.text = ammoInInventory.ToString();
    }

}
