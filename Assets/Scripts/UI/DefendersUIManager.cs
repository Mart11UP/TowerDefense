using UnityEngine;
using Tower.Defense;
using Tower.Data;
using UnityEngine.UI;

namespace Tower.UI
{
    public class DefendersUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject defendersButtons;
        [SerializeField] private ColorBlock selectedButtonColorBlock = ColorBlock.defaultColorBlock;
        DefendersManager defendersManager;

        private void Awake()
        {
            DefendersManager.OnDefendersDataUpdated += UpdateUI;
            DefendersManager.OnCurrentDefenderChanged += SetButtonSelected;
            defendersManager = FindAnyObjectByType<DefendersManager>();
        }

        private void OnDisable()
        {
            DefendersManager.OnDefendersDataUpdated -= UpdateUI;
        }

        private void UpdateUI(DefenderData[] defendersData)
        {
            Button[] buttons = defendersButtons.GetComponentsInChildren<Button>();
            for (int i = 0; i < defendersData.Length; i++)
            {
                DefenderData defenderData = defendersData[i];
                Button button = buttons[i];
                Text text = button.GetComponentInChildren<Text>();

                button.gameObject.name = defenderData.Name;
                text.text = defenderData.Name;
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
                else button.colors = ColorBlock.defaultColorBlock;
            }
        }
    }
}
