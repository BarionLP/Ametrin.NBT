namespace Ametrin.NBT.Tags;

public sealed class DoubleTag(string name, double value) : Tag(name, TagType.Double)
{
    public double Value { get; set; } = value;
    public override string ToString() => Value.ToString();
}
