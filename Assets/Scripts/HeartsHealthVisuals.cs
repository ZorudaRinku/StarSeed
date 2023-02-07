using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HeartsHealthVisuals : MonoBehaviour
{
    public Sprite fullHeartSprite;
    public Sprite halfHeartSprite;
    public Sprite emptyHeartSprite;

    private List<HeartImage> _heartImages;
    private HeartsHealthSystem _heartsHealthSystem;

    private void Start()
    {
        _heartImages = new List<HeartImage>();
        HeartsHealthSystem heartsHealthSystem = new HeartsHealthSystem(4);
        SetHeartsHealthSystem(heartsHealthSystem);
    }

    public void SetHeartsHealthSystem(HeartsHealthSystem heartsHealthSystem)
    {
        _heartsHealthSystem = heartsHealthSystem;

        List<HeartsHealthSystem.Heart> hearts = heartsHealthSystem.GetHeartList();
        
        foreach (var heart in hearts)
            CreateHeartImage().SetHeartFragments(heart.GetFragmentAmount());

        heartsHealthSystem.OnDamaged += HeartsHealthSystem_OnDamage;
        heartsHealthSystem.OnHeal += HeartsHealthSystem_OnHeal;
        heartsHealthSystem.OnDeath += HeartsHealthSystem_OnDeath;
    }

    private void HeartsHealthSystem_OnDeath(object sender, System.EventArgs e)
    {
        Debug.Log("Dead");
    }

    private void HeartsHealthSystem_OnDamage(object sender, System.EventArgs e)
    {
        RefreshAllHearts();
    }

    private void HeartsHealthSystem_OnHeal(object sender, System.EventArgs e)
    {
        RefreshAllHearts();
    }

    private void RefreshAllHearts()
    {
        List<HeartsHealthSystem.Heart> hearts = _heartsHealthSystem.GetHeartList();
        for (int i = 0; i < _heartImages.Count; i++)
        {
            HeartImage heartImage = _heartImages[i];
            HeartsHealthSystem.Heart heart = hearts[i];
            heartImage.SetHeartFragments(heart.GetFragmentAmount());
        }
    }
    
    private HeartImage CreateHeartImage()
    {
        GameObject heartGameObject = new GameObject("Heart", typeof(Image));
        heartGameObject.transform.SetParent(transform, false);
        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = fullHeartSprite;

        HeartImage heartImage = new HeartImage(this, heartImageUI);
        _heartImages.Add(heartImage);
        
        return heartImage;
    }
}

public class HeartImage
{
    private Image _heartImage;
    private HeartsHealthVisuals _heartsHealthVisuals;

    public HeartImage(HeartsHealthVisuals heartsHealthVisuals, Image heartImage)
    {
        _heartsHealthVisuals = heartsHealthVisuals;
        _heartImage = heartImage;
    }

    public void SetHeartFragments(int fragments)
    {
        switch (fragments)
        {
            case 0: _heartImage.sprite = _heartsHealthVisuals.emptyHeartSprite;
                break;
            case 1: _heartImage.sprite = _heartsHealthVisuals.halfHeartSprite;
                break;
            case 2: _heartImage.sprite = _heartsHealthVisuals.fullHeartSprite;
                break;
        }
    }
}