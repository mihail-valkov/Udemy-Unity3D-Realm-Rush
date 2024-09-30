using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;

    [SerializeField] int currentBalance;

    public int CurrentBalance { get { return currentBalance; } }

    private void Awake()
    {
        currentBalance = startingBalance;
    }

    public void Deposit(int amount)
    {
        if (amount < 0)
        {
            Debug.LogError("Deposit amount must be positive");
            return;
        }
        currentBalance += amount;
    }

    public void Withdraw(int amount)
    {
        if (amount < 0)
        {
            Debug.LogError("Withdrawal amount must be positive");
            return;
        }

        currentBalance -= amount;
    }
}
