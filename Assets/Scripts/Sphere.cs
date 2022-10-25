using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Sphere : MonoBehaviour
{
    [InfoBox("These are getting values automatically")]
    [FoldoutGroup("gr")]
    public Rigidbody rb;
    [FoldoutGroup("gr")]
    public int ID;
    [FoldoutGroup("gr")]
    public GameObject sphereParent;

    public int sphereValue;
    public GameObject nextSpherePrefab;


    private void Awake()
    {
        ID = GetInstanceID();
        rb = GetComponent<Rigidbody>();
        sphereParent = GameObject.Find("Spheres");
    }

    void CreatingForce()
    {
        rb.AddForce((Vector3.up - Vector3.right) * 150);
    }

    public void ThrowSphere()
    {
        rb.AddForce(-transform.right * 700);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sphere"))
        {
            if (collision.gameObject.TryGetComponent(out Sphere sphere))
            {
                if (sphere.sphereValue == sphereValue)
                {
                    if (ID < sphere.ID) return;
                    Destroy(this.gameObject);
                    Destroy(collision.gameObject);

                    if (nextSpherePrefab != null)
                    {
                        GameObject temp = Instantiate(nextSpherePrefab, transform.position, Quaternion.identity);
                        temp.transform.parent = sphereParent.transform;
                        if (temp.TryGetComponent(out Sphere newSphere))
                        {
                            newSphere.CreatingForce();
                        }
                    }


                }
            }
        }
    }

}
