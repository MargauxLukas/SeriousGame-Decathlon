using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    const float minPathUpdateTime       = 0.2f;
    const float pathUpdateMoveThreshold = 0.5f;

    public Transform target;
    public float speed     = 1f;
    public float turnSpeed = 3f;
    public float turnDst   = 5f;
    public float stoppingDst = 10;
    public Animator playerAnimator;

    Path path;
    public PathFinding pf;

    public bool stuck = false;

    public void DeplacementPlayer(Vector3 newPos)
    {
        target.localPosition = newPos;
        StartCoroutine(UpdatePath());
    }

    void Update()
    {
        if (!stuck)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Ended:
                        target.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 32.08f));
                        StartCoroutine(UpdatePath());
                        break;

                    case TouchPhase.Moved:
                    /*target.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 32.08f));
                    StartCoroutine(UpdatePath());
                    break;*/

                    case TouchPhase.Began:
                        //Debug.Log("ENDED not supported");
                        break;

                    case TouchPhase.Stationary:
                        //Debug.Log("STATIONARY not supported");
                        break;

                    case TouchPhase.Canceled:
                        //Debug.Log("CANCELED not supported ");
                        break;
                }
            }
        }
    }

    public void OnPathFound(Vector2[] waypoints, bool pathSuccessfull)
    {
        if(pathSuccessfull)
        {
            path = new Path(waypoints, transform.position, turnDst, stoppingDst);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator UpdatePath()
    {
        /*if(Time.timeSinceLevelLoad < 0.3f)
        {
            yield return new WaitForSeconds(0.3f);
        }*/
        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;
        while(true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
            {
                PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }
        }
    }

    IEnumerator FollowPath()
    {
        bool followingPath = true;
        int pathIndex = 0;
        //transform.LookAt(path.lookPoints[0]);

        float speedPercent = 1;

        while(followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
            while(path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if(pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    playerAnimator.SetBool("DoesWalk", false);
                    break;
                }
                else
                {
                    pathIndex++;
                }
            }

            if(followingPath)
            {
                if (pathIndex >= path.slowDownIndex && stoppingDst > 0)
                {
                    speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst);
                    if(speedPercent < 0.01f)
                    {
                        followingPath = false;
                    }
                }

                //Quaternion targetRotation = Quaternion.LookRotation(new Vector3(path.lookPoints[pathIndex].x, path.lookPoints[pathIndex].y, 0) - transform.position);
                //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1);
                //transform.Translate(Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
                Vector3 direction = new Vector3(path.lookPoints[pathIndex].x - transform.position.x, path.lookPoints[pathIndex].y - transform.position.y, 0).normalized;
                //Debug.Log(direction);

                transform.position += direction * Time.fixedDeltaTime * speed; //* speedPercent;
                //transform.Translate(direction * Time.deltaTime * speed * speedPercent, Space.Self);

                playerAnimator.SetFloat("DirectionX", direction.x);
                playerAnimator.SetFloat("DirectionY", direction.y);
                playerAnimator.SetBool ("DoesWalk"  ,        true);
                //transform.Translate(new Vector3(path.lookPoints[pathIndex].x, path.lookPoints[pathIndex].y, 0), Space.Self);
            }
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if(path != null)
        {
            path.DrawWithGizmos();
        }
    }
}
