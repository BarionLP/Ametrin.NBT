using Ametrin.NBT.Tags;

namespace Ametrin.NBT.Explorer;

internal class TagViewModel(string name, Tag tag)
{
    public string Name { get; } = name;
    public Tag Tag { get; } = tag;
}
