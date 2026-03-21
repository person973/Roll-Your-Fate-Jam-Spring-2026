using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// What the sound is being played from. This is used to determine what audio source the audio clip will play from.
/// player: player sounds are any sounds made by the player (walking, impact, chomp)
/// level: level sounds are any level geometry (box impact/death) and rotation
/// music: any background music that plays during the game
/// menu: sounds made by the menu (button hover/click)
/// </summary>
public enum SoundTarget
{
    player,
    level,
    music,
    menu
}

public class SoundManager : MonoBehaviour
{
    //Fields
    private static SoundManager _instance = null;
    /// <summary>
    /// In order to make this a singleton, _instance is used to create an instance.
    /// </summary>
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SoundManager();
            }

            return _instance;
        }
    }

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
    /// Constructor, must be private in order to be a singleton
    /// </summary>
    private SoundManager() {}

    //Methods
    /// <summary>
    /// Plays a sound somewhere in the game.
    /// </summary>
    /// <param name="target">What type of sound it is (player, level, music, menu)</param>
    /// <param name="sound">The audio clip for the sound</param>
    public void PlaySound(SoundTarget target, AudioClip sound)
    {
        if(target == SoundTarget.player)
        {
            _audioSourcePlayer.clip = sound;
            _audioSourcePlayer.Play();
        }
        else if (target == SoundTarget.level)
        {
            _audioSourceLevel.clip = sound;
            _audioSourceLevel.Play();
        }
        //This works on its own, but PlayMusic is preferred for playing music
        else if (target == SoundTarget.music)
        {
            _audioSourceMusic.clip = sound;
            _audioSourceMusic.Play();
        }
        else if (target == SoundTarget.menu)
        {
            _audioSourceMenu.clip = sound;
            _audioSourceMenu.Play();
        }
    }

    /// <summary>
    /// Loops a music track forever. REMEMBER TO CALL StopMusic!!!!!!!!!!
    /// </summary>
    /// <param name="music">The music to play</param>
    public void PlayMusic(AudioClip music)
    {
        _audioSourceMusic.clip = music;

        while(!_audioSourceMusic.isPlaying)
        {
            _audioSourceMusic.Play();
        }

        //CALL STOPMUSIC PLEASE
    }

    /// <summary>
    /// Stops the music audio source.
    /// </summary>
    public void StopMusic()
    {
        _audioSourceMusic.Stop();
    }
}
