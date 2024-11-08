using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool collided;
    public GameObject FireBulletImpact;
    

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && !collided)
        {
            collided = true;            
            Debug.Log("Projectile collided");
            var impact = Instantiate(FireBulletImpact, collision.contacts[0].point,Quaternion.identity) as GameObject;
            Destroy(this.gameObject);
                       
            Destroy(FireBulletImpact,2f);
        }
    }
   
}
