using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject WinPannel;
    void Start()
    {
        WinPannel.SetActive(false);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            WinPannel.SetActive(true);
        }
    }
}
