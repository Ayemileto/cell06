using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StingerController : MonoBehaviour
{

    [SerializeField]
    private GameObject bulletDecal;

    private float speed = 50f;
    private float timeToDestroy = 3f;

    public Vector3 target { get; set; }
    public bool hit { get; set; }

    private void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (!hit && Vector3.Distance(transform.position, target) < .01f)
        {
            Destroy(gameObject);
        }
    }

    /* void OnCollisionEnter(Collision col)
     {
         if (col.collider.tag == "Enemy")
         {
             //ContactPoint contact = other.GetContact(0);
             // GameObject.Instantiate(bulletDecal, contact.point, Quaternion.LookRotation(contact.normal)); ;
             Destroy(col.gameObject);
             Debug.Log("Hit Enemy");
         }
     }
 */
}
