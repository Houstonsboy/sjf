using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class node : MonoBehaviour
{
   

    [SerializeField]
    GameObject path_text;
    
    int max_lanes;
     public enum state { good, sick, very_sick, terminal, healed, dead };
      public enum path { right, left, up, down };
    String[] states = { "sick", "very_sick", "terminal","good" };
    String record_state;
    public state current;
    int lane_counter;
    float max_wait;
    float wait_counter;
    // Start is called before the first frame update 
    void Start()
    {
        max_lanes = UnityEngine.Random.Range(1, 4);
        lane_counter = 0;
        int set = UnityEngine.Random.Range(0, 7);
        wait_counter = 0;
        switch (set)
        {
            case 0:
                current = state.sick;
                record_state = states[set];
                max_wait = 100;
                break;
            case 1:
                current = state.very_sick;
                record_state = states[set];
                max_wait = 50;
                break;
            case 2:
                current = state.terminal;
                record_state = states[set];
                max_wait = 25;
                break;
                default:
                current = state.good;
                record_state = states[3];
                max_wait = 25;
                break;
        }
    }



    // Update is called once per frame
    void Update()
    {
  update_state();
    }

    void update_state()
    {
        switch (current)
        {
            case state.good:
                break;
            case state.sick:
                count();
                sicken();
                break;
            case state.very_sick:
                count();
                  sicken();
                break;
            case state.terminal:
                count();
                  sicken();
                break;
            case state.healed:
                count();
                  sicken();
                break;
            case state.dead:
                count();
                  sicken();
                break;
        }

    }
    void sicken()
    {
        if (wait_counter > max_wait)
        {
            switch (current)
            {

                case state.sick:
                    current = state.very_sick;
                    break;
                case state.very_sick:
                    current = state.terminal;
                    break;
                case state.terminal:
                    current = state.dead;
                    break;


            }


        }

    }
    void count()
    {
        wait_counter += (float)(0.0003);
    }
   /* void set_lanes()
    {
        //fix this so that the angle between the two nodes to be connected
        // will be found as set as the quanterion for the instantaiteof the lanes
       
        node[] nodes = node.FindObjectsOfType<node>();
        node[] filtered = new node[max_lanes];
        int found = 0;
        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].lane_counter > nodes[i].max_lanes)
            {

                nodes[i].transform.position = new Vector2(1000, 300);

            }
        }
        for (int i = 0; i < nodes.Length; i++)
        { 
            
            float val = (float)Math.Sqrt(Math.Pow(nodes[0].transform.position.x-transform.position.x, 2) + (float)Math.Pow(nodes[0].transform.position.y-transform.position.y, 2));
           
            for (int j = 1; j < nodes.Length; j++)
            {
                float comp_val = (float)Math.Sqrt(Math.Pow(nodes[j].transform.position.x-transform.position.x, 2) + (float)Math.Pow(nodes[j].transform.position.y, 2));
                if (val > comp_val)
                {
                    node a = nodes[j];
                    node b = nodes[j-1];
                    nodes[j] = b;
                    nodes[j-1] = a;
                }
                else
                {
                    val=comp_val;

                }

            }

        }
        for(int i=0;i<max_lanes;i++)
        {
            filtered[i]=nodes[i];
        }




    }
    */

}
