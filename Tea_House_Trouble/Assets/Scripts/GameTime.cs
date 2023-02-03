using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    public float FillTime;
    public float minValue = 0f;
    public float maxValue = 140f;

    public Slider MusicTimeSlider;

    void Awake()
    {
        MusicTimeSlider = gameObject.GetComponent<Slider>();
    }

    void Update()
    {
        FillSlider();
    }

    public void FillSlider()
    {
        MusicTimeSlider.value += 1f * Time.deltaTime;
    }

    public void ResetTime()
    {
        MusicTimeSlider.value = minValue;
    }
}
