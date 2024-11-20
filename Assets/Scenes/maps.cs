using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting.ReorderableList;
using UnityEditor.PackageManager;
using UnityEngine;

public class Maps : MonoBehaviour
{
  public int max_nodes = 2;
  [SerializeField]
  GameObject map;
  [SerializeField]

  GameObject nodes;

  public int grid_square_x;
  public int grid_square_y;
  float grid_width;
  float grid_height;

  public Vector2[,] grid;
  List<Vector2> taken = new List<Vector2>();



  public double spawn_counter = 0;

  public int current_scene = 0;
  int counter_max = 100;


  bool spawned = false;

  void Start()
  {
    grid_width = nodes.transform.lossyScale.x;
    grid_height = nodes.transform.lossyScale.y;
    grid_square_x = (int)(transform.localScale.x / grid_width);
    grid_square_y = (int)(transform.localScale.y / grid_height);

    grid = new Vector2[grid_square_x, grid_square_y];
    set_grid();


  }

  void set_grid()
  {
    for (int i = 0; i < grid_square_y; i++)
    {
      for (int j = 0; j < grid_square_x; j++)
      {
        int x = (int)((transform.position.x - transform.lossyScale.x / 2 + grid_width / 2) + (j * grid_width));
        int y = (int)((transform.position.y - (transform.lossyScale.y / 2) + grid_height / 2) + (i * grid_height));
        grid[j, i] = new Vector2(x, y);
        // Instantiate(nodes,grid[j,i],Quaternion.identity);
        // UnityEngine.Debug.Log((int)((transform.position.x-(transform.lossyScale.x/2))));

      }

    }


  }



  void Update()
  {
    switch (current_scene)
    {
      case 0:

        if (!spawned)
        {
          spawn_nodes();
        }

        break;
      case 1:



        spawn_counter += 0.03;
        if (spawn_counter >= counter_max && max_nodes <= 20)
        {
          if (taken.Count == 0)
          {
            spawn_base();
          }
          node[] spawned = FindObjectsByType<node>(0);
          foreach (var item in spawned)
          {
            Destroy(item.gameObject);
          }
          spawn_nodes(max_nodes);
          max_nodes += 2;
          spawn_counter = 0;
        }
        break;


    }



  }
  public void set_max(int a)
  {
    max_nodes = a;
  }
  public void spawn_base()
  {
    int x = UnityEngine.Random.Range(0, grid_square_x);
    int y = UnityEngine.Random.Range(0, grid_square_y);
    taken.Add(grid[x, y]);
    Instantiate(nodes, grid[x, y], Quaternion.identity);


  }
  public void spawn_nodes()
  {
    for (int y = 0; y < grid_square_y; y += 2)
    {
      for (int x = 0; x < grid_square_x; x += 2)
      {

        Instantiate(nodes, grid[x, y], Quaternion.identity);

      }

    }
    spawned = true;


  }
  public void spawn_nodes(int num)
  {


    List<Vector2> possible_pos = new List<Vector2>();
    int i = 1;
    while (i < num)
    {

      for (int y = 0; y < grid_square_y; y++)
      {
        for (int x = 0; x < grid_square_x; x++)
        {
          float a = grid[x, y].x - taken[taken.Count - 1].x;
          float b = grid[x, y].y - taken[taken.Count - 1].y;
          double dist = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));

          if ((dist > 40) && !cont(taken, grid[x, y]))
          {
            possible_pos.Add(grid[x, y]);
          }


        }

      }
      int place = UnityEngine.Random.Range(0, possible_pos.Count);
      //UnityEngine.Debug.Log(taken.Count);
      //UnityEngine.Debug.Log(possible_pos.Count);
      taken.Add(possible_pos[place]);
      Instantiate(nodes, possible_pos[place], Quaternion.identity);
      i++;



    }




  }
  bool cont(List<Vector2> list, Vector2 item)
  {
    foreach (Vector2 itm in list)
    {
      if ((itm.x == item.x) && (itm.y == item.y))
      {
        return true;
      }

    }
    return false;
  }


  void spawn_map(Vector2 pos)
  {
    Instantiate(map, pos, quaternion.identity);
  }

  void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.tag == "node")
    {
      UnityEngine.Debug.Log(col.gameObject.transform.position.x);

    }

  }

}
