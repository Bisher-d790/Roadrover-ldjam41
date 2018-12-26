using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private float damage = 0.5f;
    [SerializeField] private ParticleSystem HitExplosionPrefab;
    [SerializeField] private AudioClip HitSFX; 
    [SerializeField] private float ExplosionForce = 300.0f;
    [SerializeField] private float ExplosionRadius = 5.0f;
    private GameManager gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            other.gameObject.GetComponent<Enemy>().Shot(damage);
            other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce * 100.0f, transform.position, ExplosionRadius, 3.0F);
            if (HitExplosionPrefab)
            {
                ParticleSystem explosion = Instantiate(HitExplosionPrefab);
                explosion.gameObject.transform.position = transform.position;
                explosion.Play();
                Destroy(explosion.gameObject, explosion.main.duration);
            }
            GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce * 100.0f, transform.position, ExplosionRadius, 3.0F);
            gm.GetComponent<AudioSource>().PlayOneShot(HitSFX);
            Destroy(this.gameObject);
        }
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

}
