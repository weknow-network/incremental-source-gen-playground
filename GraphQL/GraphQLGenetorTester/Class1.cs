
public class Class1
{
    public int Method1() => 0;
}

// file2.cs
[MarkerAttibute("todo")]
public class Class2
{
    public Class1 Method2() => null;
}

// file3.cs
public class Class3 { }
