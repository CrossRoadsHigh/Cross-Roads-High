﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBehaviour : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);   
    }
}