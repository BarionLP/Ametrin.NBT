namespace Ametrin.NBT.Tags;

public sealed class DoubleTag(double value) : Tag
{
    public double Value { get; } = value;
    public override string ToString() => Value.ToString();
}
