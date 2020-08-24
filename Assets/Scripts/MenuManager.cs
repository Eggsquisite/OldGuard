using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject titleStuff, howToPlaystuff, creditStuff;
    // Start is called before the first frame update
    void Start()
    {
        howToPlaystuff.SetActive(false);
        creditStuff.SetActive(false);
        titleStuff.SetActive(true);
    }

    public void MainMenu()
    {
        howToPlaystuff.SetActive(false);
        creditStuff.SetActive(false);
        titleStuff.SetActive(true);
    }

    public void HowTo()
    {
        howToPlaystuff.SetActive(true);
        creditStuff.SetActive(false);
        titleStuff.SetActive(false);
    }

    public void Credits()
    {
        howToPlaystuff.SetActive(false);
        creditStuff.SetActive(true);
        titleStuff.SetActive(false);
    }
}
