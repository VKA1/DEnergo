using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevaror : MonoBehaviour
{
    public Renderer button;
    public float waitForSec = 1f;
    public AudioSource onClickAudio;
    public GameObject previewImage;
    public float previewDistance = 2f;
    public Scene scene;

    float time = 0f;
    bool onClick = false;


    void Start()
    {
        if (!gameObject.GetComponent<Collider>())
            gameObject.AddComponent<BoxCollider>();
        button.sharedMaterial.DisableKeyword("_EMISSION");
        previewImage?.SetActive(false);
    }

    private void Update()
    {
        if (onClick)
        if (time < waitForSec)
        {
            time += Time.deltaTime;
        }
        else
        {
                //SceneManager.LoadSceneAsync(scene.buildIndex);
                Debug.Log("Go to anouther scene!!!");
                onClick = false;
        }
        
    }

    private void OnMouseDown()
    {
        button.sharedMaterial.EnableKeyword("_EMISSION");
        onClick = true;
        //onClickAudio.Play();
    }

    private void OnMouseEnter()
    {
        if (Vector3.Distance(transform.position, Camera.main.transform.position) <= previewDistance)
        previewImage?.SetActive(true);
    }

    private void OnMouseExit()
    {
        previewImage?.SetActive(false);
    }
}
