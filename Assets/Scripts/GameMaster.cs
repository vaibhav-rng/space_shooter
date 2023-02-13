using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    private static int _remainingLives=3;
    public static int RemainningLives
    {
        get { return _remainingLives; }
    }
    void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }
    public Transform PlayerPrefab;
    public Transform spawnpoint;
    public int spawnDelay= 4 ;
    public Transform spawnParticles;

    [SerializeField]
    private GameObject gameOverUI;

    public Transform EnemyspawnParticles;
    public IEnumerator RespwanPlayer()
    {
        GetComponent<AudioSource>().Play(); //Play the audio
        yield return new WaitForSeconds(spawnDelay);
        
        Instantiate(PlayerPrefab,spawnpoint.position,spawnpoint.rotation); // spawn Point
        Transform  Spawn_Particle_clone = Instantiate(spawnParticles, spawnpoint.position, spawnpoint.rotation) as Transform; // spawn particles

        Destroy(Spawn_Particle_clone.gameObject, 3f);

    }
    public void endGame()
    {
        Debug.Log("game Over");
        gameOverUI.SetActive(true);

    }
    public static  void KillPlayer(Player player1)
    {
        Destroy(player1.gameObject);
        _remainingLives -= 1;
        if (_remainingLives <= 0)
        {
            gm.endGame();
        }
        else
        {

        gm.StartCoroutine(gm.RespwanPlayer());
        }



    }   
    
    public static void killenemy(ENEMystats enemy)
    {
        gm._killenemy(enemy);
    }

    public void _killenemy(ENEMystats _enemy)
    {
     GameObject _clone =   Instantiate(_enemy.deathparticles.gameObject, _enemy.transform.position, Quaternion.identity)as GameObject;
        Destroy(_clone.gameObject, 2f);
        Destroy(_enemy.gameObject);
    }
    
}
