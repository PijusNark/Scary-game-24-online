using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Light spotLight;
    public Transform batteryLevelIndicator;
    public float dischargeSpeed = 1;
    public float energy = 100;

    void Start()
    {
        spotLight = GetComponentInChildren<Light>();
        spotLight.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && energy > 0f)
        {
            spotLight.enabled = !spotLight.enabled;
        }

        if (spotLight.enabled)
        {
            energy -= Time.deltaTime * dischargeSpeed;
        }

        if (energy < 50f)
        {
            spotLight.intensity = Random.Range(0.8f, 1f);
        }

        if (energy < 0f)
        {
            spotLight.enabled = false;
            energy = 0f;
        }

        batteryLevelIndicator.localScale = new Vector3(energy / 100f, 1f, 1f);
    }
}