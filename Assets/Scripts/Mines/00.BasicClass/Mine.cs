using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] protected float damageAmount = 10f;

    protected bool isUsed = false;

    private void Start()
    {
        Init();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isUsed) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            ExcuteMine(collision.gameObject.GetComponent<Player>());
        }
    }

    protected virtual void Init()
    {
        isUsed = false;
    }



    protected virtual bool ExcuteMine(Player player)//실행이 잘되면 true 반환. 안되면 false 반환
    {
        if (isUsed) return false;
        isUsed = true;

        return true;
    }

    protected virtual void DestroyGameObj()
    {
        CreateDestroyEffect();

        Destroy(this.gameObject);
    }

    protected virtual void CreateDestroyEffect()
    {
        DestroyEffectCreator effect = GetComponent<DestroyEffectCreator>();
        if (effect) effect.CreateDestroyEffect();
    }
}
