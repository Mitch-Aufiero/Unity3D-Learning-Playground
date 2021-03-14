using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface AiWeapons
{

    float WeaponRange { get; set; }
    float CoolDown { get; set; }
    bool FinishedAttack { get; set; }

    void StartAttack();
}
