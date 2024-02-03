using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemCreator : ConsumedItemObj
{
    [SerializeField] ItemStorage itemStorage;//여기에 있는 아이템중 렌덤 생성함

    [Space(10f)]
    [SerializeField] float forceAmount = 5f;

    protected override void ConsumeItem()
    {
        base.ConsumeItem();

        GameObject obj = itemStorage.GetItem(transform.position);//렌덤 아이템 생성

        obj.GetComponent<Rigidbody2D>().AddForce(Vector2.up * forceAmount, ForceMode2D.Impulse);


        DestroyGameObj();
        //Destroy(this.gameObject);
    }
}
