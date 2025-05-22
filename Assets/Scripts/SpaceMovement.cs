using UnityEngine;

public class SpaceMovement : MonoBehaviour
{
    public Vector2 velocidadDesplazamiento = new Vector2(0.1f, 0.1f);
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        Vector2 offset = Time.time * velocidadDesplazamiento;
        material.mainTextureOffset = offset;
    }
}