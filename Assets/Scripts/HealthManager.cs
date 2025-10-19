using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public int maxHearts = 3;
    [SerializeField] private HeartsHealthVisuals HeartsHealthVisuals;
    public HeartsHealthSystem HealthSystem;
    
    // Start is called before the first frame update
    private void Start()
    {
        HealthSystem = new HeartsHealthSystem(maxHearts);
        HeartsHealthVisuals.SetHeartsHealthSystem(HealthSystem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
