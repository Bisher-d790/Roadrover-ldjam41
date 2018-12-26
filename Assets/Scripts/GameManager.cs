using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    [SerializeField] private int PlayerHealth = 3;
    [SerializeField] private ParticleSystem DamageExplosionPrefab;
    [SerializeField] private AudioClip HitExplosionSFX;
    [SerializeField] private AudioClip CarExplosionSFX;

    [SerializeField] private int thisLevel;
    [SerializeField] private Transform LevelStart;
    [SerializeField] private int nextLevel;

    [SerializeField] private GameObject HealthBar = null;


    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < PlayerHealth;i++) HealthBar.GetComponent<UnityEngine.UI.Text>().text += "-";
        Player.transform.SetPositionAndRotation(LevelStart.position,LevelStart.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamageHealth(int amount)
    {
        if (DamageExplosionPrefab)
        {
            ParticleSystem explosion = Instantiate(DamageExplosionPrefab);
            explosion.gameObject.transform.position = Player.transform.position;
            explosion.Play();
            Destroy(explosion.gameObject, explosion.main.duration);
        }
        PlayerHealth -= amount;
        HealthBar.GetComponent<UnityEngine.UI.Text>().text = "";
        for (int i = 0; i < PlayerHealth; i++) HealthBar.GetComponent<UnityEngine.UI.Text>().text += "-";
        if (PlayerHealth <= 0)
        {
            Destroy(Player);
            GetComponent<AudioSource>().PlayOneShot(HitExplosionSFX);
            StartCoroutine(GameOver(3.0f));
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(HitExplosionSFX);
        }
    }

    private IEnumerator GameOver(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        SceneManager.LoadScene(thisLevel);
    }

    public void GameWin()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void Quit(){
        Application.Quit();
    }


}

