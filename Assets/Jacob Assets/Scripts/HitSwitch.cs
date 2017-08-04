using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitSwitch : MonoBehaviour {

    public ParticleSystem particles;
    public Renderer rend;
    protected AudioSource audioSource;
    protected Stats stats;

    private int debounce = 0;
    private bool triggered;
    private Color initColor;

	// Use this for initialization
	protected void Start () {
        triggered = false;
        audioSource = GetComponent<AudioSource>();
        stats = GetComponent<Stats>();
        initColor = rend.material.color;
	}
	
	// Update is called once per frame
	protected void Update () {
		if (debounce > 0)
        {
            debounce--;
        }

        if (debounce == 0 && triggered == true)
        {
            triggered = false;
            stats.health = stats.maxHealth;
        }

        if (stats.health == 0 && triggered == false)
        {
            rend.material.color = Color.cyan;
            particles.Play();
            audioSource.Play();
            triggered = true;
            debounce = 50;
            trigger();
        }
        rend.material.color = Color.Lerp(rend.material.color, initColor, Time.deltaTime * 1f);

	}

    protected abstract void trigger();


}
