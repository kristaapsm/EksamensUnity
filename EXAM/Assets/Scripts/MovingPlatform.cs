using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private WaypointPath _waypointPath;

    [SerializeField]
    private float _speed;

    private int _targetWPI;

    private Transform _prevWP;
    private Transform _targetWP;

    private float _timeToWP;
    private float _elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        TargetNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;

        float elapsedPerc = _elapsedTime / _timeToWP;
       // elapsedPerc = Mathf.SmoothStep(0, 1, elapsedPerc);
        transform.position = Vector3.Lerp(_prevWP.position, _targetWP.position, elapsedPerc);
        transform.rotation = Quaternion.Lerp(_prevWP.rotation, _targetWP.rotation, elapsedPerc);

        if (elapsedPerc >= 1)
        {
            TargetNextWaypoint();
        }
    }

    private void TargetNextWaypoint()
    {
        _prevWP = _waypointPath.GetWaypont(_targetWPI);
        _targetWPI = _waypointPath.GetNextWaypointIndex(_targetWPI);
        _targetWP = _waypointPath.GetWaypont(_targetWPI);

        _elapsedTime = 0;

        float distanceToWP = Vector3.Distance(_prevWP.position, _targetWP.position);
        _timeToWP = distanceToWP / _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
}