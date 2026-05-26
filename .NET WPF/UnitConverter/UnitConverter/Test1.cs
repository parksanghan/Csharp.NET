using System;
using System.Reflection;

class Child
{
    public int Value { get; set; }
}

class Sample
{
    public string Name { get; set; }
    public Child RefChild { get; set; }
}

class Test1
{
    public static void Start()
    {
        var source = new Sample
        {
            Name = "Source",
            RefChild = new Child { Value = 10 }
        };

        var target = new Sample
        {
            Name = "Target",
            RefChild = new Child { Value = 20 }
        };

        CopyDataMembers(source, target);

        Console.WriteLine("== 복사 직후 ==");
        Print(source, target);

        source.Name = "Changed Source";
        source.RefChild.Value = 99;

        Console.WriteLine("\n== source 내부 값 변경 후 ==");
        Print(source, target);

        target.Name = "Changed Target";
        target.RefChild.Value = 777;

        Console.WriteLine("\n== target 내부 값 변경 후 ==");
        Print(source, target);
    }

    static void CopyDataMembers(object sourceEntity, object targetEntity)
    {
        var props = sourceEntity.GetType().GetProperties();

        foreach (PropertyInfo prop in props)
        {
            object originalValue = prop.GetValue(targetEntity, null);
            object newValue = prop.GetValue(sourceEntity, null);

            if ((newValue == null && originalValue != null) ||
                (newValue != null && !newValue.Equals(originalValue)))
            {
                prop.SetValue(targetEntity, prop.GetValue(sourceEntity, null), null);
            }
        }
    }

    static void Print(Sample source, Sample target)
    {
        Console.WriteLine($"source.Name = {source.Name}");
        Console.WriteLine($"target.Name = {target.Name}");
        Console.WriteLine($"source.RefChild.Value = {source.RefChild.Value}");
        Console.WriteLine($"target.RefChild.Value = {target.RefChild.Value}");
        Console.WriteLine($"RefChild same reference = {ReferenceEquals(source.RefChild, target.RefChild)}");
    }
}