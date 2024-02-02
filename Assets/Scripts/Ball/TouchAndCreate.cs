using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAndCreate : MonoBehaviour
{
    [SerializeField] GameObject createObj;

    bool isDestroyed = false;

    ContactFilter2D filter;


    void Start()
    {
        isDestroyed = false;

        filter = new ContactFilter2D();
        //filter.SetLayerMask()
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;//충돌 가능 오브젝트들과 만 충돌하기
        if (collision.gameObject.CompareTag("Player")) return;//충돌 된게 플레이어이면 무시하기

        if (isDestroyed) return;
        isDestroyed = true;

        Instantiate(createObj, transform.position, Quaternion.identity);

        Destroy(this.gameObject);

    }
}
