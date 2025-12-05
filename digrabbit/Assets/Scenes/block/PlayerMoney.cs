using UnityEngine;
using TMPro;   // TextMeshPro를 쓴다면

public class PlayerMoney : MonoBehaviour
{
    public static PlayerMoney Instance;

    [Header("Money Data")]
    public int currentMoney = 0;     // 현재 보유 금액

    [Header("UI")]
    public TMP_Text moneyText;       // 화면에 표시할 텍스트

    void Awake()
    {
        // 간단 싱글톤
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateMoneyUI();
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        UpdateMoneyUI();
    }

    public bool TrySpendMoney(int amount)
    {
        if (currentMoney < amount)
            return false;

        currentMoney -= amount;
        UpdateMoneyUI();
        return true;
    }

    void UpdateMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = currentMoney.ToString();
    }
}
