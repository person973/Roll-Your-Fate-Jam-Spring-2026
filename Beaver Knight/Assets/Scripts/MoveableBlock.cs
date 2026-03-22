using UnityEngine;

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
        _soundManager.PlaySound(_soundManager.LevelSounds[2]);  //Doesn't work

        if (collision.gameObject.CompareTag("playerhead"))
        {
            _soundManager.PlaySound(_soundManager.PlayerSounds[2]);
            Destroy(Player.gameObject);
        }
    }
}
