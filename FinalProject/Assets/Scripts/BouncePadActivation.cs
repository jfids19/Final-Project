using UnityEngine;
using System.Collections;

public class BouncePadActivation : MonoBehaviour
{
    [SerializeField] private GameObject bigBouncePad;
    [SerializeField] private GameObject smallBouncePad;
    [SerializeField] private float bigActivationDuration = 20f;
    [SerializeField] private float smallActivationDuration = 10f;
    [SerializeField] private float deactivationDuration = 10f;

    private bool isBigBouncePadActive = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateBouncePads());
    }

    IEnumerator ActivateBouncePads()
    {
        while (true)
        {
            if (isBigBouncePadActive)
            {
                DeactivateBigBouncePad();
                yield return new WaitForSeconds(deactivationDuration);
            }
            else
            {
                DeactivateSmallBouncePad();
                yield return new WaitForSeconds(deactivationDuration);
            }

            ToggleBouncePads();
            yield return new WaitForSeconds(isBigBouncePadActive ? bigActivationDuration : smallActivationDuration);
        }
    }

    void ToggleBouncePads()
    {
        if (isBigBouncePadActive)
        {
            ActivateSmallBouncePad();
            isBigBouncePadActive = false;
        }
        else
        {
            ActivateBigBouncePad();
            isBigBouncePadActive = true;
        }
    }

    void ActivateBigBouncePad()
    {
        bigBouncePad.SetActive(true);
    }

    void DeactivateBigBouncePad()
    {
        bigBouncePad.SetActive(false);
    }

    void ActivateSmallBouncePad()
    {
        smallBouncePad.SetActive(true);
    }

    void DeactivateSmallBouncePad()
    {
        smallBouncePad.SetActive(false);
    }
}
