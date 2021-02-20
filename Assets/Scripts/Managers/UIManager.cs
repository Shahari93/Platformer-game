using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerCollision playerColl;
    [SerializeField] private Image keyImage;
    [SerializeField] private TMP_Text coinAmountTxt;

    private void Update()
    {
        if(playerColl.hasKey)
        {
            float alphaWithKey = 255f;
            Color alpha = keyImage.color;
            alpha.a = alphaWithKey;
            keyImage.color = alpha;
        }
        coinAmountTxt.text = playerColl.coinAmount.ToString();
    }
}
