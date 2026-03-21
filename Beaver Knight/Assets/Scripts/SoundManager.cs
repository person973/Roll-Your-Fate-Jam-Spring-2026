using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// What the sound is being played from. This is used to determine what audio source the audio clip will play from
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
}
