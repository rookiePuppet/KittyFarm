using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CounterController : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button reduceButton;
    [SerializeField] private Button increaseButton;

   public int Value { get; private set; }

   private void Awake()
   {
       reduceButton.onClick.AddListener(ReduceValue);
       increaseButton.onClick.AddListener(IncreaseValue);

       inputField.onEndEdit.AddListener(OnInputFieldEndEdit);
   }

   private void OnInputFieldEndEdit(string newValue)
   {
       if (!int.TryParse(newValue, out var intValue))
       {
           intValue = 0;
       }

       if (intValue < 0)
       {
           return;
       }
       
       Value = intValue;
   }

   private void IncreaseValue()
   {
       Value += 1;
       inputField.text = Value.ToString();
   }

   private void ReduceValue()
   {
       Value -= 1;
       if (Value < 0)
       {
           Value = 0;
       }
       
       inputField.text = Value.ToString();
   }
}
