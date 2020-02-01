using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BobCabin : MonoBehaviour
{
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        gameObject.tag = Tags.BobCabinTag;
    }
}
