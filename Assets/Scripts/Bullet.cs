using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject FireBulletImpact;
    public AttributesManager enemyAtm;
    public AttributesManager playerAtm;
    public bool collided;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && !collided)
        {
            collided = true;
            var impact = Instantiate(FireBulletImpact, collision.contacts[0].point, Quaternion.identity) as GameObject;
            Destroy(gameObject);
            enemyAtm.DealDamage(playerAtm.gameObject);
            Destroy(FireBulletImpact, 2f);
        }
    }
}
    
