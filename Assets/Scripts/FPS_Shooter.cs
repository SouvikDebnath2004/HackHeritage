using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FPS_Shooter : MonoBehaviour
{
    [SerializeField] public float health = 500f;
    [SerializeField] public float damageBasic = 30f;
    public float currenthealth = 0f ;
    public int basicDamage = 30;

    public Enemy_AI attackDmg;
    private int enemyDmg;

    private Vector3 destination;

    [SerializeField] private Image healthImg;
    [SerializeField] public GameObject[] Attack;
    [SerializeField] public GameObject died;
    [SerializeField] public float projectileSpeed = 1.0f;
    [SerializeField] public float fireRate = 3f;

    public Camera camera; 
    public Transform CenterFirePoint;
    private float timeOfFire;

    

    // Start is called before the first frame update
    void Start()
    {
        currenthealth = health;
        enemyDmg = attackDmg.attack; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButton("Fire1") && Time.time >= timeOfFire)
        {
            timeOfFire = Time.time + 1 / fireRate;
            shootProjectiles();
        }

        if (currenthealth < 0)
        {
            died.SetActive(true);
            Debug.Log("You Died");
        }
        else
        {
            UpdateHealthBar(health, currenthealth);
            Debug.Log("Current" + currenthealth);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        currenthealth = currenthealth - enemyDmg;
    }



    public void UpdateHealthBar(float health, float currenthealth)
    {
        healthImg.fillAmount = currenthealth / health;
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
       // playerAtm.DealDamage(enemyAtm.gameObject);
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
