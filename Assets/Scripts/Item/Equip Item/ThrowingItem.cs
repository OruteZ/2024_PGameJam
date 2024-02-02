﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingItem : EquipItemObj//던지는 아이템
{
    [SerializeField] GameObject throwingObj;

    [SerializeField, Space(10f)] float throwForceAmount = 10f;

    protected override void InitWhenPick()
    {
        base.InitWhenPick();

        this.transform.SetParent(usingPlayer.transform);//플레이어 자식화
        this.transform.localPosition = Vector3.zero;//임시로 위치 플레이어 중심으로 잡음

        GetComponent<SpriteRenderer>().enabled = false;//스프라이트 꺼줌
    }

    protected override bool UseItem(out bool isDestroyed)
    {
        GameObject obj = Instantiate(throwingObj, transform.position, Quaternion.identity);

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if(rb == null)
        {
            Debug.LogError("던지는 옵젝에 리지드바디 없다??");
        }

        rb.AddForce(usingPlayer.GetFacing() * throwForceAmount);//힘 부여해서 던지기

        isDestroyed = true;
        Destroy(this.gameObject);

        return true;
    }
}