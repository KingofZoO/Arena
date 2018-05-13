using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreatingHeroScript : MonoBehaviour
{
    private Sprite[] heroSprites;
    private Image heroImage;
    private int currImage;
    private InputField heroName;
    private Button nextHeroButton;
    private int currHero = 0;
    private bool isHeroCreated = false;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        heroName = transform.Find("HeroName").GetComponent<InputField>();
        nextHeroButton = transform.Find("NextHeroButton").GetComponent<Button>();
        heroImage = transform.Find("HeroImage").GetComponent<Image>();
        heroImage.preserveAspect = true;

        heroSprites = Resources.LoadAll<Sprite>("Helms Sprite");
        currImage = 0;
        SetImage();
    }

    public void NextHero()
    {
        var hero = HeroDataContainer.Instance.GetHeroAt(currHero++);
        hero.heroSprite = heroImage.sprite;

        if (heroName.text == "")
            hero.heroName = "Hero" + currHero;
        else hero.heroName = heroName.text;
        heroName.text = null;

        if (isHeroCreated)
        {
            SceneManager.LoadScene("InventoryScene");
            return;
        }

        if (currHero == HeroDataContainer.Instance.HeroCount - 1)
        {
            isHeroCreated = true;
            nextHeroButton.GetComponentInChildren<Text>().text = "To the arsenal!";
        }
    }

    public void PrevImage()
    {
        currImage--;
        SetImage();
    }

    public void NextImage()
    {
        currImage++;
        SetImage();
    }

    private void SetImage()
    {
        if (currImage == heroSprites.Length)
            currImage = 0;
        else if (currImage < 0)
            currImage = heroSprites.Length - 1;

        heroImage.sprite = heroSprites[currImage];
    }
}
