namespace Ametrin.NBT.Tags;

public sealed class IntTag(string name, int value) : Tag(name)
{
    public int Value { get; set; } = value;
    public override string ToString() => Value.ToString();
}
