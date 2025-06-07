namespace Ametrin.NBT.Tags;

public sealed class FloatTag(float value) : Tag
{
    public float Value { get; } = value;
    public override string ToString() => Value.ToString();
}
