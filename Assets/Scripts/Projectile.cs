using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool collided;
    public GameObject FireBulletImpact;
    public AttributesManager enemyAtm;
    public AttributesManager playerAtm;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && !collided)
        {
            collided = true;
            var impact = Instantiate(FireBulletImpact, collision.contacts[0].point,Quaternion.identity) as GameObject;
            Destroy(gameObject);
            playerAtm.DealDamage(enemyAtm.gameObject);
            Destroy(FireBulletImpact,2f);
        }
    }
}
