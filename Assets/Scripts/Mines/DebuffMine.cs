using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffMine : Mine
{
    //[SerializeField] protected float damageAmount = 10f;

    [Header("Debuff Setting")]
    [SerializeField] float frictionAmount = 0.2f;

    [SerializeField, Space(5f)] float debuffTime = 3f;

    PhysicsMaterial2D phMat;

    bool isUnDebuff = false;

    //bool isUsed = false;

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isUsed) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyDebuff(collision.gameObject.GetComponent<Player>());
        }
    }
    */

    protected override void Init()
    {
        base.Init();
        isUnDebuff = false;
    }

    protected override bool ExcuteMine(Player player)
    {
        //Debug.Log("버프 적용됨");

        if (!base.ExcuteMine(player)) return false;

        ApplyDebuff(player);

        return true;
    }

    void ApplyDebuff(Player player)
    {

        player.TakeDamage(damageAmount);

        //이후 디버프 기능 들어가면 디버프 추가
        phMat = player.GetComponent<Rigidbody2D>().sharedMaterial;

        phMat.friction *= frictionAmount;

        player.GetComponent<Rigidbody2D>().AddForce(player.GetFacing() * 10f,ForceMode2D.Impulse);

        GetComponent<SpriteRenderer>().enabled = false;//스프라이트 끄기
        GetComponent<Collider2D>().enabled = false;

        Debug.Log("버프 적용됨");

        Invoke(nameof(UnApplyDebuff), debuffTime);
    }

    void UnApplyDebuff()
    {
        if (isUnDebuff) return;
        isUnDebuff = true;

        phMat.friction /= frictionAmount;

        Debug.Log("버프 해제됨");
        Destroy(this.gameObject);
    }

    private void OnDestroy()//에디터에서 피직스 메테리얼 값 유지하기 위해 이 함수 적용
    {
        UnApplyDebuff();
    }
}
