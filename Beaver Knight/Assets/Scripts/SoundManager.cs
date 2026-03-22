using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    //Fields
    [SerializeField]
    [Tooltip("The Audio Source that player sounds will be played from.")]
    private AudioSource _audioSourcePlayer;

    [SerializeField]
    [Tooltip("A list of all player sounds")]
    private List<AudioClip> _playerSounds;
    /// <summary>
    /// Player Step: footsteps for the player, used when the player walks
    /// Player Impact: when the player makes a large impact with something, such as falling to the floor
    /// Player Death: when the player dies
    /// </summary>
    public List<AudioClip> PlayerSounds
    {
        get
        {
            return _playerSounds;
        }
    }
    
    [SerializeField]
    [Tooltip("The Audio Source that level sounds will be played from.")]
    private AudioSource _audioSourceLevel;

    [SerializeField]
    [Tooltip("A list of all level sounds")]
    private List<AudioClip> _levelSounds;
    /// <summary>
    /// Box Death: when a box is killed in some way, like being eaten or touching spikes
    /// Box Push: when the player pushes a box
    /// Box Impact: when a box makes a large impact with something, such as falling to the floor
    /// Win: when the player collides with the other knight, triggering a level win
    /// Level Rotate: when the player rotates the level by any amount
    /// </summary>
    public List<AudioClip> LevelSounds
    {
        get
        {
            return _levelSounds;
        }
    }

    [SerializeField]
    [Tooltip("The Audio Source that music will be played from.")]
    private AudioSource _audioSourceMusic;

    [SerializeField]
    [Tooltip("A list of all music")]
    private List<AudioClip> _musicList;
    /// <summary>
    /// Beaver Adventure: waltzish string-focussed track, bouncy and hopeful, a bit whimsical. Probably for level 1
    /// Duet for Harpsichord and organ: short, slow, angry duet. Probably for main menu
    /// Escape Room: bright, jollyish canon. Probably for level 3
    /// Await: solumn, then turns to joy, and then sad again. Probably for level 2
    /// </summary>
    public List<AudioClip> MusicList
    {
        get
        {
            return _musicList;
        }
    }

    [SerializeField]
    [Tooltip("The Audio Source that menu sounds will be played from.")]
    private AudioSource _audioSourceMenu;

    [SerializeField]
    [Tooltip("A list of all menu sounds")]
    private List<AudioClip> _menuSounds;
    /// <summary>
    /// Menu Mouse Over: When the user mouses over a button on the menu
    /// Menu Button Click: When the user clicks a button on the menu
    /// </summary>
    public List<AudioClip> MenuSounds
    {
        get
        {
            return _menuSounds;
        }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public SoundManager() 
    {
        _playerSounds = new List<AudioClip>();
        _levelSounds = new List<AudioClip>();
        _musicList = new List<AudioClip>();
        _menuSounds = new List<AudioClip>();
    }

    //Methods
    public void Update()
    {
        //Change scene names later
        //Doesn't work
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            PlaySound(_musicList[0], true);
        }
        else if (SceneManager.GetActiveScene().name == "NewLevel2")
        {
            PlaySound(_musicList[3], true);
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            PlaySound(_musicList[2], true);
        }
        else if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "GameOver" ||
            SceneManager.GetActiveScene().name == "Win" || SceneManager.GetActiveScene().name == "LevelSelect")
        {
            PlaySound(_musicList[1], true);
        }
    }

    /// <summary>
    /// Plays a sound somewhere in the game.
    /// </summary>
    /// <param name="target">What type of sound it is (player, level, music, menu)</param>
    /// <param name="sound">The audio clip for the sound</param>
    public void PlaySound(AudioClip sound, bool checkIfPlaying = false)
    {
        if (PlayerSounds.Contains(sound))
        {
            if(checkIfPlaying)
            {
                if(_audioSourcePlayer.isPlaying)
                {
                    return;
                }
            }

            _audioSourcePlayer.clip = sound;
            _audioSourcePlayer.PlayOneShot(sound);
        }
        else if (LevelSounds.Contains(sound))
        {
            if (checkIfPlaying)
            {
                if (_audioSourceLevel.isPlaying)
                {
                    return;
                }
            }

            _audioSourceLevel.clip = sound;
            _audioSourceLevel.PlayOneShot(sound);
        }
        //This works on its own, but PlayMusic is preferred for playing music
        else if (MusicList.Contains(sound))
        {
            if (checkIfPlaying)
            {
                if (_audioSourceMusic.isPlaying)
                {
                    return;
                }
            }

            _audioSourceMusic.clip = sound;
            _audioSourceMusic.PlayOneShot(sound);
        }
        else if (MenuSounds.Contains(sound))
        {
            if (checkIfPlaying)
            {
                if (_audioSourceMenu.isPlaying)
                {
                    return;
                }
            }

            _audioSourceMenu.clip = sound;
            _audioSourceMenu.PlayOneShot(sound);
        }
    }

    /// <summary>
    /// Loops a music track forever. REMEMBER TO CALL StopMusic!!!!!!!!!!
    /// </summary>
    /// <param name="music">The music to play</param>
    public void PlayMusic(AudioClip music)
    {
        _audioSourceMusic.clip = music;
        _audioSourceMusic.PlayOneShot(music);
    }

    /// <summary>
    /// Stops the music audio source.
    /// </summary>
    public void StopMusic()
    {
        _audioSourceMusic.loop = false;
        _audioSourceMusic.Stop();
    }
}
