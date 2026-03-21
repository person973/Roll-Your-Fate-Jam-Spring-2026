using UnityEngine;

public class MoveableBlock : MonoBehaviour
{
    [SerializeField]
    GameObject Player;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerhead"))
        {
            Destroy(Player.gameObject);
        }
    }
}
