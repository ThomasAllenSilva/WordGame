using UnityEngine;
using TMPro;
using UnityEngine.Localization;

public class InterestingFact : MonoBehaviour
{
    [SerializeField] private LocalizedStringTable interestingFactsTable;

    private TextMeshProUGUI interestingFactText;

    private void Awake() => interestingFactText = GetComponent<TextMeshProUGUI>();

    private void Start()
    {
        int randomInterestingFact = Random.Range(1, interestingFactsTable.GetTable().Count);
        interestingFactText.text = GetFunFactString(randomInterestingFact);
    }

    private string GetFunFactString(int funFactIndex)
    {
        string entryKey = "InterestingFact" + funFactIndex + "Text";
        return interestingFactsTable.GetTable().GetEntry(entryKey).GetLocalizedString();
    }
}
