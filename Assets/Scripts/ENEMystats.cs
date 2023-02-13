using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMystats : MonoBehaviour
{

    [System.Serializable]
    public class enemystats
    {
        public int maxHealth=100;

        private int _curHealth;
        public int curHEalth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }


        }
        public int damage;
        public void init()
        {
            curHEalth = maxHealth;
            
        }

    }   
        public enemystats stats = new enemystats();
       public Transform deathparticles;

    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {
        stats.init();

        if (statusIndicator != null)
        {
           statusIndicator.SetHealth1(stats.curHEalth, stats.maxHealth);
        }
        if (deathparticles == null)
        {
            Debug.LogError("no death particle Refrence found");
        }
    }
    public void DAmageEnemy(int Damage)
    {
        stats.maxHealth -= Damage;
        if (stats.maxHealth <= 0)
        {
            GameMaster.killenemy(this);
        }


        if (statusIndicator != null)
        {
            statusIndicator.SetHealth1(stats.curHEalth, stats.maxHealth);

        }

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth1(stats.curHEalth, stats.maxHealth);
        }

    }


    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        Player _player = collisionInfo.collider.GetComponent<Player>();
        if (_player != null)
        {
            _player.DamagePlayer(stats.damage);
            DAmageEnemy(99999999);
        }

        
    }




}
