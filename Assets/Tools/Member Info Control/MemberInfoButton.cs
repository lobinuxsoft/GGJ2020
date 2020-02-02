using TMPro;
using UnityEngine;

public class MemberInfoButton : MonoBehaviour
{
    public MemberInfo memberInfo;

    [SerializeField] TextMeshProUGUI memberNameText;
    [SerializeField] TextMeshProUGUI memberRoleText;
    [SerializeField] UnityEngine.UI.Button portfolioUrlButton;

    // Start is called before the first frame update
    void Start()
    {
        memberNameText.text = memberInfo.memberName;
        memberRoleText.text = memberInfo.memberRole;
        portfolioUrlButton.onClick.AddListener(OpenPorfolioURL);
    }

    public void OpenPorfolioURL()
    {
        Application.OpenURL(memberInfo.memberPortfolioURL);
    }
}
