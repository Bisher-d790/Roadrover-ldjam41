using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField] GameObject shootingPoint;
    [SerializeField] GameObject ProjectilePrefab;
    [SerializeField] GameObject PivotPoint;
    [SerializeField] AudioClip ShootSFX;
    [SerializeField] float Velocity = 100;
    [SerializeField] private float ProjectileDamage = 0.5f;
    [SerializeField] float DestroyAfter = 10;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.rotation = PivotPoint.transform.rotation;
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        this.GetComponent<AudioSource>().PlayOneShot(ShootSFX);
        GameObject projectile = Instantiate(ProjectilePrefab,
            shootingPoint.transform.position,
            shootingPoint.transform.rotation);
        projectile.GetComponent<Projectile>().SetDamage(ProjectileDamage);

        // Add velocity to the bullet 
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * Velocity;

        // Destroy the bullet after time seconds
        Destroy(projectile, DestroyAfter);
    }
}
