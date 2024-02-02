using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFrontItemObj : MultiUsableItiemObj//전방을 공격하는 아이템 클래스
{
    [SerializeField, Space(5f)] float damageAmount = 40f;

    [SerializeField, Space(5f)] float forceAmount = 10f;

    [Header("Attack Range Setting")]
    [SerializeField] float attackCenterOffset = 2f;
    [SerializeField] Vector2 attackAreaSize = Vector2.one;//트리거 된 콜라이더 참조해서 사용해도 되지만 혹시 몰라서 따로 값 지정해서 사용

    //오브젝트 판별 시 사용
    ContactFilter2D filter;
    List<Collider2D> colls;

    #region Editor Setting
    private void OnDrawGizmos()//공격범위 그려주는 함수
    {
        Gizmos.color = Color.green;

        //Debug.Log(usingPlayer);

        if (usingPlayer)
        {
            Gizmos.DrawWireCube((Vector2)usingPlayer.GetPlayerThrowTsf().position + usingPlayer.GetFacing() * attackCenterOffset,attackAreaSize);
        }
        else
        {
            Gizmos.DrawWireCube(this.transform.position + this.transform.right * attackCenterOffset, attackAreaSize);
        }
    }
    #endregion Editor Setting

    protected override void InitWhenPick()
    {
        base.InitWhenPick();

        filter = new ContactFilter2D();

        colls = new List<Collider2D>();

        this.transform.SetParent(usingPlayer.transform);//플레이어 자식화
        this.transform.localPosition = Vector3.zero;//임시로 위치 플레이어 중심으로 잡음

        GetComponent<SpriteRenderer>().enabled = false;//스프라이트 꺼줌
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        foreach (Collider2D coll in GetComponentsInChildren<Collider2D>()) coll.enabled = false;
    }


    protected override bool UseItem(out bool isDestroyed)
    {
        colls.Clear();
        Physics2D.OverlapBox(
            (Vector2)usingPlayer.GetPlayerThrowTsf().position + usingPlayer.GetFacing() * attackCenterOffset
            , attackAreaSize, 0f, filter, colls);

        foreach(Collider2D coll in colls)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                coll.gameObject.GetComponent<Player>().TakeDamage(damageAmount, usingPlayer.GetFacing(), forceAmount);//바라보는 방향으로 힘 부여
                continue;
            }

            Rigidbody2D rb = coll.gameObject.GetComponent<Rigidbody2D>();
            if (rb)
            {
                rb.AddForce(forceAmount * usingPlayer.GetFacing().normalized, ForceMode2D.Impulse);
            }
        }

        return base.UseItem(out isDestroyed);
    }
}
