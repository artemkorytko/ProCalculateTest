using System;
using System.Collections.Generic;
using UnityEngine;


public class CalculatorModel
{
    [Serializable]
    private class SaveData
    {
        public List<string> History = new List<string>();
    }


    private const string ERROR = "ERROR";
    private const string SAVE_KEY = "CalculatorState";
    private string currentInput;
    private SaveData saveData;

    public CalculatorModel()
    {
        LoadState();
    }

    public List<string> CalculationHistory => saveData.History;

    public void Calculate(string input, out bool isError)
    {
        string calculation = string.Empty;
        isError = false;
        if (string.IsNullOrEmpty(input))
        {
            calculation = $"{ERROR}";
            isError = true;
        }
        else if (!IsValidInput(input))
        {
            calculation = $"{input}={ERROR}";
            isError = true;
        }
        else
        {
            string[] parts = input.Split('+');
            if (parts.Length != 2)
            {
                isError = true;
            }

            if (!int.TryParse(parts[0].Trim(), out int num1))
                isError = true;
            if (!int.TryParse(parts[1].Trim(), out int num2))
                isError = true;

            if (!isError)
            {
                int result = num1 + num2;
                calculation = $"{input}={result}";
            }
            else
            {
                calculation = $"{input}={ERROR}";
            }
        }

        saveData.History.Insert(0, calculation);
        SaveState();
    }

    private bool IsValidInput(string input)
    {
        return input.Contains("+") && !input.Contains(".") && !input.Contains("-") && !input.Contains("*") && !input.Contains("/");
    }

    private void SaveState()
    {
        string state = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_KEY, state);
        PlayerPrefs.Save();
    }

    private void LoadState()
    {
        string state = PlayerPrefs.GetString(SAVE_KEY, string.Empty);
        if (!string.IsNullOrEmpty(state))
        {
            try
            {
                saveData = JsonUtility.FromJson<SaveData>(state);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                saveData = new SaveData();
            }
        }
        else
        {
            saveData = new SaveData();
        }
    }
}