using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLine : MonoBehaviour
{
    LineRenderer _lineRenderer;
    Bird _bird;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _bird = FindObjectOfType<Bird>();
        _lineRenderer.transform.position = _bird.transform.position;
        _lineRenderer.SetPosition(0, new Vector3(0f, 0f, -0.1f));
    }

    // Update is called once per frame
    void Update()
    {
        _lineRenderer.enabled = _bird.IsDragging;
        _lineRenderer.SetPosition(1, _bird.transform.position - _lineRenderer.transform.position);
    }
}
