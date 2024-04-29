using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IsItPirated : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject brokenTerrain1;
    [SerializeField] GameObject brokenTerrain2;

    private string[] allowedHosts =
    {
        "itch.io",
        "itch.zone",
        //http://"localhost",
        //https://"localhost",
    };

    void Start()
    {
        if (Application.absoluteURL.Length > 0)
            text.SetText("Host: " + Application.absoluteURL);
        else
            text.SetText("Host: N/A");

        if (Application.isEditor)
            ValidateURL();
    }

    void Update()
    {
        if (GameObject.FindWithTag("Player").transform.position.y < -100)
        {
            SceneManager.LoadScene(3);
        }
    }

    private void ValidateURL()
    {
        if (Application.isEditor || (!IsValidURL(allowedHosts) && Application.absoluteURL.Length > 0))
        {
            playerMovement.setJumpable(false);
            GameObject.FindWithTag("TrueTerrain").SetActive(false);
            brokenTerrain1.SetActive(true);
            brokenTerrain2.SetActive(true);
        }
    }

    private static bool IsValidURL(IEnumerable<string> urls)
    {
        return urls.Any(url => Application.absoluteURL.ToLower().Contains(url));
    }
}