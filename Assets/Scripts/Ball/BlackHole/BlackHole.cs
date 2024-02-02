using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    List<Rigidbody2D> nearObj;//블랙홀의 영향을 받을 오브젝트들

    [SerializeField, Space(5f)] float forceAmount = 10f;

    [Header("Destroy Setting")]
    [SerializeField] float lastingTime = 3f;
    [SerializeField] AnimationCurve sizeCurve;

    float _time = 0f;//지난 시간
    Vector3 defaultScale = Vector3.one;

    [Header("Damage Setting")]
    [SerializeField] float attackDelay = 1f;
    [SerializeField] float attackAmount = 3f;
    bool isDelay = false;

    // Start is called before the first frame update
    void Start()
    {
        nearObj = new List<Rigidbody2D>();
        isDelay = false;

        _time = 0f;
        defaultScale = transform.localScale;
        //Destroy(this.gameObject, lastingTime);//일정 시간 이후에 자동 파괴
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = defaultScale * sizeCurve.Evaluate(_time / lastingTime);
        _time += Time.deltaTime;
        if(_time >= lastingTime)//특정 시간 지나면 파괴되게 하기
        {
            Destroy(this.gameObject);
            return;
        }

        foreach(Rigidbody2D obj in nearObj)
        {
            if (obj.CompareTag("Player"))
            {
                GiveDamage(obj.GetComponent<Player>());
            }

            obj.AddForce(-(obj.position - (Vector2)transform.position).normalized * forceAmount);

        }
    }

    #region Attack Setting
    void GiveDamage(Player player)
    {
        if (isDelay) return;
        StartCoroutine("SetAttackTimer");//DPS 공격이 아닌 정해진 시간마다 한번씩 데미지 부여

        player.TakeDamage(attackAmount);
    }

    IEnumerator SetAttackTimer()
    {
        isDelay = false;

        yield return new WaitForSeconds(attackDelay);

        isDelay = true;
    }
    #endregion Attack Setting

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (!rb) return;

        if (nearObj.Contains(rb)) return;

        nearObj.Add(rb);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (!rb) return;

        if (!nearObj.Contains(rb)) return;

        nearObj.Remove(rb);
    }
}
