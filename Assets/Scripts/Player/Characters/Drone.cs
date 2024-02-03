using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Manager;

public class Drone : MonoBehaviour
{
    Player _usingPlayer = null;
    [SerializeField] private GameObject lazer;

    [SerializeField] private float mapWidth;
    [SerializeField] private Vector3 mapCenter;
    
    [SerializeField] private float moveTime;
    [SerializeField] private AnimationCurve moveCurve;
    
    [SerializeField] private float damage;
    [SerializeField] private float knockbackPower;

    private Vector3 _startPosition;
    private Vector3 _shootPosition;
    private Vector3 _targetPosition;

    [SerializeField] AnimationCurve lazerCurve;

    public void SetShootPosition()
    {
        //set shoot position to the center of the map with random x offset
        _shootPosition = new Vector3(mapCenter.x + UnityEngine.Random.Range(-mapWidth / 2, mapWidth / 2), transform.position.y, 0);
        
        //set start position to the current position
        _startPosition = transform.position;
        
        //set target position to the out of map position
        _targetPosition = new Vector3(mapCenter.x  + mapWidth, transform.position.y, 0);
    }

    public void Start()
    {
        SetShootPosition();
        StartCoroutine(FirstMoveCoroutine(_startPosition, _shootPosition));
    }

    public void SetOwner(Player player)
    {
        _usingPlayer = player;
    }

    private IEnumerator ShootCoroutine()
    {
        //shoot lazer
        lazer.SetActive(true);
        SoundManager.Instance.PlaySFX("explosion-m");
        CameraShaker.Instance.ShakeCamera(1, Vector2.right);
        //wait for 0.5 seconds
        yield return StartCoroutine(LazerCoroutine());
        lazer.SetActive(false);
        
        //move to shoot position
        StartCoroutine(OutMoveCoroutine(_shootPosition, _targetPosition));
    }

    IEnumerator LazerCoroutine()
    {
        float delayTime = 2f;

        float _time = 0f;

        Vector3 size = lazer.transform.localScale;
        Vector3 defaultSize = size;

        while (delayTime > _time)
        {
            size.x = defaultSize.x * lazerCurve.Evaluate(_time / delayTime);

            lazer.transform.localScale = size;

            _time += Time.deltaTime;

            yield return null;
        }

        yield break;
    }

    private IEnumerator FirstMoveCoroutine(Vector3 start, Vector3 target)
    {
        //move to target position with curve
        var time = 0f;
        while (time < moveTime)
        {
            time += Time.deltaTime;
            var t = time / moveTime;
            transform.position = Vector3.Lerp(start, target, moveCurve.Evaluate(t));
            yield return null;
        }

        StartCoroutine(ShootCoroutine());
    }
    
    private IEnumerator OutMoveCoroutine(Vector3 start, Vector3 target)
    {
        //move to target position with curve
        var time = 0f;
        while (time < moveTime)
        {
            time += Time.deltaTime;
            var t = time / moveTime;
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }


        //destroy drone
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != _usingPlayer)
            {
                //calculate knockback direction
                bool isRight = transform.position.x < player.transform.position.x;
                
                Vector2 direction = new Vector3(isRight ? -1 : 1, 1, 0);

                player.TakeDamage(damage, direction, knockbackPower);
            }
        }
    }
}
