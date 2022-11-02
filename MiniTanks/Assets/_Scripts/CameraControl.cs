using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    #region Variables
    public float dampTime = 0.2f;
    public float screenEdgeBuffer = 4f;
    public float minDistance = 6.5f;
    public Transform[] targets;

    Camera _camera;
    float _zoomSpeed;
    Vector3 _moveVelocity;
    Vector3 _desiredPosition;
    #endregion

    #region SystemMethods
    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();

    }
    void FixedUpdate()
    {
        Move();
        Zoom();
    }
    #endregion

    #region CustomMethods
    void Move()
    {
        FindAveragePosition();
        transform.position = Vector3.SmoothDamp(transform.position, _desiredPosition, ref _moveVelocity, dampTime);
    }

    void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;

        for (int i = 0; i < targets.Length; i++)
        {
            if (!targets[i].gameObject.activeSelf)
                continue;

            averagePos += targets[i].position;
            numTargets++;
        }
        if (numTargets > 0)
            averagePos /= numTargets;

        averagePos.y = transform.position.y;

        _desiredPosition = averagePos;
    }

    void Zoom()
    {
        float requiredSize = FindRequiredSize();
        _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, requiredSize, ref _zoomSpeed, dampTime);
    }

    float FindRequiredSize()
    {
        Vector3 desiredLocalPos = transform.InverseTransformPoint(_desiredPosition);
        float size = 0f;
        for (int i = 0; i < targets.Length; i++)
        {
            if (!targets[i].gameObject.activeSelf)
                continue;

            Vector3 targetLocalPos = transform.InverseTransformPoint(targets[i].position);
            Vector3 desiredPostoTarget = targetLocalPos - desiredLocalPos;

            size = Mathf.Max(size, Mathf.Abs(desiredPostoTarget.y));
            size = Mathf.Max(size, Mathf.Abs(desiredPostoTarget.x) / _camera.aspect);
        }
        size += screenEdgeBuffer;
        size = Mathf.Max(size, minDistance);
        return size;
    }
    #endregion
}
