using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IsItPirated : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] TextMeshProUGUI text;

    private string[] allowedHosts =
    {
        "itch.io",
        "itch.zone",
        "file://",
        "localhost",
    };

    private void Start()
    {
        if (Application.absoluteURL.Length > 0)
            text.SetText(Application.absoluteURL);
        else
            text.SetText("Nothing to see here...");
        if (!Application.isEditor)
            ValidateURL();
    }

    private void ValidateURL()
    {
        if (!IsValidURL(allowedHosts) && Application.absoluteURL.Length > 0)
        {
            playerMovement.setJumpable(false);
            GameObject.FindWithTag("TrueTerrain").SetActive(false);
            GameObject.FindWithTag("BrokenTerrain1").SetActive(true);
            GameObject.FindWithTag("BrokenTerrain2").SetActive(true);
        }
    }

    private static bool IsValidURL(IEnumerable<string> urls)
    {
        return urls.Any(url => Application.absoluteURL.ToLower().Contains(url));
    }
}