# Thread-Safe-Exercise

### 1a
The bit is thread-safe because it locks the operations that access and change the value of the state, m_balance, so only one thread can change the value at a time.

### 1b
**Couldn't figure it out so I looked at the answer**

### 2
`public class User1
{
    protected readonly int _id = 0;
    public readonly string _name;
    public int Designation { get; }
    public string Location { get;}

    public User1(int id, string name, int designation, string location)
    {
        _id = id;
        _name = name;
        Designation = designation;
        Location = location;
        
    }

    public string GetUserDetails(int uid, string userName)
    {
        return $"{_id} - {uid} - {userName} - {_name}";
    }

    public User1 UpdateDetails(string newName, string location)
    {
        return new User1(_id, newName, Designation, location);
    } 
}`
- Made everything readonly
- If something has to mutate, then it returns a new instance with the new values.


### 3
`
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
        Name copy = new Name(_name);
        copy.Last = copy.Last.ToLowerInvariant();
        copy.First = copy.First.ToLowerInvariant(); 
        return copy.Search(search);
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
}`
- Did the same as question 2
- For the search function, created a copy of name with the lower case values and used that to search

### 4 

Since the original isn't being locked, other threads can still use it. If a loc is randomly selected from a list, it's likely that the same loc may be selected more than once
Solution: lock the original loc, do we even need to lock the copy?

### 5 
Deadlock. Both lock blocks are waiting for the other to release their locks. 

### 6 
_phonebook.Clear() doesn't lock the phonebook. So a thread that clears the phonebook can happen as another thread is adding a number.  
Solution: lock the phonebook when clearing
