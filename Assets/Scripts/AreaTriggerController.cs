using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaTriggerController : MonoBehaviour
{
    [SerializeField] private string areaToLoad;

    [SerializeField] private string areaTransitionName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FadeSystem.instance.fadeToBlack = true;

            PlayerMovementController.instance.areaTransitionName = areaTransitionName;

            StartCoroutine(changeScene());
        }
    }

    private IEnumerator changeScene()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(areaToLoad);

        FadeSystem.instance.fadeFromBlack = true;
    }
}
