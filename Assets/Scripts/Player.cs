using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int maxhealth = 100;

        private int _curhealth;
        public int curHealth
        {
            get { return _curhealth; }
            set {_curhealth = Mathf.Clamp(value,0,maxhealth); }
        }
        public void Init()
        {
            curHealth = maxhealth;
        }

    }
    public PlayerStats playerstats = new PlayerStats();

    public int fallBoundry = -20;
    [SerializeField]
    private StatusBar statusIndicator;
    void Start()
    {
        playerstats.Init();
        if (statusIndicator != null)
        {
       
            statusIndicator.SetHealth(playerstats.curHealth,playerstats.maxhealth);
        }

        
    }
    void Update()
    {
        if (transform.position.y <= fallBoundry)
        {
            DamagePlayer(99999);
        }
    }
    public void DamagePlayer(int Damage)
    {
        playerstats.curHealth -= Damage;
        if (playerstats.curHealth <= 0)
        {
            GameMaster.KillPlayer(this);
        }
         if (statusIndicator != null)
        {

            statusIndicator.SetHealth(playerstats.curHealth, playerstats.maxhealth);
        }

    }

}
