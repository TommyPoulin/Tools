using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field)]
public class FindMeAttribute : Attribute
{
    public string someCustomData;
    public int moreSampleDate;

    public FindMeAttribute(string someCustomData, int moreSampleDate) {
        this.someCustomData = someCustomData;
        this.moreSampleDate = moreSampleDate;
    }
}
