using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NumberCruncher : MonoBehaviour, IPointerDownHandler
{
    public GameController gameController;
    public CellController target;
    public Vector3 startPos;
    public float speed;
    bool doneCrunching = false;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        if (doneCrunching)
        {
            float distX = startPos.x - transform.position.x;
            float distY = startPos.y - transform.position.y;
            transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(distY, distX) * Mathf.Rad2Deg);
            transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
        } 
        else 
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
            {
                target.Reset();
                doneCrunching = true;
            }
        }
    }

    public void Go()
    {
        float distX = target.transform.position.x - transform.position.x;
        float distY = target.transform.position.y - transform.position.y;
        transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(distY, distX) * Mathf.Rad2Deg);
    }
    
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Destroy(gameObject);
    }
}
