using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveableBlock : MonoBehaviour
{
    [SerializeField]
    GameObject Player;

    //Temp fix since singletons don't wanna work
    [SerializeField]
    private GameObject _soundManagerGameObject;
    private SoundManager _soundManager;

    private Rigidbody2D rb;

    private void Awake()
    {
        _soundManager = _soundManagerGameObject.GetComponent<SoundManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// When a collision happens
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerhead"))
        {
            _soundManager.PlaySound(_soundManager.PlayerSounds[2]);
            //Add call to death function
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _soundManager.PlaySound(_soundManager.LevelSounds[1]);
        if (collision.gameObject.Equals(Player))
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.Equals(Player))
            rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
