using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Shooter : MonoBehaviour
{
    public Camera camera;
    private Vector3 destination;
    [SerializeField] public GameObject[] Attack;
    [SerializeField] public float projectileSpeed = 1.0f;
    public Transform CenterFirePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shootProjectiles();
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
    }

    private void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(Attack[0], firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
    }   
    
    public void Fire()
    {
        shootProjectiles();
    }

}
