using UnityEngine;

public class Switcher : MonoBehaviour
{
    public GameObject[] lines;
    public float animationSpeed = 1f;
    public Transform handle;
    public AudioSource onAudio;
    public AudioSource offAudio;

    bool on = false;
    bool onAnimation = false;
    float time = 0f;
    Quaternion startAngle = Quaternion.Euler(0, -90.001f, 45);
    // Start is called before the first frame update
    void Start()
    {
        if (!gameObject.GetComponent<Collider>())
            gameObject.AddComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
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

                        lines[i]?.GetComponent<Renderer>().sharedMaterial.EnableKeyword("_EMISSION");
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
                        lines[i]?.GetComponent<Renderer>().sharedMaterial.DisableKeyword("_EMISSION");
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
