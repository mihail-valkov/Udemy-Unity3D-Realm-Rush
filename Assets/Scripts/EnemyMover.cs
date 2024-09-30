using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

public class EnemyMover : MonoBehaviour
{
    
    [SerializeField] [Range(0.01f, 5f)] float moveInterval = 1f;
    [SerializeField] List<Waypoint> path;

    Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        RestartEnemy();
        enemy = GetComponent<Enemy>();
    }

    public void RestartEnemy()
    {
        if (path == null || path.Count == 0)
        {
            FindPath();
        }
        StartCoroutine(FollowPath());
    }

    void FindPath()
    {
        path = new List<Waypoint>();
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            if (!waypoint.IsPlaceable)
            {
                path.Add(waypoint);
            }
        }

        //make sure the path is ordered so from start to finish by traversing the path from left to right
        //path.Sort((a, b) => a.GetGridPos().x.CompareTo(b.GetGridPos().x));
    }

    IEnumerator FollowPath()
    {
        if (path == null || path.Count == 0) { yield break; }
        foreach (Waypoint waypoint in path)
        {
            //Move enemy to waypoint with sliding animation

            StartCoroutine(AnimateEnemy(waypoint.transform.position));
            yield return new WaitForSeconds(moveInterval);
        }

        //Return enemy to pool when it reaches the end of the path
        enemy.PenaltyOnEscape();
        gameObject.SetActive(false);
    }

    IEnumerator AnimateEnemy(Vector3 target)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;

        //rotate enemy to face the path, gradualy for 0.1 second
        var targetAngle = Vector3.SignedAngle(transform.forward, target - transform.position, Vector3.up);
        float rotationInterval = moveInterval / 5;
        if (Mathf.Abs(targetAngle) > 0.1f)
        {
            //Debug.Log("Rotating enemy to " + targetAngle);
            while (elapsedTime < rotationInterval)
            {
                transform.Rotate(Vector3.up, Vector3.SignedAngle(transform.forward,  target - transform.position, Vector3.up) * (elapsedTime / rotationInterval / 21f));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        // Ensure the final angle matches exactly
        transform.LookAt(target);

        var moveTime = moveInterval - elapsedTime;
        elapsedTime = 0f;
        
        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startingPosition, target, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is set to the target position
        transform.position = target;
    }
}
