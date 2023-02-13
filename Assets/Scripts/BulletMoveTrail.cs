using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveTrail : MonoBehaviour
{
   public int move_speed=200;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * move_speed);
        Destroy(gameObject, 1); 
        
    }
}
