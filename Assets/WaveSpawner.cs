using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum spawnstate { spawning, waiting, counting }
    [System.Serializable]
    public class wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float Rate;

    }
    public wave[] waves;

    public Transform[] spawnpoints;
    private int nextwave = 0;
    public int Nextwave
    {
        get { return nextwave + 1; }
    }

    public float timeBetweenwave=5f;
   private float waveCountdown;
    public float Wavecountdown
    {
        get { return waveCountdown; }
    }

    private float searchcountdown = 1f;

    private spawnstate state = spawnstate.counting;
    public spawnstate State
    {
        get { return state; }

    }

     void Start()
    {
        waveCountdown = timeBetweenwave;
        
    }

     void Update()
    {
        if (state == spawnstate.waiting)
        {
            if (!enemyisAlive())
            {
                wavecompleted();
            }
            else
            {
                return;
            }

        }
        if (waveCountdown <= 0)
        {
            if (state != spawnstate.spawning)
            {

                StartCoroutine(spawnWave(waves[nextwave]));                       //start spawning 
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;  // start countdown
        }
        

        
    }

    void wavecompleted()
    {
        state = spawnstate.counting;
        waveCountdown = timeBetweenwave;
        if (nextwave+1>waves.Length-1)
        {
            nextwave = 0;
            Debug.Log("all Waves complete");           // used for change or increase dificulty (can increase stats of enemy)
        }
        nextwave++;
    }

    bool enemyisAlive()        //method to check enemy is alive or not
    {
        searchcountdown -= Time.deltaTime;
        if (searchcountdown <= 0f)
        {
            searchcountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
       
        return true;
    }
     IEnumerator  spawnWave(wave _wave) //method to looping spwanwaves
    {
        state = spawnstate.spawning;
        for(int i=0; i<_wave.count; i++){
            spawnEnwmy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.Rate);

                            
        }

        state = spawnstate.waiting;
        yield break;
    }

    void spawnEnwmy(Transform _enemy)  //spwaning logic
    {
       Transform sp = spawnpoints[Random.Range(0, spawnpoints.Length)];
        Instantiate(_enemy, sp.position, sp.rotation);
        Debug.Log("spaning Enemy "+_enemy.name);
    }

    }


