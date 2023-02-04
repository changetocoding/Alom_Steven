
public class CustomDictionary
{
    private string[] _values;

    public CustomDictionary()
    {
        _values = new string[8];
    }

    private int GetHash(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException("key");
        }

        char firstCharacter = key[0];
        var asInt = (int) firstCharacter;

        return asInt % _values.Length;
    }

    public void Insert(string key, string value)
    {
        int hash = this.GetHash(key);
        _values[hash] = value;
    }

    public string Get(string key)
    {
        int hash = this.GetHash(key);

        return _values[hash];
    }
}
