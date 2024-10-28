using System.Collections;
using UnityEngine;
using Vuforia;

public class Movimiento : MonoBehaviour
{
    public GameObject model;
    public ObserverBehaviour[] imageTargets;
    public int currentTarget ;
    public float speed = 1.0f;
    private bool isMoving;
    private int control = 0;

    public void moverSiguienteMarcador()
    {
        if (!isMoving)
        {
            StartCoroutine(MoverModelo());
        }
    }

    private IEnumerator MoverModelo()
    {
        isMoving = true;
        ObserverBehaviour target = GetNextTarget();
        if (target == null)
        {
            isMoving = false;
            yield break;
        }
        bool isPositive = checaAvance(currentTarget);

        if (isPositive)
        {
            control++;
            if (control >= 2)
            {
                control = 0; // Reinicia el contador
                currentTarget++; // Avanza al siguiente objetivo
            }
        }
        else
        {
            control = 0; // Reinicia el contador
            currentTarget = 0; // Regresa al inicio
        }
        
        Vector3 starPosition = model.transform.position;
        Vector3 endPosition = target.transform.position;

        float journey = 0;

        while (journey <= 1f)
        {
            journey += Time.deltaTime * speed;
            model.transform.position = Vector3.Lerp(starPosition, endPosition, journey);
            yield return null;
        }

        isMoving = false;
    }

    private ObserverBehaviour GetNextTarget()
    {
        foreach (ObserverBehaviour target in imageTargets)
        {
            if (target != null && (target.TargetStatus.Status == Status.TRACKED || target.TargetStatus.Status == Status.EXTENDED_TRACKED))
            {
                return target;
            }
        }
        return null;
    }

    private bool checaAvance(int index)
    {
        if (index == 0 || index == 1)
        {
            return true;
        }
        else if (index == 2 || index == 3)
        {
            return false;
        }

        return false;
    }
}
