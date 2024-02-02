using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiUsableItiemObj : EquipItemObj//바로 소비되는 것이 아닌 여러번 사용할 수 있는 아이템들
{
    [SerializeField] protected int usableCount = 3;//아이템을 사용할 수 있는 횟수

    protected override bool UseItem(out bool isDestroyed)
    {
        usableCount -= 1;//한번 사용
        if(usableCount == 0)
        {
            isDestroyed = true;
            Destroy(this.gameObject);
        }
        else
        {
            isDestroyed = false;
        }

        return true;
    }
}
