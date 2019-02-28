
public class Page
{
    public string type { get; set; }
    public string title { get; set; }
    public Ancestor[] ancestors { get; set; }
    public Space space { get; set; }
    public Body body { get; set; }
}

public class Space
{
    public string key { get; set; }
}

public class Body
{
    public Storage storage { get; set; }
}

public class Storage
{
    public string value { get; set; }
    public string representation { get; set; }
}

public class Ancestor
{
    public int id { get; set; }
}
