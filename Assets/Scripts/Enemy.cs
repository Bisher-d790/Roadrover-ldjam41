using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] subobjectsToClip;
    [SerializeField] private float clipAfterDistance;
    private bool isClipped;

    [SerializeField] private int DamageAmount = 1;
    [SerializeField] private float Health = 1;
    [SerializeField] private float SelfDamageAmount = 1;
    [SerializeField] private ParticleSystem DestroyExplosionPrefab;
    [SerializeField] private AudioClip EnemyExplosionSFX; 
    [SerializeField] private float ExplosionForce = 10000.0f;
    [SerializeField] private float ExplosionRadius = 10.0f;
    private GameManager gm;

    // Use this for initialization
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        isClipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            if (DestroyExplosionPrefab)
            {
                ParticleSystem explosion = Instantiate(DestroyExplosionPrefab);
                explosion.gameObject.transform.position = transform.position;
                explosion.Play();
                Destroy(explosion.gameObject, explosion.main.duration);
            }            
            gm.GetComponent<AudioSource>().PlayOneShot(EnemyExplosionSFX);
            Destroy(this.gameObject);

        }

        if(player)
        if((player.transform.position - this.transform.position).magnitude >= clipAfterDistance)
        {
            if (!isClipped)
            {
                foreach (GameObject obj in subobjectsToClip)
                {
                    obj.SetActive(false);
                }
            }
        }else if (isClipped)
        {
            foreach (GameObject obj in subobjectsToClip)
            {
                obj.SetActive(true);
            }
        }
    }



    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce * 100.0f, transform.position, ExplosionRadius, 3.0F);
            GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce * 100.0f, transform.position, ExplosionRadius, 3.0f);
            Health -= SelfDamageAmount;
            gm.DamageHealth(DamageAmount);

        }
    }

    public void Shot(float damage)
    {
        this.Health -= damage;
    }

    public void setHealth(float health){
        this.Health = health;
    }

}
