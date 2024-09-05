using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FPS_Shooter : MonoBehaviour
{
    public int health;
    public int attackDamage;
    private int currenthealth;

    public AttributesManager enemyAtm;
    public AttributesManager playerAtm;

    private Vector3 destination;

    [SerializeField] private Image healthImg;
    [SerializeField] public GameObject[] Attack;
    [SerializeField] public float projectileSpeed = 1.0f;
    [SerializeField] public float fireRate = 3f;

    public Camera camera; 
    public Transform CenterFirePoint;
    private float timeOfFire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButton("Fire1") && Time.time >= timeOfFire)
        {
            timeOfFire = Time.time + 1 / fireRate;
            shootProjectiles();
        }
    }

    public void UpdateHealthBar(float health, float currenthealth)
    {
        healthImg.fillAmount = currenthealth / health;
    }

    public void TakeDamage(int amount)
    {
        currenthealth =health - amount;
        if(currenthealth < 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            UpdateHealthBar(health, currenthealth);
            Debug.Log("Current" +currenthealth);
        }
    }

    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<AttributesManager>();
        if (atm != null)
        {
            atm.TakeDamage(attackDamage);
        }
    }

    void shootProjectiles()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f , 0));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else 
            destination = ray.GetPoint(1000);

        InstantiateProjectile(CenterFirePoint);
        playerAtm.DealDamage(enemyAtm.gameObject);
    }

    private void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(Attack[0], firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
    }   
    
    public void Fire()
    {
        if(Time.time >= timeOfFire)
        {
            timeOfFire = Time.time + 1 / fireRate;
            shootProjectiles();
        }        
    }

}
