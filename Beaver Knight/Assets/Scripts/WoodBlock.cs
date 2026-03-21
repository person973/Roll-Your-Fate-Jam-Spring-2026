using UnityEngine;

public class WoodBlock : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        //check of object colliding with is the Player
        if (collision.gameObject.CompareTag("Player"))
        {
            //check if the Wood Block's Z rotation is the same as the players
            //this ensures that you can only chew through the block if you are walking on the same axis of rotation
            if (collision.gameObject.transform.rotation.z == transform.rotation.z)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
