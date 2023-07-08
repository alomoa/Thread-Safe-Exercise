public class PhoneBook
{
    private Dictionary<string, long> _phonebook;
    private object _lock = new object();

    public void AddNumber(string name, long number)
    {
        lock (_lock)
        {
            if (!_phonebook.ContainsKey(name))
            {
                _phonebook.Add(name, number);
            }
            else
            {
                _phonebook[name] = number;
            }
        }
    }

    public void Clear()
    {
        lock (_lock)
        {
            _phonebook.Clear();
        }
    }
}

// _phonebook.Clear() doesn't lock the phonebook. So a thread that clears the phonebook can happen as another thread is adding a number.  
// Solution: lock the phonebook when clearing