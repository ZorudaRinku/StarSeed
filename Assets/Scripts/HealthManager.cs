using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public int maxHearts = 3;
    private HeartsHealthVisuals HeartsHealthVisuals;
    public HeartsHealthSystem HealthSystem;
    
    // Start is called before the first frame update
    private void Start()
    {
        HealthSystem = new HeartsHealthSystem(maxHearts);
        HeartsHealthVisuals = transform.GetChild(3).GetChild(0).GetComponent<HeartsHealthVisuals>();
        HeartsHealthVisuals.SetHeartsHealthSystem(HealthSystem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
