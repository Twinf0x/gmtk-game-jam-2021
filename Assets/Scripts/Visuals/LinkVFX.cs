using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkVFX : MonoBehaviour
{
    #region Inspector
    [SerializeField] private Transform linkStart;
    [SerializeField] private Transform linkTarget;
    #endregion

    public void Initialize(LinkableObject start, LinkableObject target)
    {
        linkStart.position = start.transform.position;
        linkTarget.position = target.transform.position;
        linkStart.parent = start.transform;
        linkTarget.parent = target.transform;
    }
}
