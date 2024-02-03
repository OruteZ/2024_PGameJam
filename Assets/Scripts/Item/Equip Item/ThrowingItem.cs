using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingItem : EquipItemObj//던지는 아이템
{
    [SerializeField,Space(10f)] bool isChangingToSameSprite = false;

    [SerializeField] GameObject throwingObj;

    [SerializeField, Space(10f)] float throwForceAmount = 10f;

    protected override void InitWhenPick()
    {
        base.InitWhenPick();

        this.transform.SetParent(usingPlayer.transform);//플레이어 자식화
        this.transform.localPosition = Vector3.zero;//임시로 위치 플레이어 중심으로 잡음

        GetComponent<SpriteRenderer>().enabled = false;//스프라이트 꺼줌
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        foreach (Collider2D coll in GetComponentsInChildren<Collider2D>()) coll.enabled = false;
    }

    protected override bool UseItem(out bool isDestroyed)
    {
        GameObject obj = Instantiate(throwingObj, usingPlayer.GetPlayerThrowTsf().position, Quaternion.identity);

        if (isChangingToSameSprite)
        {
            obj.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        }

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if(rb == null)
        {
            Debug.LogError("던지는 옵젝에 리지드바디 없다??");
        }

        rb.AddForce(usingPlayer.GetFacing() * throwForceAmount,ForceMode2D.Impulse);//힘 부여해서 던지기

        isDestroyed = true;
        Destroy(this.gameObject);

        return true;
    }
}
