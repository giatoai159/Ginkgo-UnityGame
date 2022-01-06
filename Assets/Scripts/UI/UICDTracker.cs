using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICDTracker : MonoBehaviour
{
    public static UICDTracker instance;
    Image dashImage;
    Text dashText;
    bool isDashCooldown = false;
    GameObject dashTracker;
    [SerializeField] GameObject dashCD;
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.instance.canDash)
        {
            dashTracker = Instantiate(dashCD, transform.position, Quaternion.identity);
            dashTracker.transform.SetParent(transform);
            dashImage = dashTracker.transform.Find("Image").Find("CD").GetComponent<Image>();
            dashText = dashTracker.transform.Find("Number").GetComponent<Text>();
        }

    }

    // Update is called once per frame
    void Update()
    {

        UpdateDashCD();
    }

    public void UpdateDashCD()
    {
        if (PlayerController.instance.getDashCDTimer > 0 && !isDashCooldown)
        {
            isDashCooldown = true;
            dashImage.fillAmount = 1;
            dashText.gameObject.SetActive(true);
            dashText.text = PlayerController.instance.dashCooldown.ToString();
        }
        else
        {
            dashImage.fillAmount = (PlayerController.instance.getDashCDTimer / PlayerController.instance.dashCooldown);
            dashText.text = Mathf.CeilToInt(PlayerController.instance.getDashCDTimer).ToString();
            if (dashImage.fillAmount <= 0)
            {
                dashImage.fillAmount = 0;
                dashText.gameObject.SetActive(false);
                isDashCooldown = false;
            }
        }
    }
}
