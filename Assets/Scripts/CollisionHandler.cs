using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private ManagerAgora agoraManager;

    void Start()
    {
        agoraManager = FindObjectOfType<ManagerAgora>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            agoraManager.Join();
            Debug.Log("Collided");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            agoraManager.Leave();
        }
    }
}
