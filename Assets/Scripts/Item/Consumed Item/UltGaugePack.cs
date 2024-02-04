using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltGaugePack : ConsumedItemObj 
{
    [SerializeField,Range(0f,1f)] float ultAmount = 0.5f;

    protected override void ConsumeItem()
    {
        base.ConsumeItem();

        usingPlayer.ultimateGauge += ultAmount;
        //플레이어에게 힐은 준다

        DestroyGameObj();
    }
}
