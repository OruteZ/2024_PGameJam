using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : ConsumedItemObj
{
    [SerializeField] float healAmount = 20f;

    protected override void ConsumeItem()
    {
        base.ConsumeItem();

        usingPlayer.Heal(healAmount);
        //플레이어에게 힐은 준다

        DestroyGameObj();
    }
}
