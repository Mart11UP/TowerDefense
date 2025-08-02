using UnityEngine;
using Tower.Defense;
using Tower.Data;
using UnityEngine.UI;
using Tower.Economy;

namespace Tower.UI
{
    public class DefendersUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject defendersButtons;
        [SerializeField] private Text moneyAmountText;
        [Header("Default Button Color Block")]
        [Space]
        [SerializeField] private ColorBlock defaultButtonColorBlock = ColorBlock.defaultColorBlock;
        [Header("Selected Button Color Block")]
        [Space]
        [SerializeField] private ColorBlock selectedButtonColorBlock = ColorBlock.defaultColorBlock;
        private DefendersManager defendersManager;
        private EconomyManager economyManager;

        private void Awake()
        {
            DefendersManager.OnDefendersDataUpdated += UpdateUI;
            DefendersManager.OnCurrentDefenderChanged += SetButtonSelected;
            DefendersManager.OnDefenderSelectionRejected += DeselectButtons;
            DefendersManager.OnCanAffordChanged += EnableButton;
            defendersManager = FindAnyObjectByType<DefendersManager>();
            economyManager = FindAnyObjectByType<EconomyManager>();
            economyManager.OnMoneyAmountChanged += UpdateMoneyAmount;
        }

        private void OnDisable()
        {
            DefendersManager.OnDefenderSelectionRejected -= DeselectButtons;
            DefendersManager.OnDefendersDataUpdated -= UpdateUI;
            DefendersManager.OnCanAffordChanged -= EnableButton;
            economyManager.OnMoneyAmountChanged -= UpdateMoneyAmount;
        }

        private void UpdateUI(DefenderData[] defendersData)
        {
            Button[] buttons = defendersButtons.GetComponentsInChildren<Button>();
            for (int i = 0; i < defendersData.Length; i++)
            {
                DefenderData defenderData = defendersData[i];
                Button button = buttons[i];
                Text textName = button.transform.Find(nameof(defenderData.Name)).GetComponent<Text>();
                Text textCost = button.transform.Find(nameof(defenderData.Cost)).GetComponent<Text>();

                textName.text = defenderData.Name;
                textCost.text = defenderData.Cost.ToString();
                button.gameObject.name = defenderData.Name;
                buttons[i].gameObject.SetActive(true);
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => defendersManager.DefenderSelectionRequest(defenderData));
            }
            for (int i = defendersData.Length; i < buttons.Length; i++)
                buttons[i].gameObject.SetActive(false);
        }

        private void SetButtonSelected(DefenderData defenderData)
        {
            foreach (Button button in defendersButtons.GetComponentsInChildren<Button>())
            {
                if (button.name == defenderData.Name) button.colors = selectedButtonColorBlock;
                else button.colors = defaultButtonColorBlock;
            }
        }

        private void DeselectButtons()
        {
            foreach (Button button in defendersButtons.GetComponentsInChildren<Button>())
                button.colors = defaultButtonColorBlock;
        }

        private void EnableButton(DefenderData defenderData, bool enabled)
        {
            Button button = defendersButtons.transform.Find(defenderData.Name).GetComponent<Button>();
            if (button == null) return;

            button.interactable = enabled;
        }

        private void UpdateMoneyAmount(int lastAmount, int currentAmount)
        {
            moneyAmountText.text = currentAmount.ToString();
        }
    }
}
