using UnityEngine;

public class WoodBlock : MonoBehaviour
{
    //Temp fix since singletons don't wanna work
    [SerializeField]
    private GameObject _soundManagerGameObject;
    private SoundManager _soundManager;

    private void Awake()
    {
        _soundManager = _soundManagerGameObject.GetComponent<SoundManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        _soundManager.PlaySound(_soundManager.LevelSounds[2]);

        //check of object colliding with is the Player
        if (collision.gameObject.CompareTag("Player"))
        {
            _soundManager.PlaySound(_soundManager.LevelSounds[1]);

            //check if the Wood Block's Z rotation is the same as the players
            //this ensures that you can only chew through the block if you are walking on the same axis of rotation
            if (collision.gameObject.transform.rotation.z == transform.rotation.z)
            {
                _soundManager.PlaySound(_soundManager.LevelSounds[0]);
                Destroy(this.gameObject);
            }
        }
    }
}
