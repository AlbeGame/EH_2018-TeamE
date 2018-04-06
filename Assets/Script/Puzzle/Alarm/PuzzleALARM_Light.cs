﻿using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer))]
public class PuzzleALARM_Light : MonoBehaviour
{
    MeshRenderer mRenderer;
    List<Material> materialPattern = new List<Material>();
    int patternIndex = 0;

    public Material OFF_mat;
    public Material Yellow_mat;
    public Material Red_mat;

    public void Init(List<PuzzleALARM.LightsValue> _pattern)
    {
        materialPattern = new List<Material>(_pattern.Count);
        for (int i = 0; i < _pattern.Count; i++)
        {
            switch (_pattern[i])
            {
                case PuzzleALARM.LightsValue.OFF:
                    materialPattern[i] = OFF_mat;
                    break;
                case PuzzleALARM.LightsValue.RED:
                    materialPattern[i] = Red_mat;
                    break;
                case PuzzleALARM.LightsValue.YELLOW:
                    materialPattern[i] = Yellow_mat;
                    break;
                default:
                    break;
            }
        }

        patternIndex = 0;
    }

	public void Pulse()
    {
        SetMat(materialPattern[patternIndex]);
        patternIndex++;
    }

    public void TurnOff()
    {
        SetMat(OFF_mat);
        patternIndex = 0;
    }

    private void Start()
    {
        mRenderer = GetComponent<MeshRenderer>();
    }

    void SetMat(Material _mat)
    {
        mRenderer.materials[1] = _mat;
    }
}