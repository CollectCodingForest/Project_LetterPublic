using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayData
{
    // 플레이어가 하루동안 한 행동을 저장.
    
    public int Gold  // 현재 돈
    {
        get => GameManager.Instance.currentSaveData.gold; 
        set
        {
            if(value > GameManager.Instance.currentSaveData.gold)
            {
                int difference = value - GameManager.Instance.currentSaveData.gold;
                GameManager.Instance.currentSaveData.totalEarnGold += difference;
                GameManager.Instance.currentSaveData.gold = value;
            }
            else if(value < GameManager.Instance.currentSaveData.gold)
            {
                GameManager.Instance.currentSaveData.gold = value;
            }
        } 
    }
    private int customer;    // 하루에 맞이한 신청자 수
    public int Customer 
    { 
        get { return customer; }
        set { customer = value;
            GameManager.Instance.currentSaveData.totalCustomer = value; } }
    private int allow;      // 하루에 허가한 신청자 수
    public int Allow
    {
        get { return allow; }
        set
        {
            allow = value;
            GameManager.Instance.currentSaveData.totalAllow = value;
        }
    }
    private int refuse;     // 하루에 거부한 신청자 수
    public int Refuse
    {
        get { return refuse; }
        set
        {
            refuse = value;
            GameManager.Instance.currentSaveData.totalRefuse = value;
        }
    }
    private int success;    // 성공 횟수
    public int Success
    {
        get { return success; }
        set
        {
            success = value;
            GameManager.Instance.currentSaveData.totalSuccess = value;
        }
    }
    private int failed;     // 실패 횟수
    public int Failed
    {
        get { return failed; }
        set
        {
            failed = value;
            GameManager.Instance.currentSaveData.totalFailed = value;
        }
    }
    private int fineCount;  // 벌금 받은 횟수
    public int FineCount
     {
        get { return fineCount; }
        set
        {
            fineCount = value;
            GameManager.Instance.currentSaveData.totalFineCount = value;
        }
    }
    private int fine;   // 벌금 낸 총액
    public int Fine
    {
        get { return fine; }
        set
        {
            fine = value;
            GameManager.Instance.currentSaveData.totalFine = value;
        }
    }

    public void ResetToday()
    {
        customer = 0;
        allow = 0;
        refuse = 0;
        success = 0;
        failed = 0;
        fineCount = 0;
        fine = 0;
    }

    
}

