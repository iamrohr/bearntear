using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public int bulletDamage;

    private Vector3 shootDir;

    CameraShake cameraShake;

    public bool projectileIsMoving = true;

    private void Start()
    {
        cameraShake = GameObject.Find("CameraHolder").GetComponent<CameraShake>();
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(shootDir));
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        if(projectileIsMoving)
        {
            transform.position += shootDir * projectileSpeed * Time.deltaTime;
        }
    }

    void ProjectileWait()
    {
        projectileIsMoving = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(bulletDamage);
            StartCoroutine(cameraShake.Shake(0.3f, 0.2f));
            gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            projectileIsMoving = false;
            Invoke(nameof(ProjectileWait), 0.1f);

            if(gameObject.transform.localScale == new Vector3(0f, 0f, 0f))
                {
                    Destroy(gameObject);
                }
        }
    }
}
