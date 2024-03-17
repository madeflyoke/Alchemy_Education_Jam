using UnityEngine;

public class PooferAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private ParticleSystem _particle;
    private bool _canPlay;
    
    
    private void FixedUpdate()
    {
        if (_particle.isPlaying && _canPlay)
        {
            _source.Play();
            _canPlay = false;
        }

        if (_particle.isPlaying==false)
        {
            _canPlay = true;
        }
    }
}
