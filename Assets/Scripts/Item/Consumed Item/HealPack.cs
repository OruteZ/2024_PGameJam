using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : ConsumedItemObj
{
    protected override void ConsumeItem()
    {
        base.ConsumeItem();

        //�÷��̾�� ���� �ش�

        Destroy(this.gameObject);
    }
}