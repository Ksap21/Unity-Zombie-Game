using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    
    public int bulletDamage;
    [SerializeField] private HealthBar healthBar;
    // Start is called before the first frame update

    
  

    

    //public bool isDead;




   
private void OnCollisionEnter(Collision objectWeHit)
    {
        if (objectWeHit.gameObject.CompareTag("Target"))
        {
            print("hit " + objectWeHit.gameObject.name + "!");
         
            Destroy(gameObject);
            

        }
        if (objectWeHit.gameObject.CompareTag("Wall"))
        {
            print("hit " + objectWeHit.gameObject.name + "!");
           
            Destroy(gameObject);
        
        }

        if (objectWeHit.gameObject.CompareTag("Enemy"))
        {

            print("hit " + objectWeHit.gameObject.name + "!");

            if(objectWeHit.gameObject.GetComponent<Enemy>().isDead==false)
            { 
            objectWeHit.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);
            }
            // CreateBulletImpactEffect(objectWeHit);
            CreateBloodSprayEffect(objectWeHit);
           
            Destroy(gameObject);
        }
    }

    private void CreateBloodSprayEffect(Collision objectWeHit)
    {


        ContactPoint contact = objectWeHit.contacts[0];

        GameObject bloodSprayPrefab = Instantiate(GlobalReferences.Instance.bloodSprayEffect,
        contact.point, Quaternion.LookRotation(contact.normal)); 

        bloodSprayPrefab.transform.SetParent(objectWeHit.gameObject.transform);

    }


    //to leave a hole effect where the bullet hits
    //void CreateBulletImpactEffect(Collision objectWeHit)
    //{
    //    ContactPoint contact = objectWeHit.contacts[0];


    //    GameObject hole = Instantiate(Globalreference.Instance.bulletImpactEffectPrefab,
    //    contact.point, Quaternion.LookRotation(contact.normal));

    //    hole.transform.SetParent(objectWeHit.gameObject.transform);
    //}


    //public void TakeDamage(int damageAmount)
    //{
    //    HP -= damageAmount;

    //    if (HP <= 0)
    //    {

    //        int randomValue = Random.Range(0, 2);   //only 0 and 1


    //        isDead = true;



    //    }

    //}


}




