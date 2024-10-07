using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TreeData
{
    public int level = 0;
    public int maxValue = 2;
    public int currValue = 0;

    public Rect area;

    public List<TreeData> nodeList;

    public void nextLevelNode()
    {
        if (nodeList != null)
            return;

        nodeList = new List<TreeData>();

        Rect[] iRectArray = Split();
        for (int i = 0; i < 4; ++i)
        {
            TreeData itreeData = new TreeData();
            itreeData.level = level + 1;
            itreeData.area = iRectArray[i];

            nodeList.Add(itreeData);
        }
    }

    //공간 분할 함수.
    Rect[] Split()
    {
        float subWidth = area.width * 0.5f;
        float subHeight = area.height * 0.5f;

        float x = area.x;
        float y = area.y;

        Rect[] iRectArray = new Rect[4];
        iRectArray[0] = new Rect(x - (subWidth * 0.5f), y + (subHeight * 0.5f), subWidth, subHeight);
        iRectArray[1] = new Rect(x + (subWidth * 0.5f), y + (subHeight * 0.5f), subWidth, subHeight);
        iRectArray[2] = new Rect(x + (subWidth * 0.5f), y - (subHeight * 0.5f), subWidth, subHeight);
        iRectArray[3] = new Rect(x - (subWidth * 0.5f), y - (subHeight * 0.5f), subWidth, subHeight);

        return iRectArray;
    }
}


public class QuadTree : MonoBehaviour
{
    public RectTransform quadMap;

    public List<GameObject> objectList;

    TreeData treeData;
    List<Color> colorList;
    void Start()
    {
        treeData = new TreeData();
        treeData.currValue = int.MaxValue;
        treeData.level = 0;
        treeData.area = new Rect(quadMap.localPosition.x, quadMap.localPosition.y, quadMap.rect.width, quadMap.rect.height);

        treeData.nextLevelNode();

        //gizmo
        colorList = new List<Color>();
        for (int i = 0; i < 100; ++i)
        {
            colorList.Add(Random.ColorHSV());
        }
    }

    // Update is called once per frame
    void Update()
    {
        NodeAreaCheck(treeData);
    }

    private void NodeAreaCheck(TreeData _treeData)
    {
        if (_treeData == null || _treeData.nodeList == null)
            return;

        foreach (var iNode in _treeData.nodeList)
        {
            if (AreaInObjectCount(iNode) > iNode.maxValue)
                iNode.nextLevelNode();
        }

        if (_treeData.currValue <= _treeData.maxValue)
            _treeData.nodeList = null;

    }

    private int AreaInObjectCount(TreeData _treeData)
    {
        _treeData.currValue = 0;

        int iCount = objectList.Count;
        for (int i = 0; i < iCount; ++i)
        {
            var iDis = Vector3.Distance(_treeData.area.position, objectList[i].transform.localPosition);

            if (iDis <= (_treeData.area.height * 0.5f) + (objectList[i].transform.localScale.x * 0.5f))
                _treeData.currValue++;

        }

        return _treeData.currValue;
    }

    private void OnDrawGizmos()
    {
        Drawing(treeData);
    }

    void Drawing(TreeData _treedata)
    {
        if (_treedata == null || _treedata.nodeList == null)
            return;

        foreach (var iNode in _treedata.nodeList)
        {
            ColorSelect(_treedata.level);
            DrawRect(iNode.area);

            Drawing(iNode);
        }
    }

    void DrawRect(Rect rect)
    {
        Gizmos.DrawWireCube(new Vector3(rect.x + 1920, rect.y + 1080, 0.01f), new Vector3(rect.size.x, rect.size.y, 0.01f));
    }

    void ColorSelect(int _level)
    {
        Gizmos.color = colorList[_level];
    }
}
