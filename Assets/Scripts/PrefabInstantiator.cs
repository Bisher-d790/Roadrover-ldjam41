using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInstantiator : MonoBehaviour
{

    [SerializeField] private float InstantiateTimeout = 10.0f;
    [SerializeField] private GameObject ObjectPrefab;
    [SerializeField] private Transform InstantiateTransform;
    [SerializeField] private bool DestroyAferTime = false;
    [SerializeField] private float DistroyAfter = 10.0f;
    [SerializeField] private int ObjectLimit = 10;
    [SerializeField] private float EnemyPower = 3;

    private int objectsAlive = 0;

    private float lastInstantiateTime = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lastInstantiateTime += Time.deltaTime;
        if (lastInstantiateTime >= InstantiateTimeout && objectsAlive <= ObjectLimit)
        {
            lastInstantiateTime = 0;
            GameObject obj = Instantiate(ObjectPrefab, InstantiateTransform.position, InstantiateTransform.rotation * Quaternion.Euler(new Vector3(0, 90, 0)));
            if (obj.CompareTag("Enemy"))
            {
                obj.GetComponent<Enemy>().setHealth(EnemyPower);
            }
            if (DestroyAferTime) Destroy(obj, DistroyAfter);
            objectsAlive++;
        }
        if (objectsAlive >= ObjectLimit && lastInstantiateTime >= (DistroyAfter - InstantiateTimeout) * objectsAlive)
        {
            objectsAlive = 0;
        }
    }
}
