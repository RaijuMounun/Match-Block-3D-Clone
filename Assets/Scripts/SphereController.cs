using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SphereController : MonoBehaviour
{
    public List<Sphere> sphereList = new List<Sphere>();
    public Sphere currentSphere;
    public Transform spawnPoint;

    private Touch touch;
    private Vector3 downPos, upPos;
    private bool dragStarted;


    private Sphere PickRandomSphere()
    {
        GameObject temp = Instantiate(sphereList[Random.Range(0,4)].gameObject, spawnPoint.position, Quaternion.identity);
        return temp.GetComponent<Sphere>();
    }


    private void Start() => currentSphere = PickRandomSphere();
    
    private IEnumerator SetSphere()
    {
        yield return new WaitForSeconds(1);
        currentSphere = PickRandomSphere();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began) return;
            dragStarted = true;
            downPos = upPos = touch.position;
        }

        if (!dragStarted) return;
        //Drag started
        if (touch.phase == TouchPhase.Moved) downPos = touch.position;
        
        if (currentSphere) currentSphere.rb.velocity = CalculateDirection() * 5;

        if (touch.phase != TouchPhase.Ended) return;
        //Launched
        downPos = touch.position;
        dragStarted = false;
        if (!currentSphere) return;
        currentSphere.rb.velocity = Vector3.zero;
        currentSphere.ThrowSphere();
        currentSphere = null;
        StartCoroutine(SetSphere());
    }

    private Vector3 CalculateDirection()
    {
        Vector3 temp = (downPos - upPos).normalized;
        temp.z = temp.x;
        temp.x = temp.y = 0;
        return temp;
    }

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
