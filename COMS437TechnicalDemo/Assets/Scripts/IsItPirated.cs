using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IsItPirated : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;

    private string[] allowedHosts =
    {
        "itch.io",
        "itch.zone",
        "file://",
        "localhost",
    };

    private void Start()
    {
        if (!Application.isEditor)
            ValidateURL();
    }

    private void ValidateURL()
    {
        if (!IsValidURL(allowedHosts))
        {
            playerMovement.setJumpable(false);
            GameObject.FindWithTag("TrueTerrain").SetActive(false);
            GameObject.FindWithTag("BrokenTerrain1").SetActive(true);
            GameObject.FindWithTag("BrokenTerrain2").SetActive(true);
            //SceneManager.LoadScene("PirateScene");
        }
    }

    private static bool IsValidURL(IEnumerable<string> urls)
    {
        return urls.Any(url => Application.absoluteURL.ToLower().Contains(url));
    }
}