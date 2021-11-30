using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFrontDetector : MonoBehaviour
{
    private Car myCar;

    private float deltaSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        myCar = this.transform.parent.gameObject.GetComponent<Car>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Trigger enter {other.gameObject.name} / {gameObject.name}", gameObject);
        if (other.transform.parent == null)
        {
            return;
        }
        Debug.Log($"Trigger enter {this.transform.parent}", other.gameObject);

        var otherCar = other.transform.parent.GetComponent<Car>();
        if (otherCar != null)
        {
            //Match speed and decelerate
            myCar.Speed = otherCar.Speed - deltaSpeed;
        }
    }
}
