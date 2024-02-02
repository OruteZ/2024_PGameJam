using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemObj : ItemObj
{
    public override ItemType PickItem(Player owner)
    {
        if(usingPlayer == null)
        {
            usingPlayer = owner;

            InitWhenPick();

            return ItemType.Equip;
        }
        else
        {
            return ItemType.Consumed;//이미 사용되었거나 소비된 아이템이면 착용 안하고 반환하기
        }
    }

    public override bool TryUse(Player player, out bool isDestroyed)
    {
        bool _isDestroyed = false;

        bool isUsed = UseItem(out _isDestroyed);
        isDestroyed = _isDestroyed;

        return isUsed;
    }

    protected virtual void InitWhenPick()
    {

    }

    protected virtual bool UseItem(out bool isDestroyed)
    {
        isDestroyed = false;

        return false;
    }
}
