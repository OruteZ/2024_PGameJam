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
            return ItemType.Consumed;//�̹� ���Ǿ��ų� �Һ�� �������̸� ���� ���ϰ� ��ȯ�ϱ�
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
