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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger enter {this.transform.parent}");
        var otherCar = other.gameObject.GetComponent<Car>();
        if(otherCar != null){
            //Match speed and decelerate
            myCar.Speed = otherCar.Speed - deltaSpeed;
        }
    }
}
