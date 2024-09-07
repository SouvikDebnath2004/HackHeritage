using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject FireBulletImpact;
    
    private float DealDamage = 20f;
   
    public bool collided;

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && !collided)
        {
            collided = true;            
            Debug.Log("Bullet collided");            
            var impact = Instantiate(FireBulletImpact, collision.contacts[0].point, Quaternion.identity) as GameObject;
            Destroy(this.gameObject);            
            Destroy(FireBulletImpact, 2f);
        }
    }
}
    
 