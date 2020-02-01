using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BobCabin : MonoBehaviour
{
    [SerializeField] GameState gameState;
    [SerializeField] Transform resetPos;
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        gameObject.tag = Tags.BobCabinTag;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameState.currentEntity = GameState.PlayableEntities.Ship;
        other.transform.position = resetPos.position;
        other.transform.rotation = resetPos.rotation;
    }
}
