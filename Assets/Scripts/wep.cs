using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wep : MonoBehaviour

{
    public float fireRate = 0;
    public int Damage = 10;
    public LayerMask WhatToHit;

    public Transform BulletTrail;
    public Transform MuzzleFlash;
    public Transform HitPrefab;

    float timespawnEffect = 0;
    public float RateOfSpawnEffect = 10;


    float TimetoFire = 0;
    Transform FirePoint;
    // Start is called before the first frame update
    void Awake()
    {

        FirePoint = transform.Find("FirePoint");
        if (FirePoint == null)
        {
            Debug.Log("error,Firepoint not FOUND");
        }

    }

    // Update is called once per frame
    void Update()
        
    {
        
        
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                shoot();
            }

        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > TimetoFire)
            {
                TimetoFire = Time.time + 1/ fireRate;
                shoot();
            }
        }
    }
    void shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(FirePoint.position.x, FirePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, WhatToHit);
        //Debug.DrawLine(firePointPosition, (mousePosition-firePointPosition)*100 , Color.red);
     
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
           Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage.");
            ENEMystats enemy = hit.collider.GetComponent<ENEMystats>();  //hitting the enemy
            if (enemy != null)
            {
                enemy.DAmageEnemy(Damage);
            }
        }
        if (Time.time >= timespawnEffect)
        {
            Vector3 hitpos;
            Vector3 hitnormal;
            if (hit.collider == null)
            {
                hitpos = (mousePosition - firePointPosition) * 3;
                hitnormal = new Vector3(9999, 9999, 9999);
            }
            else
            {
                hitpos = hit.point;
                hitnormal = hit.normal;
            }





              effect(hitpos,hitnormal);
            timespawnEffect = Time.time + 1 / RateOfSpawnEffect;
        }

    }
    void effect(Vector3 hitpos,Vector3 hitnormal) 
    {
        Transform trail = Instantiate(BulletTrail, FirePoint.position, FirePoint.rotation) as Transform;
        LineRenderer lr = trail.GetComponent<LineRenderer>();

        if (lr != null)
        {
            lr.SetPosition(0, FirePoint.position);
            lr.SetPosition(1, hitpos);
        }
        Destroy(trail.gameObject, 0.02f);
        if(hitnormal!=new Vector3(9999, 9999, 9999))
        {
           Transform hitparticles= Instantiate(HitPrefab,hitpos,Quaternion.FromToRotation(Vector3.right,hitnormal))as Transform;
            Destroy(hitparticles.gameObject, 1f);
        }


        Transform Clone = Instantiate(MuzzleFlash, FirePoint.position, FirePoint.rotation)as Transform;
        Clone.parent = FirePoint;
        float size = Random.Range(0.5f, 0.9f);

        Clone.localScale = new Vector3(size,size,size);
        
        Destroy(Clone.gameObject,0.02f);


    }


}
