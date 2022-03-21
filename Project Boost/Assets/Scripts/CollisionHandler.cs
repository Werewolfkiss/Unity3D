using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float delayOnCrash = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] float crashVolume = 0.5f;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] AudioClip finishSound;
    [SerializeField] float finishVolume = 0.5f;
    [SerializeField] ParticleSystem finishParticle;

    private AudioSource audioSource;

    bool isTransitioning;
    bool collisionsOn = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionsOn = !collisionsOn;
            Debug.Log($"CollisionsOn: {collisionsOn}");
        }
        if (Input.GetKeyDown(KeyCode.L)) { StartNextLevelSequence(); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || !collisionsOn)
        {
            return;
        }

        if (collision.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            switch (collision.gameObject.tag)
            {
                case "Player":
                case "Friendly":
                    Debug.Log("We hit a friendly");
                    break;
                case "Finish":
                    StartNextLevelSequence();
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
    }

    private void StartNextLevelSequence()
    {
        isTransitioning = true;
        StopRocket();
        PlaySound(finishSound, finishVolume);
        PlayParticle(finishParticle);
        Invoke(nameof(GoToNextLevel), delayOnCrash);
    }

    void StartCrashSequence()
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            StopRocket();
            PlaySound(crashSound, crashVolume);
            PlayParticle(crashParticle);
            Invoke(nameof(ReloadLevel), delayOnCrash);
        }
    }

    private void PlaySound(AudioClip audioClip, float volume)
    {
        audioSource.PlayOneShot(audioClip, volume);
    }

    private void PlayParticle(ParticleSystem particleSystem)
    {
        particleSystem.Play();
    }

    private void StopRocket()
    {
        var movement = GetComponent<Movement>();
        movement.StopRocket();
        movement.enabled = false;
        var audioSource = GetComponent<AudioSource>();
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    private void GoToNextLevel()
    {
        var nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings == nextSceneIndex)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadLevel()
    {
        var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }
}
