using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePuzzle : MonoBehaviour
{
    private Vector3 dragOffset;
    private float speedDrag = 100f;
    public GameObject form;
    bool finish;
    public Camera cam;
    static int fullCount;
    static int current;
    public GameObject fullPanel;
    private void Start()
    {
        fullCount = fullPanel.transform.childCount;
    }
    private void OnMouseDown()
    {
        dragOffset = this.transform.position - GetMousePos();
    }
    private void OnMouseUp()
    {
        if (Mathf.Abs(this.transform.localPosition.x -form.transform.localPosition.x)<=5f &&
            Mathf.Abs(this.transform.localPosition.y - form.transform.localPosition.y) <= 5f)
        {
            this.transform.position = new Vector2(form.transform.position.x, form.transform.position.y);
            current += 1;
            if(current==fullCount)
            {
                FindObjectOfType<TriggersTemp>().EndMGameNew();
            }
        }
    }
    private void OnMouseDrag()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, GetMousePos() + dragOffset, speedDrag * Time.deltaTime);
    }

    private Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
