using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoSingleton<UiManager>
{
    [field: SerializeField] public PlayerStatusUi playerStatusUi { get; private set; }
    [field: SerializeField] public StatusUi enemyStatusUi { get; private set; }
}
