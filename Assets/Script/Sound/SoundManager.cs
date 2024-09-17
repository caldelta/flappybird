using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField]
    private List<AudioClip> m_soundList;

    private AudioSource m_audioSource;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }
    public void Play(SoundType sound)
    {
        m_audioSource.Stop();
        m_audioSource.clip = m_soundList[(int)sound];
        m_audioSource.Play();
    }

    public bool IsPlaying(SoundType sound)
    {
        return m_audioSource.clip == m_soundList[(int)sound] && m_audioSource.isPlaying;
    }
}
