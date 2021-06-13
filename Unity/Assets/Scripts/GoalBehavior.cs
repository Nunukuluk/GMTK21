using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GoalBehavior : MonoBehaviour
{
    public GameObject GameManagement;
    public GameObject chargingScreen;
    public Slider slider;

    public float sliderInstantiateTime = 1f;
    float elapsed;

    private float incrementInterval = 0.02f;
    private bool won = false;
    // Start is called before the first frame update
    void Start()
    {
        GameManagement = GameObject.FindGameObjectWithTag("LevelManager");
        chargingScreen.SetActive(false);
        slider.value = 0;
    }

    void Update()
    {
        if (slider.value == 1 && !won)
        {
            GameManagement.GetComponent<LevelLogic>().ChangeScene();// if the loading slider has reached the max, do something
            won = true;
        }
    }

            private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Charger")
		{
			elapsed += Time.fixedDeltaTime;

			if (elapsed > sliderInstantiateTime)
			{
				chargingScreen?.SetActive(true);
				slider.value += incrementInterval;
			}
		}
	}

    private void OnTriggerExit2D(Collider2D other)
    {
        elapsed = 0;
        chargingScreen.SetActive(false);
        slider.value = 0;
    }
}
