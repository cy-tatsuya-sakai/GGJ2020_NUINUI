using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スコアを保持しておくなにか
/// </summary>
public static class MainGameScore
{
    public static int good = 0;
    public static int nice = 0;
    public static int excellent = 0;

    // ハイスコアの概念は存在しないらしい
    // public static int hiScoreGood = 0;
    // public static int hiScoreNice = 0;
    // public static int hiScoreExcellent = 0;

    /// <summary>
    /// スコアをリセット
    /// </summary>
    public static void ResetScore()
    {
        good = 0;
        nice = 0;
        excellent = 0;
    }

    /// <summary>
    /// ハイスコア更新
    /// </summary>
    // public static void UpdateHiScore()
    // {
    //     if(good > hiScoreGood) { hiScoreGood = good; }
    //     if(nice > hiScoreNice) { hiScoreNice = nice; }
    //     if(excellent > hiScoreExcellent) { hiScoreExcellent = excellent; }
    // }
}
