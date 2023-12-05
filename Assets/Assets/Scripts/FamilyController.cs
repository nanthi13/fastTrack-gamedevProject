using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FamilyController : MonoBehaviour
{
    [Header("Event")]
    public TextMeshProUGUI eventDescText;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI balanceText;

    [Header("Summary")]
    public TextMeshProUGUI incomeText;
    public TextMeshProUGUI penaltiesText;
    public TextMeshProUGUI totalIncomeText;

    [Header("Expenses")]
    public TextMeshProUGUI newBalanceText;
    public TextMeshProUGUI rentText;
    public TextMeshProUGUI electricityText;
    public TextMeshProUGUI totalText;

    [Header("Medicine")]
    public GameObject medicineDisplay;
    public TextMeshProUGUI medicineText;

    [Header("Family")]
    public GameObject momButton;
    public TextMeshProUGUI momText;
    public GameObject wifeButton;
    public TextMeshProUGUI wifeText;
    public GameObject sonButton;
    public TextMeshProUGUI sonText;

    public static FamilyMember mom = new("Mom", Status.Ok);
    public static FamilyMember wife = new("Wife", Status.Ok);
    public static FamilyMember son = new("Son", Status.Ok);

    public static int rentCost = 25;
    public static int electricityCost = 5;
    public static int medicineCost = 0;
    public static int medicinePrice = 10;

    public static int incomePerPass = 15;
    public static int penaltyPerIllegalPass = 25;

    private int increaseDiffEveryXDays = 4;

    public static event Action OnPlayerGameOver;

    public void Start()
    {
        medicineCost = 0;
        momButton.GetComponent<Button>().onClick.AddListener(() => BuyMedicine(mom));
        wifeButton.GetComponent<Button>().onClick.AddListener(() => BuyMedicine(wife));
        sonButton.GetComponent<Button>().onClick.AddListener(() => BuyMedicine(son));
        UpdateDisplay();
        UpdateFamilyDisplay();
        // Events based on day
        SetEventText("");
    }

    private void BuyMedicine(FamilyMember member)
    {
        member.GiveMedicine();
        medicineCost += medicinePrice;
        UpdateFamilyDisplay();
        UpdateDisplay();
    }

    private void UpdateFamilyDisplay()
    {
        momText.text = mom.GetDisplayText();
        wifeText.text = wife.GetDisplayText();
        sonText.text = son.GetDisplayText();
        if (mom.status == Status.Sick)
        {
            momButton.SetActive(true);
            if ((FinanceController.totalMoney + GetTotalIncome() - GetTotalExpenses()) < 10)
            {
                momButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                momButton.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            momButton.SetActive(false);
        }
        if (wife.status == Status.Sick)
        {
            wifeButton.SetActive(true);
            if ((FinanceController.totalMoney + GetTotalIncome() - GetTotalExpenses()) < 10)
            {
                wifeButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                wifeButton.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            wifeButton.SetActive(false);
        }
        if (son.status == Status.Sick)
        {
            sonButton.SetActive(true);
            if ((FinanceController.totalMoney + GetTotalIncome() - GetTotalExpenses()) < 10)
            {
                wifeButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                wifeButton.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            sonButton.SetActive(false);
        }
    }

    private void UpdateDisplay()
    {
        // Update display
        dayText.text = "Month " + DayController.day.ToString();
        balanceText.text = "$" + FinanceController.totalMoney.ToString();
        incomeText.text = FinanceController.numberPassed.ToString() + " * $" + incomePerPass.ToString() + " = $" + GetIncome().ToString();
        penaltiesText.text = FinanceController.numberFailed.ToString() + " * $" + penaltyPerIllegalPass.ToString() + " = $" + GetPenalties().ToString();
        totalIncomeText.text = "$" + GetTotalIncome().ToString();
        if (GetTotalIncome() >= 0)
        {
            totalIncomeText.faceColor = new Color32(41, 185, 44, 255);
        }
        else
        {
            totalIncomeText.faceColor = new Color32(255, 16, 48, 255);
        }

        if (FinanceController.totalMoney >= 0)
        {
            balanceText.faceColor = new Color32(41, 185, 44, 255);
        }
        else
        {
            balanceText.faceColor = new Color32(255, 16, 48, 255);
        }

        // increase the difficulty every x days
        if ((DayController.day % increaseDiffEveryXDays) == 0 && DayController.day != 0)
        {
            rentCost += 5;
            electricityCost += 1;
            SetEventText("Living Costs increased!");
        }

        newBalanceText.text = "$" + (FinanceController.totalMoney + GetTotalIncome() - GetTotalExpenses()).ToString();
        if ((FinanceController.totalMoney + GetTotalIncome() - GetTotalExpenses()) >= 0)
        {
            newBalanceText.faceColor = new Color32(41, 185, 44, 255);
        }
        else
        {
            newBalanceText.faceColor = new Color32(255, 16, 48, 255);
        }

        // Monthly costs
        rentText.text = "$" + rentCost.ToString();
        electricityText.text = "$" + electricityCost.ToString();

        // Total
        totalText.text = "$" + GetTotalExpenses().ToString();

        // Medicine
        if (medicineCost > 0)
        {
            medicineDisplay.SetActive(true);
            medicineText.text = medicineCost.ToString();
        }
        else
        {
            medicineDisplay.SetActive(false);
        }
    }

    private int GetIncome()
    {
        return (FinanceController.numberPassed * incomePerPass);
    }

    private int GetPenalties()
    {
        return (FinanceController.numberFailed * penaltyPerIllegalPass);
    }

    private int GetTotalIncome()
    {
        return (GetIncome() - GetPenalties());
    }

    private int GetTotalExpenses()
    {
        int total = 0;
        total += rentCost;
        total += electricityCost;
        total += medicineCost;
        return total;
    }

    private void SetEventText(string eventDesc)
    {
        eventDescText.text = eventDesc;
    }

    public void NextDay()
    {
        int cost = GetTotalExpenses();
        int income = GetTotalIncome();
        mom.StatusProgress();
        wife.StatusProgress();
        son.StatusProgress();

        FinanceController.totalMoney = FinanceController.totalMoney + income - cost;
    }
}

[System.Serializable]
public enum Status
{
    Ok,
    Sick,
    Dead,
}

public class FamilyMember
{
    public string Name;
    public Status status;

    public FamilyMember(string name, Status status)
    {
        this.Name = name;
        this.status = status;
    }

    public string GetDisplayText()
    {
        return Name + " (" + status + ")";
    }

    public void StatusProgress()
    {
        float luck = UnityEngine.Random.Range(0, 10);
        if (status == Status.Ok && luck <= 2.5)
        {
            Infect();
        }
        else if (status == Status.Sick && luck >= 6.5)
        {
            Kill();
        }
    }

    public void UpdateStatus(Status newStatus)
    {
        this.status = newStatus;
    }

    public void GiveMedicine()
    {
        UpdateStatus(Status.Ok);
    }

    public void Infect()
    {
        UpdateStatus(Status.Sick);
    }

    public void Kill()
    {
        UpdateStatus(Status.Dead);
    }
}