using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class PlaneKinematicController : MonoBehaviour {

    public List<Transform> FallTrajectory = new List<Transform>();
    float timeToCrash;
    Tween fallTwn;

    public void StartFall(float _timeToFall)
    {
        Vector3[] fallPts = FallTrajectory.ConvertAll(p => p.position).ToArray();
        fallTwn = transform.DOPath(fallPts, _timeToFall, PathType.CatmullRom);
    }

    public void UpdateFallTime(float _newFallTime)
    {

    }
}
