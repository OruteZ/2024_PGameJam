using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteHole : MonoBehaviour
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
    [SerializeField] float attackForce = 10f;
    [SerializeField] float cameraShake = 0.2f;
    //bool isDelay = false;

    [Header("AnimationSetting")]
    [SerializeField] GameObject centerObj;

    // Start is called before the first frame update
    void Start()
    {
        nearObj = new List<Rigidbody2D>();
        //isDelay = false;

        _time = 0f;
        defaultScale = transform.localScale;

        StartCoroutine(centerAnim());
        StartCoroutine(SetAttackTimer());
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = defaultScale * sizeCurve.Evaluate(_time / lastingTime);

        _time += Time.deltaTime;
        if (_time >= lastingTime)//특정 시간 지나면 파괴되게 하기
        {
            Destroy(this.gameObject);
            return;
        }

        foreach (Rigidbody2D obj in nearObj)//계속 약간의 힘 부여
        {

            obj.AddForce((obj.position - (Vector2)transform.position).normalized * forceAmount);

        }
    }

    #region Attack Setting
    void GiveDamage(Player player)
    {
        //if (isDelay) return;
        //StartCoroutine("SetAttackTimer");//DPS 공격이 아닌 정해진 시간마다 한번씩 데미지 부여

        player.TakeDamage(attackAmount);
    }

    IEnumerator SetAttackTimer()//여기서 추가로 데미지 부여
    {
        //isDelay = false;

        WaitForSeconds wait = new WaitForSeconds(attackDelay);

        while (true)
        {
            yield return wait;

            foreach (Rigidbody2D obj in nearObj)
            {
                
                if (obj.CompareTag("Player"))
                {
                    GiveDamage(obj.GetComponent<Player>());
                }

                obj.AddForce((obj.position - (Vector2)transform.position).normalized * attackForce,ForceMode2D.Impulse);

            }

            if (CameraShaker.Instance) CameraShaker.Instance.ShakeCamera(cameraShake * transform.localScale.x / defaultScale.x);
        }

        //isDelay = true;
    }
    #endregion Attack Setting

    IEnumerator centerAnim()
    {
        //WaitForSeconds wait = new WaitForSeconds(0.1f);
        WaitForFixedUpdate wait = new WaitForFixedUpdate();

        while (true)
        {
            yield return wait;

            Vector3 pos = Vector2.zero;
            pos.x = Random.Range(-1f, 1f);
            pos.y = Random.Range(-1f, 1f);

            pos.Normalize();

            centerObj.transform.localPosition = pos * 0.1f;
        }
    }

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
