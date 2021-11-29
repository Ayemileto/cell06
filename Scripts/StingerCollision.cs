using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StingerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Enemy")
        {
            //ContactPoint contact = other.GetContact(0);
            // GameObject.Instantiate(bulletDecal, contact.point, Quaternion.LookRotation(contact.normal)); ;
            // Destroy(gameObject);
            Debug.Log("Hit Enemy");
        }
    }

}
