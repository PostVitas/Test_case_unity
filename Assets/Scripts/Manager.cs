using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [ContextMenu("Load")]
    private void LoadGame() {
        new Factory().Load();
    }

    void Start() {
        new Factory().Load();
    }
}
