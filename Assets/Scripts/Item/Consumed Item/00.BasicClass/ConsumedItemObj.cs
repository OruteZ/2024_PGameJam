using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumedItemObj : ItemObj
{

    public override bool TryUse(Player player, out bool isDestroyed)
    {
        Debug.LogError("�ٷ� �Һ�Ǵ� ������Ʈ�ε� ����� �Ϸ��ϴ� ���װ� �����");

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

    protected virtual void ConsumeItem()//���� ȿ�� �߻� �Լ�
    {

    }
}
