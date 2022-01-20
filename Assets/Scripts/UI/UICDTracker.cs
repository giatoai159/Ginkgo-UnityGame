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

    Image icicleImage;
    Text icicleText;
    bool isIcicleCooldown = false;
    GameObject icicleTracker;
    [SerializeField] GameObject icicleCD;

    Image shieldImage;
    Text shieldText;
    bool isShieldCooldown = false;
    GameObject shieldTracker;
    [SerializeField] GameObject shieldCD;
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
            dashImage.fillAmount = 0;
        }
        if (PlayerAbilityController.instance.canShoot)
        {
            icicleTracker = Instantiate(icicleCD, transform.position, Quaternion.identity);
            icicleTracker.transform.SetParent(transform);
            icicleImage = icicleTracker.transform.Find("Image").Find("CD").GetComponent<Image>();
            icicleText = icicleTracker.transform.Find("Number").GetComponent<Text>();
            icicleImage.fillAmount = 0;
        }

        if (PlayerAbilityController.instance.canShield)
        {
            shieldTracker = Instantiate(shieldCD, transform.position, Quaternion.identity);
            shieldTracker.transform.SetParent(transform);
            shieldImage = shieldTracker.transform.Find("Image").Find("CD").GetComponent<Image>();
            shieldText = shieldTracker.transform.Find("Number").GetComponent<Text>();
            shieldImage.fillAmount = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.canDash)
        {
            if (!dashTracker)
            {
                dashTracker = Instantiate(dashCD, transform.position, Quaternion.identity);
                dashTracker.transform.SetParent(transform);
                dashImage = dashTracker.transform.Find("Image").Find("CD").GetComponent<Image>();
                dashText = dashTracker.transform.Find("Number").GetComponent<Text>();
                dashImage.fillAmount = 0;
            }
            UpdateDashCD();
        }
        else { Destroy(dashTracker); }

        if (PlayerAbilityController.instance.canShoot)
        {
            if (!icicleTracker)
            {
                icicleTracker = Instantiate(icicleCD, transform.position, Quaternion.identity);
                icicleTracker.transform.SetParent(transform);
                icicleImage = icicleTracker.transform.Find("Image").Find("CD").GetComponent<Image>();
                icicleText = icicleTracker.transform.Find("Number").GetComponent<Text>();
                icicleImage.fillAmount = 0;
            }
            UpdateIcicleCD();
        }
        else { Destroy(icicleTracker); }

        if (PlayerAbilityController.instance.canShield)
        {
            if (!shieldTracker)
            {
                shieldTracker = Instantiate(shieldCD, transform.position, Quaternion.identity);
                shieldTracker.transform.SetParent(transform);
                shieldImage = shieldTracker.transform.Find("Image").Find("CD").GetComponent<Image>();
                shieldText = shieldTracker.transform.Find("Number").GetComponent<Text>();
                shieldImage.fillAmount = 0;
            }
            UpdateShieldCD();
        }
        else { Destroy(shieldTracker); }
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
        else if (isDashCooldown)
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

    public void UpdateIcicleCD()
    {
        if (PlayerAbilityController.instance.getShootingTimer > 0 && !isIcicleCooldown)
        {
            isIcicleCooldown = true;
            icicleImage.fillAmount = 1;
            icicleText.gameObject.SetActive(true);
            icicleText.text = PlayerAbilityController.instance.shootingCD.ToString();
        }
        else if (isIcicleCooldown)
        {
            icicleImage.fillAmount = (PlayerAbilityController.instance.getShootingTimer / PlayerAbilityController.instance.shootingCD);
            icicleText.text = Mathf.CeilToInt(PlayerAbilityController.instance.getShootingTimer).ToString();
            if (icicleImage.fillAmount <= 0)
            {
                icicleImage.fillAmount = 0;
                icicleText.gameObject.SetActive(false);
                isIcicleCooldown = false;
            }
        }
    }

    public void UpdateShieldCD()
    {
        if (PlayerAbilityController.instance.getShieldTimer > 0 && !isShieldCooldown)
        {
            isShieldCooldown = true;
            shieldImage.fillAmount = 1;
            shieldText.gameObject.SetActive(true);
            shieldText.text = PlayerAbilityController.instance.shieldCD.ToString();
        }
        else if (isShieldCooldown)
        {
            shieldImage.fillAmount = (PlayerAbilityController.instance.getShieldTimer / PlayerAbilityController.instance.shieldCD);
            shieldText.text = Mathf.CeilToInt(PlayerAbilityController.instance.getShieldTimer).ToString();
            if (shieldImage.fillAmount <= 0)
            {
                shieldImage.fillAmount = 0;
                shieldText.gameObject.SetActive(false);
                isShieldCooldown = false;
            }
        }
    }
}
