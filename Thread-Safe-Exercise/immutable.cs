public class User1
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

}