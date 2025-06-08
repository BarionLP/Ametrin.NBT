namespace Ametrin.NBT.Tags;

public sealed class FloatTag(string name, float value) : Tag(name)
{
    public float Value { get; set; } = value;
    public override string ToString() => Value.ToString();
}
