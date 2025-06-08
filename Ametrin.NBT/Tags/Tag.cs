namespace Ametrin.NBT.Tags;

public abstract class Tag(string name)
{
    public string Name { get; } = name;
}
