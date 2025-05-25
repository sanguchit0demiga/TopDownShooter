using UnityEngine;

public class SpaceMovement : MonoBehaviour
{


    public float scrollSpeed = 0.1f;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
       
        rend.material.mainTextureOffset = new Vector2(0, offset);
    }
}