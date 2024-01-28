using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundtrackController : MonoBehaviour
{
    private Transform _monsterLocation;
    private Transform _playerLocation;

    private bool _threatIsActive = false;
    private bool _soundtrackIsPlaying = false;

    private AudioSource _source;
    [SerializeField] private AudioMixerSnapshot _threatSnapshot;
    [SerializeField] private AudioMixerSnapshot _noThreatSnapshot;

    [SerializeField] private float threatDistance = 10;
    [SerializeField] private float transitionTime = 1;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_threatIsActive)
        {
            EvaluateThreat();
        }
    }

    private void EvaluateThreat()
    {
        SetCurrentPlayerGameobject();
        var distance = Vector3.Distance(_monsterLocation.position, _playerLocation.position);
        if (!_soundtrackIsPlaying)
        {
            if (distance <= threatDistance) PlaySoundtrack();
        }
        else
        {
            if (distance > threatDistance) StopSoundtrack();
        }
    }

    public void ActivateThreat(GameObject monster, GameObject player)
    {
        _threatIsActive = true;
        _monsterLocation = monster.transform;
        _playerLocation = player.transform;
    }

    public void DeactivateThreat()
    {
        _threatIsActive = false;
        _monsterLocation = null;
        _playerLocation = null;
        StopSoundtrack();
    }

    private void PlaySoundtrack()
    {
        _soundtrackIsPlaying = true;
        _threatSnapshot.TransitionTo(transitionTime);
        _source.PlayScheduled(transitionTime + AudioSettings.dspTime);
    }

    private void StopSoundtrack()
    {
        _soundtrackIsPlaying = false;
        _noThreatSnapshot.TransitionTo(transitionTime);
        Invoke(nameof(AudioSourceStop), transitionTime);
    }

    private void AudioSourceStop()
    {
        _source.Stop();
    }

    private void SetCurrentPlayerGameobject()
    {
        GameObject playerWalking = GameObject.Find("Human(Clone)");
        if(playerWalking != null)
        {
            _playerLocation = playerWalking.transform;
        }
        else
        {
            _playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
