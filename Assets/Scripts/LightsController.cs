using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public enum Lightshow
{
    None = 0,
    Lightshow1 = 1
}

public class LightsController : MonoBehaviour
{
    public Animator lightsAnimator;
    public Light2D globalLight;
    private static readonly int LightsOn = Animator.StringToHash("LightsOn");
    
    private bool _nightLightsEnabled;
    private static readonly int LightshowKey = Animator.StringToHash("Lightshow");

    public bool NightLightsEnabled
    {
        get => _nightLightsEnabled;
        set
        {
            _nightLightsEnabled = value;
            lightsAnimator.SetBool(LightsOn, value);
        }
    }

    public void SetLightShow(Lightshow lightshow)
    {
        lightsAnimator.SetInteger(LightshowKey, (int) lightshow);
    }


}
