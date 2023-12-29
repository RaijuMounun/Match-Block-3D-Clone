using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    Rigidbody rb;
    int ID;
    GameObject sphereParent;

    public int sphereValue;
    public GameObject nextSpherePrefab;

    [SerializeField] int creatingForce = 150, throwForce = 700;


    private void Awake()
    {
        ID = GetInstanceID();
        rb = GetComponent<Rigidbody>();
        sphereParent = GameObject.Find("Spheres");
    }

    void CreatingForce() => rb.AddForce((Vector3.up - Vector3.right) * creatingForce);

    public void ThrowSphere() => rb.AddForce(-transform.right * throwForce);


    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Sphere")) return;
        if (!collision.gameObject.TryGetComponent(out Sphere sphere)) return;
        if (sphere.sphereValue != sphereValue) return;
        if (ID >= sphere.ID) return;

        Destroy(this.gameObject);
        Destroy(collision.gameObject);

        if (nextSpherePrefab == null) return;
        GameObject temp = Instantiate(nextSpherePrefab, transform.position, Quaternion.identity);
        temp.transform.parent = sphereParent.transform;
        
        if (!temp.TryGetComponent(out Sphere newSphere)) return;
        newSphere.CreatingForce();
    }

}
