    internal class MyDictionary
    {
        // private because variables are used inside of the constructors, variables would not be used externally
        // Create variable "data" which is an array of keyValuePair, both key and value are strings
        private int arraySize = 100;
        private KeyValuePair<string, string>[] data;

        // create public constructor called "MyDictionary" and initialize with KeyValuePair array
        public MyDictionary()
        {
            data = new KeyValuePair<string, string>[arraySize];
        }

        // create Hash method, take string and convert the first character to a string 
        public int Hash(string key)
        {
            // Use the first character in the key to hash
            // using remainder operator to ensure that the generated int is within the arraySize
            // DO NOT USE THIS, poor hashing algo which will result in collisions
            return (int)key[0] % arraySize;
        }

        // create Add method to add key-value pair to dictionary
        public bool Add(string key, string value)
        {
            // use Hash method to generate index in dictionary
            // Add key-value pair add index and return true per H/w requirement
            int index = Hash(key);
            data[index] = new KeyValuePair<string, string>(key, value);
            return true;
        }

        // Get(null)
        // Empty arrary: Get("Test");

        // Class - everyone
        // Struct - Guarantees always a value -> int, bool

        //create Get method which take a string as key and return value based on the key
        public string Get(string key) 
        {
            if (string.IsNullOrEmpty(key)){
                return null;
            }
            
            // use Hash method to generate index in dictionary
            // if index is not null then return value else return key not found
            int index = Hash(key);
            if (data[index].Key != null && key == data[index].Key)
            {
                return data[index].Value;
            }
            else
                return "Key not found";
        }

        // create remove method whcih takes a string and removes the key-value pair from the dictionary
        public bool Remove(string key)
        {
            // use Hash method to generate index in dictionary
            // if index is not null then replace the key-value pair with new empty KeyValuePair<string, string> object at index
            int index = Hash(key);
            if (data[index].Key != null)
            {
                data[index] = new KeyValuePair<string, string>();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
