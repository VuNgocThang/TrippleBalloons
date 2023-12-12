using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarHandler : MonoBehaviour
{
    public int star;
    public TextMeshProUGUI txtStar;

    private void OnGUI()
    {
        star = DataUseInGame.gameData.star;
        txtStar.text = star.ToString();
    }
}
