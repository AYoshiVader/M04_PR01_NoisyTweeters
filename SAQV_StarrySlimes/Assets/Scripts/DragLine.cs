using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLine : MonoBehaviour
{
    LineRenderer _lineRenderer;
    Star _star;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _star = FindObjectOfType<Star>();
        _lineRenderer.transform.position = _star.transform.position;
        _lineRenderer.SetPosition(0, new Vector3(0f, 0f, -0.1f));
    }

    // Update is called once per frame
    void Update()
    {
        _lineRenderer.enabled = _star.IsDragging;
        _lineRenderer.SetPosition(1, _star.transform.position - _lineRenderer.transform.position);
    }
}
