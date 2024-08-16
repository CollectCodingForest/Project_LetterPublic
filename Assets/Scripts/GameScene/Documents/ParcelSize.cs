using System;

[Serializable]
public struct ParcelSize // 택배 사이즈
{
    public float Width;
    public float Height;


    public ParcelSize(float width, float height)
    {
        Width = width;
        Height = height;
    }
}