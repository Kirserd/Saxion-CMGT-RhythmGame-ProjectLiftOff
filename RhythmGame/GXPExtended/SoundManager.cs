using System.Collections.Generic;
using GXPEngine;
using GXPEngine.Core;

public static class SoundManager
{
    #region Fields

    private static Dictionary<string, SoundChannel> _loopingSoundChannels;
    private static SoloudSoundSystem _soLoudSoundSystem = new SoloudSoundSystem();

    #endregion

    #region Init
    public static void Init()
    {
        _soLoudSoundSystem.Init();
        _loopingSoundChannels = new Dictionary<string, SoundChannel>();
    }
    #endregion

    #region Controllers
    public static void Play(string filename, bool streaming = true) => _loopingSoundChannels.Add(filename, LoadSound(filename, true, streaming));

    public static void PlayOnce(string filename, bool streaming = false) => LoadSound(filename, false, streaming);

    private static SoundChannel LoadSound(string filename, bool looping, bool streaming)
    {
        Sound sound = new Sound(Settings.AssetsPath + "Sounds\\" + filename + ".wav", looping, streaming);
        SoundChannel soundChannel = sound.Play(volume: Settings.Volume);
        return soundChannel;
    }

    public static SoundChannel GetSoundChannel(string key)
    {
        if (_loopingSoundChannels.ContainsKey(key))
            return _loopingSoundChannels[key];

        return null;
    }

    public static void AddSoundChannel(string key, SoundChannel soundChannel)
    {
        if (_loopingSoundChannels.ContainsKey(key))
            return;

        _loopingSoundChannels.Add(key, soundChannel);
    }

    public static void StopSound(string key)
    {
        if (_loopingSoundChannels.ContainsKey(key))
        {
            _loopingSoundChannels[key].Volume = 0;
            _loopingSoundChannels[key].Stop();
            _loopingSoundChannels.Remove(key);
        }
    }

    public static void StopAll()
    {
        List<string> keysToRemove = new List<string>();

        foreach (string soundChannel in _loopingSoundChannels.Keys)
            keysToRemove.Add(soundChannel);

        foreach (string soundChannel in keysToRemove)
            StopSound(soundChannel);
    }

    public static void PauseSound(string key)
    {
        if (_loopingSoundChannels.ContainsKey(key))
            _loopingSoundChannels[key].IsPaused = true;
    }

    public static void ResumeSound(string key)
    {
        if (_loopingSoundChannels.ContainsKey(key))
            _loopingSoundChannels[key].IsPaused = false;
    }

    public static void SetVolume(string key, float volume)
    {
        if (_loopingSoundChannels.ContainsKey(key))
            _loopingSoundChannels[key].Volume = volume;
    }

    public static void SetPan(string key, float pan)
    {
        if (_loopingSoundChannels.ContainsKey(key))
            _loopingSoundChannels[key].Pan = pan;
    }
    public static bool ContainsKey(string key) => _loopingSoundChannels.ContainsKey(key);
    #endregion
}