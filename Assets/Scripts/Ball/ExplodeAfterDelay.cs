using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeAfterDelay : MonoBehaviour
{
    [SerializeField] float explodeRange = 2f;

    [SerializeField, Space(5f)] float explodeDamage = 10f;

    [SerializeField, Space(5f)] float explodePower = 10f;

    [SerializeField, Space(5f)] float explodeDelay = 5f;
    [SerializeField, Space(5f)] AnimationCurve effectCurve;
    float _time = 0f;
    SpriteRenderer renderer;
    
    bool isDestroyed = false;

    ContactFilter2D filter;

    void Start()
    {
        float _time = 0f;

        isDestroyed = false;

        filter = new ContactFilter2D();
        //filter.SetLayerMask()

        renderer = GetComponent<SpriteRenderer>();

        StartCoroutine("ExplodeDelay");
    }

    private void Update()
    {
        renderer.material.SetFloat("_HitValue", effectCurve.Evaluate(_time / explodeDelay));

        _time += Time.deltaTime;
    }

    IEnumerator ExplodeDelay()//일정시간 기다린 후에 폭발
    {
        yield return new WaitForSeconds(explodeDelay);//일정시간 기다림

        //이후 주위 오브젝트 폭발
        List<Collider2D> colls = new List<Collider2D>();
        Physics2D.OverlapCircle(transform.position, explodeRange, filter, colls);

        //Debug.Log(colls.Length);
        foreach (Collider2D coll in colls)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                coll.gameObject.GetComponent<Player>().TakeDamage(explodeDamage, (coll.transform.position - transform.position).normalized, explodePower);
                continue;
            }

            Rigidbody2D rb = coll.gameObject.GetComponent<Rigidbody2D>();
            if (rb)
            {
                rb.AddForce(explodePower * (coll.transform.position - transform.position).normalized,ForceMode2D.Impulse);
            }

        }

        DestroyEffectCreator effect = GetComponent<DestroyEffectCreator>();
        if (effect) effect.CreateDestroyEffect();

        Destroy(this.gameObject);//이후 제거

    }
}
