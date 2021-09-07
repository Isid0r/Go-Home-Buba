using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Levels
{
    public static Levels instance;
    private Levels() { }
    public static Levels GetInstance()
    {
        if (instance == null)
            instance = new Levels();
        return instance;
    }

    private static int levelsCount = 12; // Константа, отвечающая за количество уровней в игре

    private bool[] _levelAvailable = new bool[levelsCount];
    private int[] _cherriesTotal = new int[levelsCount];
    private int[] _cherriesCollected = new int[levelsCount];
    private bool[] _isMazePassed = new bool[levelsCount];
    private bool[] _isMazeExist = new bool[levelsCount];
    private bool[] _isLevelFullyCompleted = new bool[levelsCount];


    public void OpenLevel(int num)
    {
        _levelAvailable[num] = true;
    }

    private void CheckBonusLevelAvailable()
    {
        if (_levelAvailable[levelsCount-2])
        {
            for (int i = 0; i < levelsCount-1; i++)
            {
                if (!_isLevelFullyCompleted[i]) // если есть хоть один не пройденный полностью, бонус-уровень не доступен
                {

                    _levelAvailable[levelsCount-1] = false;
                    return;
                }
            }
            Debug.Log("Zdec");
            _levelAvailable[levelsCount-1] = true;
        }
        else
            _levelAvailable[levelsCount-1] = false;
    }

    public void ClearProgress() 
    {
        instance = new Levels();
    }

    public int GetCherriesTotal(int num)
    {
        return _cherriesTotal[num];
    }

    public int GetCherriesCollected(int num)
    {
        return _cherriesCollected[num];
    }

    public bool IsMazePassed(int num)
    {
        return _isMazePassed[num];
    }

    public void LevelPassed(int num, int сherriesCollected, int сherriesTotal, bool isMazeExist, bool isMazePassed)
    {
        if (num < levelsCount-1) _levelAvailable[num+1] = true; // если предпоследний, то последний не открывается
        _cherriesTotal[num] = сherriesTotal;
        _cherriesCollected[num] = сherriesCollected;
        _isMazeExist[num] = isMazeExist;
        _isMazePassed[num] = isMazePassed;

        IsLevelFullyCompleted(num);
        CheckBonusLevelAvailable();

    }

    private void IsLevelFullyCompleted(int num)
    {
        if (_cherriesCollected[num] == _cherriesTotal[num])
        {
            if (_isMazeExist[num] == true)
            {
                if (_isMazePassed[num] == true) _isLevelFullyCompleted[num] = true;
                else _isLevelFullyCompleted[num] = false;
            }
            else
            {
                _isLevelFullyCompleted[num] = true;
            }
        }
        else
        {
            _isLevelFullyCompleted[num] = false;
        }
    }
    public int GetLevelsCount()
    {
        return levelsCount;
    }

    public bool CheckLevelAvailable(int num) 
    {
        return _levelAvailable[num];
    }
}
