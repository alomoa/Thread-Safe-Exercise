public class User2
{
    private readonly int _id;
    public readonly Name _name;

    public User2(int id, Name name, string location)
    {
        _id = id;
        _name = name;
        Location = location;
    }

    public User2 UpdateDetails(string newName, string location)
    {
        return new User2(_id, _name, location);
    }

    public bool SearchForUser(string search)
    {
        // We have to make lower before we search so we search case insensitive
        search = search.ToLowerInvariant();
        _name.Last = _name.Last.ToLowerInvariant();
        _name.First = _name.First.ToLowerInvariant();
        return _name.Search(search);
    }

    public string Location { get;}
}

// You cannot change this class: It is in an external library just provided you the code so you can see it.
public class Name
{
    public Name(Name name)
    {
        First = name.First;
        Initials = name.Initials;
        Last = name.Last;
    }

    public Name(string name)
    {
        var split = name.Split(" ");
        First = split[0];
        Initials = split[1];
        Last = split[2];
    }

    public string First { get; set; }
    public string Initials { get; set; }
    public string Last { get; set; }

    public bool Search(string search)
    {
        return First.Contains(search) || Last.Contains(search);
    }

    public override string ToString() => $"{First} {Initials} {Last}";
}