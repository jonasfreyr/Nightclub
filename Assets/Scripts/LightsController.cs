using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class LightsController : MonoBehaviour
{
    public Animator lightsAnimator;
    public Light2D globalLight;
    private static readonly int LightsOn = Animator.StringToHash("LightsOn");

    private bool _globalLightEnabled = true;
    public bool GlobalLightEnabled
    {
        get => _globalLightEnabled;
        set
        {
            _globalLightEnabled = value;
            globalLight.intensity = value ? 1f : 0.3f;
        }
    }

    private bool _nightLightsEnabled;
    public bool NightLightsEnabled
    {
        get => _nightLightsEnabled;
        set
        {
            _nightLightsEnabled = value;
            lightsAnimator.SetBool(LightsOn, value);
        }
    }


}
