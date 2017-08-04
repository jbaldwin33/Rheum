using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FootstepListener : MonoBehaviour
{

    private UnityAction<Vector3, string> footListener;
    public AudioClip[] stoneSounds;
    public AudioClip[] sandSounds;

    public Transform partTrans;
    public ParticleSystem parts;

    // Use this for initialization
    void Awake()
    {
        footListener = new UnityAction<Vector3, string>(footstepEventHandler);
    }

    private void OnEnable()
    {
        EventManager.StartListening<Footstep_Event, Vector3, string>(footListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening<Footstep_Event, Vector3, string>(footListener);
    }


    void footstepEventHandler(Vector3 pos, string type)
    {

        switch (type)
        {
            case "stone":
                int r = UnityEngine.Random.Range(0, stoneSounds.Length);
                for (int i = 0; i < stoneSounds.Length; i++)
                {
                    if (i == r)
                    {
                        AudioSource.PlayClipAtPoint(stoneSounds[i], pos);
                    }
                }
                break;
            case "sand":
                int l = UnityEngine.Random.Range(0, sandSounds.Length);
                for (int i = 0; i < stoneSounds.Length; i++)
                {
                    if (i == l)
                    {
                        AudioSource.PlayClipAtPoint(sandSounds[i], pos);
                    }
                }
                break;
            default:
                for (int i = 0; i < stoneSounds.Length; i++)
                {
                    AudioSource.PlayClipAtPoint(stoneSounds[i], pos);
                }
                break;

        }
        partTrans.position = pos;
        parts.Play();


    }
}
