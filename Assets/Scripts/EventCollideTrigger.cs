using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class EventCollideTrigger : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] float radius;
    SphereCollider sphereCollider;

    public UnityEvent onEventTrigger;

    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = this.GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        sphereCollider.radius = radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        onEventTrigger.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(this.transform.position, radius);
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
