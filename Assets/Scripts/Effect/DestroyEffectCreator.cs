using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffectCreator : MonoBehaviour
{
    [SerializeField] GameObject destroyEffect;

    [SerializeField, Space(5f)] float cameraShakeAmount = 0.5f;

    [SerializeField, Space(5f)] AudioClip audio;

    public void CreateDestroyEffect()
    {
        ParticleSystem effect = Instantiate(destroyEffect, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;

        effect.textureSheetAnimation.SetSprite(0,sprite);
        effect.textureSheetAnimation.AddSprite(sprite);

        effect.Play();

        if (Utility.Manager.SoundManager.Instance) Utility.Manager.SoundManager.Instance.PlaySFX(audio.name);

        if (CameraShaker.Instance) CameraShaker.Instance.ShakeCamera(cameraShakeAmount);
    }
}
