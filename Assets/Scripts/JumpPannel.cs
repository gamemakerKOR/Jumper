using UnityEngine;

public class JumpPannel : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        player.SuperJump(5);
    }
}
