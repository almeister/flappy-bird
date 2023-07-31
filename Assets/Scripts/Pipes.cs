using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
  public float horizontalSpacing = 4f;

  public float yMaxTopPipe = 0f;
  public float yMinTopPipe = 0f;
  public float yMaxBottomPipe = 0f;
  public float yMinBottomPipe = 0f;

  public GameObject pipesPrefab;
  public float pipesSpeed;

  private float _initialPipesSpeed;
  private GameObject[] pipesContainers;

  public void StopMovement()
  {
    pipesSpeed = 0f;
  }

  public void StartMovement()
  {
    pipesSpeed = _initialPipesSpeed;
  }

  void Start()
  {
    _initialPipesSpeed = pipesSpeed;
    createPipes();
  }


  void Update()
  {
    // Move pipes towards player
    for (int i = 0; i < pipesContainers.Length; i++)
    {
      pipesContainers[i].transform.position -= new Vector3(0, 0, pipesSpeed * Time.deltaTime);
    }
  }

  private void createPipes()
  {
    pipesContainers = new GameObject[5];
    Vector3 nextPipePos = transform.position;
    for (int i = 0; i < pipesContainers.Length; ++i)
    {
      pipesContainers[i] = GameObject.Instantiate(pipesPrefab);
      GameObject pipePair = pipesContainers[i];

      nextPipePos.z += horizontalSpacing;
      pipePair.transform.position = nextPipePos;

      // Give random vertical spacing
      PlacePipes("pipes/Pipe-T", pipePair, yMaxTopPipe, yMinTopPipe);
      PlacePipes("pipes/Pipe-B", pipePair, yMaxTopPipe, yMinTopPipe);
    }
  }

  private void PlacePipes(string pipeName, GameObject pipePair, float yMin, float yMax)
  {
    float yPipe = Random.Range(yMin, yMax);
    Vector3 topPipeLocalPos = new Vector3(0, yPipe, 0);

    Transform topPipe = pipePair.transform.Find(pipeName);
    if (topPipe == null)
    {
      Debug.LogError("Could not find a pipe.");
      return;
    }

    topPipe.localPosition = topPipeLocalPos;
  }
}


