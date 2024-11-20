using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections.Specialized;

public class Distance_Calculator : MonoBehaviour
{

    public TMP_Text textdistance;

    public GameObject pointA;
    public GameObject pointB;
    public float health = 90f;    // Health attribute for the object
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       float distance= Vector3.Distance(pointA.transform.position, pointB.transform.position);
        textdistance.text = distance.ToString("F2"); // F2 formats to 2 decimal places
        

    }
}
