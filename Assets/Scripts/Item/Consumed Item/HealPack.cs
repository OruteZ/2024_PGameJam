using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : ConsumedItemObj
{
    protected override void ConsumeItem()
    {
        base.ConsumeItem();

        //플레이어에게 힐은 준다

        Destroy(this.gameObject);
    }
}