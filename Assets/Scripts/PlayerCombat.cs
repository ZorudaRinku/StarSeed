using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCombat : MonoBehaviour
{
    public float cooldown = .75f;
    public GameObject arrow;
    private float _cooldownCount = 0;
    private bool _ready = true;

    public int ArrowSpeedMultiplier = 2;

    private Animator armsAnimator;

    // Start is called before the first frame update
    void Start()
    {
        armsAnimator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) // Shoot Up
        { 
            if (_cooldownCount >= cooldown)
            {
                AudioManager.instance.PlaySFX("Bow");
                GameObject arrow = Instantiate(this.arrow);
                arrow.GetComponent<Rigidbody2D>().velocity += Vector2.up * ArrowSpeedMultiplier;
                arrow.transform.eulerAngles = new Vector3(0, 0, 45);
                arrow.transform.position = transform.position + new Vector3(0, 1);
                _cooldownCount = 0;
            }
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (_cooldownCount >= cooldown)
            {
                AudioManager.instance.PlaySFX("Bow");
                GameObject arrow = Instantiate(this.arrow);
                arrow.GetComponent<Rigidbody2D>().velocity += Vector2.left * ArrowSpeedMultiplier;
                arrow.transform.eulerAngles = new Vector3(0, 0, 135);
                arrow.transform.position = transform.position + new Vector3(-.5f, .25f);
                _cooldownCount = 0;
            }
        }

        if (Input.GetKey(KeyCode.DownArrow))// Shoot Down
        {
            if (_cooldownCount >= cooldown)
            {
                AudioManager.instance.PlaySFX("Bow");
                GameObject arrow = Instantiate(this.arrow);
                arrow.GetComponent<Rigidbody2D>().velocity += Vector2.down * ArrowSpeedMultiplier;
                arrow.transform.eulerAngles = new Vector3(0, 0, -135);
                arrow.transform.position = transform.position + new Vector3(0, -1.5f);
                _cooldownCount = 0;
            }
        }
        
        if (Input.GetKey(KeyCode.RightArrow)) // Shoot Right
        {
            if (_cooldownCount >= cooldown)
            {
                AudioManager.instance.PlaySFX("Bow");
                GameObject arrow = Instantiate(this.arrow);
                arrow.GetComponent<Rigidbody2D>().velocity += Vector2.right * ArrowSpeedMultiplier;
                arrow.transform.eulerAngles = new Vector3(0, 0, -45);
                arrow.transform.position = transform.position + new Vector3(.5f, .3f);
                _cooldownCount = 0;
            }
        }
        _cooldownCount = _cooldownCount + (1 * Time.deltaTime);
    }
}
