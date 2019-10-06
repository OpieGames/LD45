using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    
    public float rotationSpeed = 10.0f;
    public float bounceMagnitude = 0.2f;
    public float bounceSpeed = 5.0f;
    private float yPos;
    // Start is called before the first frame update
    void Start()
    {
        yPos = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.eulerAngles = new Vector3 (this.transform.eulerAngles.x, this.transform.eulerAngles.y+rotationSpeed*Time.deltaTime, this.transform.eulerAngles.z);
        this.transform.position = new Vector3 (this.transform.position.x, yPos+(Mathf.Sin(Time.time*bounceSpeed)*bounceMagnitude), this.transform.position.z);
    }
}
