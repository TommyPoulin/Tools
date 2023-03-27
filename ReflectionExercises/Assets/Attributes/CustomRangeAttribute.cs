using System;

public class CustomRangeAttribute : Attribute
{
    int minimum;
    int maximum;

    public CustomRangeAttribute(int minimum, int maximum) {
        this.minimum = minimum;
        this.maximum = maximum;
    }
}
