using UnityEngine;

public class Switcher : MonoBehaviour
{
    public GameObject[] lines;
    [Header("Animation Setting")]
    public float animationSpeed = 1f;
    public Transform handle;
    [Header("Trigger Zone")]
    public float triggeredDistance = 3f;
    public float frequency = 1f;
    public Color emissionColor = new Color(0, 1, 0);
    public int intensity = 2;
    [Header ("Audio Setting")]
    public AudioSource onAudio;
    public AudioSource offAudio;

    Material rubilnikMaterial;
    bool on = false;
    bool onAnimation = false;
    float time = 0f;
    float emission = 0f;
    Quaternion startAngle = Quaternion.Euler(0, -90.001f, 45);
    // Start is called before the first frame update
    void Start()
    {
        rubilnikMaterial = GetComponent<Renderer>().sharedMaterial;
        if (!gameObject.GetComponent<Collider>())
            gameObject.AddComponent<BoxCollider>();
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i]?.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, Camera.main.transform.position) <= triggeredDistance && !on)
        {
            rubilnikMaterial.EnableKeyword("_EMISSION");
            emission = Mathf.PingPong(Time.time * frequency, intensity + 1) - 1f;
            rubilnikMaterial.SetColor("_EmissionColor", emissionColor * Mathf.LinearToGammaSpace(emission));
        }
        else rubilnikMaterial.DisableKeyword("_EMISSION");
        if (onAnimation)
        {
            if (on)
            {
                Quaternion endAngle = Quaternion.Euler(0, -90.001f, -25);
                //onAudio?.Play();
                if (time <= animationSpeed)
                {
                    handle.localRotation = Quaternion.Slerp(startAngle, endAngle, time / animationSpeed);
                    time += Time.deltaTime;
                }
                else
                {
                    handle.localRotation = endAngle;
                    startAngle = endAngle;
                    for (int i = 0; i < lines.Length; i++)
                    {

                        lines[i]?.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    }
                    onAnimation = false;
                }
            }
            else
            {
                Quaternion endAngle = Quaternion.Euler(0, -90.001f, 45);
                //offAudio?.Play();
                if (time <= animationSpeed)
                {
                    handle.localRotation = Quaternion.Slerp(startAngle, endAngle, time / animationSpeed);
                    time += Time.deltaTime;
                }
                else
                {
                    handle.localRotation = endAngle;
                    startAngle = endAngle;
                    for (int i = 0; i < lines.Length; i++)
                    {
                        lines[i]?.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                    }
                    onAnimation = false;
                }
            }

        }
    }

    private void OnMouseDown()
    {
        if (!onAnimation)
        {
            on = !on;
            time = 0f;
            onAnimation = true;
        }
    }

}
