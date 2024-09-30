using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    Bank bank;
    int balanceCache;

    private void Awake()
    {
        bank = FindObjectOfType<Bank>();
    }

    // Update is called once per frame
    void Update()
    {
        if (balanceCache != bank.CurrentBalance) 
        {
            balanceCache = bank.CurrentBalance;
            scoreText.text = "Score: " + bank.CurrentBalance;
        }
    }
}
