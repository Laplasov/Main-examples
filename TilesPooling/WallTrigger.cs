using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    private bool isTriggered = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.CompareTag("Player"))
        {
            isTriggered = true; 
            this.gameObject.GetComponent<Collider>().enabled = false;
            TilesPooling.Instance.CreatTile(transform.parent, this);
        }
    }
}
