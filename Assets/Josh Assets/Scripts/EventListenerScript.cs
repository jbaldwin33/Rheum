using UnityEngine;
using UnityEngine.Events;

public class EventListenerScript : MonoBehaviour {

    public string lastMesgRecvd = "";

    private UnityAction<string> simpleEventListener;
    GameObject player;
    AudioSource audio;
    AudioClip[] clips;
    public bool onSwitch = false;

    public bool seqSwitch1 = false;
    public bool seqSwitch2 = false;
    public bool seqSwitch3 = false;
    public bool seqSwitch4 = false;
    public bool movePlatform = false;
    public bool bossSwitch = false;

    public bool leftDown = false;
    public bool rightDown = false;
    private ParticleSystem particleLeft;
    private ParticleSystem particleRight;
    private float timer;
    private float timer2;
    void Awake()
    {
        simpleEventListener = new UnityAction<string>(SomeFunction);
        player = GameObject.Find("Player");
        audio = GetComponent<AudioSource>();
        //clips = new AudioClip[] { (AudioClip)Resources.Load("footstep_sand_1") as AudioClip, (AudioClip)Resources.Load("footstep_concrete_1") as AudioClip };//, (AudioClip)Resources.Load("Slash 3") as AudioClip, (AudioClip)Resources.Load("Drop to Metal") as AudioClip};
        //particleLeft = GameObject.Find("ParticleLeft").GetComponent<ParticleSystem>();
        //particleRight = GameObject.Find("ParticleRight").GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        EventManager.StartListening<SimpleEvent, string>(simpleEventListener);
    }

    void OnDisable()
    {
        EventManager.StopListening<SimpleEvent, string>(simpleEventListener);
    }

    void SomeFunction(string s)
    {
        lastMesgRecvd = s;
        if (onSwitch)
        {
            audio.PlayOneShot(clips[0]);
            onSwitch = false;
        }

        if (seqSwitch1 && seqSwitch2 && seqSwitch3 && seqSwitch4)
        {
            movePlatform = true;
        }

        /*
        if (leftDown && Time.timeSinceLevelLoad - timer > 0.5f)
        {
            timer = Time.timeSinceLevelLoad;
            audio.PlayOneShot(clips[1]);
            leftDown = false;
            particleLeft.Play();
        }
        else
        {
            particleLeft.Stop();
        }
        if (rightDown && Time.timeSinceLevelLoad - timer2 > 0.5f)
        {
            timer2 = Time.timeSinceLevelLoad;
            audio.PlayOneShot(clips[1]);
            rightDown = false;
            particleRight.Play();
        }
        else
        {
            particleRight.Stop();
        }
        */
    }
}
