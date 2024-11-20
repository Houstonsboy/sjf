using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections.Specialized;


public class NewBehaviourScript : MonoBehaviour
{

    public TextMeshProUGUI health;

       // Health attribute for the object
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int userhealth = 100;
        health.text = userhealth.ToString();
    }
}
