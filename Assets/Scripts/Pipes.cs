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
  public int pipesCount = 10;
  public GameObject pipesRecyclePoint;
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
      // Recycle pipe if it has reached the off-screen point recycle point
      if (pipesContainers[i].transform.position.z <= pipesRecyclePoint.transform.position.z)
      {
        pipesContainers[i].transform.localPosition += pipesCount * new Vector3(0, 0, horizontalSpacing);
      }

      pipesContainers[i].transform.localPosition -= new Vector3(0, 0, pipesSpeed * Time.deltaTime);
    }
  }

  private void createPipes()
  {
    pipesContainers = new GameObject[pipesCount];
    Vector3 nextPipePos = new Vector3();
    for (int i = 0; i < pipesContainers.Length; ++i)
    {
      pipesContainers[i] = GameObject.Instantiate(pipesPrefab);
      GameObject pipePair = pipesContainers[i];
      pipePair.transform.SetParent(transform);

      nextPipePos.z += horizontalSpacing;
      pipePair.transform.localPosition = nextPipePos;

      // Give random vertical spacing
      PlacePipes("pipes/Pipe-T", pipePair, yMinTopPipe, yMaxTopPipe);
      PlacePipes("pipes/Pipe-B", pipePair, yMinBottomPipe, yMaxBottomPipe);
    }
  }

  private void PlacePipes(string pipeName, GameObject pipePair, float yMin, float yMax)
  {
    float yPipe = Random.Range(yMin, yMax);
    Vector3 topPipeLocalPos = new Vector3(0, yPipe, 0);

    Transform topPipe = pipePair.transform.Find(pipeName);
    if (topPipe == null)
    {
      Debug.LogError($"Could not find a pipe with tag {pipeName}");
      return;
    }

    topPipe.localPosition = topPipeLocalPos;
  }
}


