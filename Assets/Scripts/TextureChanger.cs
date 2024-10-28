using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    public Material[] materials; // Arreglo de materiales que deseas alternar
    private int currentMaterialIndex = 0; // Índice del material actual
    public Renderer modelRenderer;

    void Start()
    {
        // Obtén el componente Renderer del modelo
        modelRenderer = GetComponent<Renderer>();
        if (materials.Length > 0)
        {
            // Establece el material inicial
            modelRenderer.material = materials[currentMaterialIndex];
        }
    }

    void Update()
    {
        // Verifica si hay un toque en la pantalla
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Raycast para detectar si el toque está sobre el objeto
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Verifica si el objeto tocado es este
                if (hit.transform == transform)
                {
                    CambiarMaterial();
                }
            }
        }
    }

    void CambiarMaterial()
    {
        // Cambiar al siguiente índice de material en el arreglo
        currentMaterialIndex++;

        // Verifica si el índice excede el tamaño del arreglo
        if (currentMaterialIndex >= materials.Length)
        {
            currentMaterialIndex = 0; // Regresa al primer material
        }

        // Actualizar el material del modelo
        modelRenderer.material = materials[currentMaterialIndex];
    }
}
