using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{

  public GameObject pipesPrefab;
  public float pipesSpeed;

  private GameObject[] pipes;
  private const float spacing = 2f;

  void Start()
  {
    createPipes();
  }


  void Update()
  {
    // Move pipes towards player
    for (int i = 0; i < pipes.Length; i++)
    {
      pipes[i].transform.position -= new Vector3(0, 0, pipesSpeed * Time.deltaTime);
    }
  }

  private void createPipes()
  {
    pipes = new GameObject[5];
    Vector3 nextPipePos = transform.position;
    for (int i = 0; i < pipes.Length; ++i)
    {
      pipes[i] = GameObject.Instantiate(pipesPrefab);
      pipes[i].transform.position = nextPipePos;
      nextPipePos.z += spacing;
    }
  }

}
