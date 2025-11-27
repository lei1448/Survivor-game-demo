using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public void UpdataLevel(int level)
    {
         levelText.text = $"Lv.{level.ToString()}";   
    }
}
