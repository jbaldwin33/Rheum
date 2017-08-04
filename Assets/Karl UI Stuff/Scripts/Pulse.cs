using UnityEngine;
using UnityStandardAssets.ImageEffects;
public class Pulse : MonoBehaviour {

    [SerializeField]
    VignetteAndChromaticAberration vignetteScript;
    [SerializeField]
    float startValue;
    [SerializeField]
    float endValue;
    [SerializeField]
    float transitionTime;
    bool dark;
    private float timer;

    void Start()
    {
        vignetteScript = GetComponent<VignetteAndChromaticAberration>();
        timer = transitionTime;
        dark = true;
    }

    void Update() {
        if (timer >= 0)
        {
            if (dark == true)
            {
                vignetteScript.intensity += 0.001f;
                timer -= Time.deltaTime;
            }
            else if (dark == false)
            {
                vignetteScript.intensity -= 0.001f;
                timer -= Time.deltaTime;
            }

        }
        else
        {
            timer = transitionTime;
            dark = !dark;
        }
    }

}

