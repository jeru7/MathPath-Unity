using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    /// <summary>
    /// Open or closes the received panel based on the received boolean.
    /// </summary>
    /// <param name="panel">References to the panel.</param>
    /// <param name="isActive">Boolean value to toggle the panel visibility.</param>
    protected void SetPanelActive(GameObject panel, bool isActive)
    {
        panel.SetActive(isActive);
    }
}
