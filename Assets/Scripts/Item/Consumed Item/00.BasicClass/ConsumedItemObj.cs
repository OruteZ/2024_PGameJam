using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumedItemObj : ItemObj
{
    
    public override bool TryUse(Player player, out bool isDestroyed)
    {
        Debug.LogError("바로 소비되는 오브젝트인데 사용을 하려하는 버그가 생긴다");

        isDestroyed = true;

        return false;
    }

    public override ItemType PickItem(Player owner)
    {
        if(usingPlayer == null)
        {
            usingPlayer = owner;
            ConsumeItem();
        }

        return ItemType.Consumed;
    }

    protected virtual void ConsumeItem()//실제 효과 발생 함수
    {

    }

    protected void DestroyGameObj()
    {
        DestroyEffectCreator effect = GetComponent<DestroyEffectCreator>();
        if (effect) effect.CreateDestroyEffect();

        Destroy(this.gameObject);
    }
}
