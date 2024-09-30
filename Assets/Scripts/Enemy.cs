using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int scoreReward = 25;
    [SerializeField] int scorePenalty = 25;
    Bank bank;

    private void Start() 
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardOnDestroy()
    {
        if (!bank) { return; }
        bank.Deposit(scoreReward);
    }

    public void PenaltyOnEscape()
    {
        if (!bank) { return; }
        bank.Withdraw(scorePenalty);
    }
}
