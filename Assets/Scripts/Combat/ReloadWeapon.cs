using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat { 
    public class ReloadWeapon : MonoBehaviour
    {
        public Animator rigController;
        public WeaponAnimationEvents animationEvents;
        public ActiveWeapon activeWeapon;
        public Transform leftHand;
        public AmmoWidgetController ammoWidget;

        GameObject magazineHand;

        // Start is called before the first frame update
        void Start()
        {
            animationEvents.WeaponAnimationEvent.AddListener(OnAnimationEvent);
        }

        // Update is called once per frame
        void Update()
        {
            RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
            if (weapon)
            {
                if ((Input.GetKeyDown(KeyCode.R)  && weapon.ammoCount < weapon.clipSize)|| weapon.ammoCount <= 0)
                {
                    WeaponReload();
                    weapon.reloading = true;
                }
                if (weapon.isFiring)
                {
                    ammoWidget.RefreshAmmoCount(weapon.ammoCount);
                }
            }
        
        }

        public void WeaponReload() {
            rigController.SetTrigger("reload_weapon");
        }

        void OnAnimationEvent(string eventName)
        {

            switch (eventName)
            {
                case "detach_magazine":
                    DetachMagazine();
                    break;
                case "drop_magazine":
                    DropMagazine();
                    break;
                case "refill_magazine":
                    RefillMagazine();
                    break;
                case "attach_magazine":
                    AttachMagazine();
                    break;
            }
        }

        void DetachMagazine()
        {
            RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
            magazineHand = Instantiate(weapon.magazine, leftHand, true);
            weapon.magazine.SetActive(false);
            

        }
        void DropMagazine()
        {
            GameObject droppedMagazine = Instantiate(magazineHand, magazineHand.transform.position, magazineHand.transform.rotation);
            droppedMagazine.AddComponent<Rigidbody>();
            droppedMagazine.AddComponent<BoxCollider>();
            magazineHand.SetActive(false);

        }
        void RefillMagazine()
        {
            magazineHand.SetActive(true);
        }
        void AttachMagazine()
        {
            RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
            weapon.magazine.SetActive(true);
            Destroy(magazineHand);
            weapon.ammoCount = weapon.clipSize;
            weapon.reloading = false;
            rigController.ResetTrigger("reload_weapon");


            ammoWidget.RefreshAmmoCount(weapon.ammoCount);
        }
    }
}