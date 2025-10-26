using TMPro;
using UnityEngine;


namespace ProCalculate.Calculator
{
    public class UIHistoryEntry : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;

        public void Setup(string text)
        {
            if (_label)
                _label.text = text;
        }
    }
}