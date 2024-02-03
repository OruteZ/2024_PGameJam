using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffectCreator : MonoBehaviour
{
    [SerializeField] GameObject destroyEffect;

    public void CreateDestroyEffect()
    {
        ParticleSystemRenderer effect = Instantiate(destroyEffect, transform.position, Quaternion.identity).GetComponent<ParticleSystemRenderer>();

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;

        effect.material.SetTexture("_MainTex", sprite.texture);
    }
}
