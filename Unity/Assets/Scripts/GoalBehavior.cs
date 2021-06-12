using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GoalBehavior : MonoBehaviour
{

    public GameObject chargingScreen;
    public Slider slider;

    public float sliderInstantiateTime = 1f;
    float elapsed;

    private float incrementInterval = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        chargingScreen.SetActive(false);
        slider.value = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {

        elapsed += Time.fixedDeltaTime;

        if (elapsed > sliderInstantiateTime)
        {
            chargingScreen.SetActive(true);
            slider.value += incrementInterval;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        elapsed = 0;
        chargingScreen.SetActive(false);
        slider.value = 0;
    }
}
