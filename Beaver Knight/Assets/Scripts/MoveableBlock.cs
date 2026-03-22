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

    private void Awake()
    {
        _soundManager = _soundManagerGameObject.GetComponent<SoundManager>();
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
    }
}
