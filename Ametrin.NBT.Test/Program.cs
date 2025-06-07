using Ametrin.NBT;

using var reader = NbtReader.CreateFromFile(@"C:\Users\Barion\AppData\Roaming\.curseforge\Instances\1.21.4 Test\saves\New World\level.dat");

var tag = reader.ReadTag();
Console.WriteLine(tag);