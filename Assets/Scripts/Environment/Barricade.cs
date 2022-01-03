using System;
using System.Collections;
using UnityEngine;

public class Barricade : MonoBehaviour
{

    [SerializeField] private float health, maxHealth, shakeTime, shakeSpeed, shakeMul, fadeTime;
    [SerializeField] private AttackType attackType;
    private Transform _transform;
    private SpriteRenderer spr;
    private BoxCollider2D bc2d;
    private bool alive;

    private void Awake()
    {
        _transform = transform;
        spr = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        alive = true;
        health = maxHealth;
    }

    public void TakeDamage(float damage, AttackType attackType)
    {
        if (this.attackType != attackType) return;

        health -= damage;
        StartCoroutine(Shake());

        if (health <= 0 && alive)
        {
            StartCoroutine(Die());
            alive = false;
        }
    }
    
    private IEnumerator Die()
    {
        bc2d.enabled = false;
        
        var color = spr.color;
        float t = 0;
        while (t < 1)
        {
            color.a = 1 - t * t;
            spr.color = color;
            t += Time.deltaTime / fadeTime;
            yield return null;
        }
        Destroy(_transform.parent.gameObject);
        yield return null;
    }

    private IEnumerator Shake()
    {
        var startPosition = _transform.localPosition;
        float t = 0;
        while (t < shakeTime)
        {
            var localPosition = _transform.localPosition;
            localPosition.x = startPosition.x + Mathf.Sin(Time.time * shakeSpeed) * shakeMul;
            localPosition.y = startPosition.y + Mathf.Sin(Time.time * shakeSpeed) * shakeMul;
            _transform.localPosition = localPosition;
            t += Time.deltaTime / shakeTime;
            yield return null;
        }

        _transform.localPosition = startPosition;
        yield return null;
    }
}
