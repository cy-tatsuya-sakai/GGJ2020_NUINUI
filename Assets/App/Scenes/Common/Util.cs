using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// とりあえず突っ込む場所
/// </summary>
public static class Util
{
    /// <summary>
    /// マウス位置をカメラ座標に変更。Z値はゼロなので、いい感じに変えてね
    /// </summary>
    /// <param name="cam"></param>
    /// <returns></returns>
    public static Vector3 GetMouseWorldPosition(Camera cam)
    {
        Vector3 pos = Input.mousePosition;
        pos.z = -cam.transform.position.z;
        return cam.ScreenToWorldPoint(pos);
    }
}
