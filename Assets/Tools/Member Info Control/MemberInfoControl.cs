using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberInfoControl : MonoBehaviour
{
    [SerializeField] MemberInfoButton memberInfoButton;
    [SerializeField] Transform container;
    [SerializeField] List<MemberInfo> members;

    // Start is called before the first frame update
    void Start()
    {
        if(members.Count > 0)
        {
            foreach (var item in members)
            {
                memberInfoButton.memberInfo = item;
                MemberInfoButton mib = Instantiate(memberInfoButton, container) as MemberInfoButton;
            }
        }
    }
}

[System.Serializable]
public struct MemberInfo
{
    public string memberName;
    public string memberRole;
    public string memberPortfolioURL;
}
