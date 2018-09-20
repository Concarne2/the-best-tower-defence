using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    public float speed;
    public float damageValue;
    public ParticleSystem bulletImpact;

    private Transform target;
    private Rigidbody rb;

    public void setTarget(Transform targ)
    {
        target = targ;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(TrackTarget());
    }

    IEnumerator TrackTarget()
    {
        while (target != null)
        {
            rb.MovePosition(Vector3.MoveTowards(rb.position, target.position, speed * Time.deltaTime));
            yield return null;
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            EnemyHealth health = other.GetComponent<EnemyHealth>();
            health.TakeDamage(damageValue);
            ParticleSystem particle= Instantiate(bulletImpact, transform.position, transform.rotation);
            particle.gameObject.hideFlags = HideFlags.HideInHierarchy;
            gameObject.SetActive(false);
        }
    }
}
