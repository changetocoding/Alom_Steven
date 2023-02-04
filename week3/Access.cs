    public class MyClass
    {
        public int _myField;

        private void MyMethod()
        {
            _myField++;
        }
    }

    class MyChildClass : MyClass
    {
        private void MyMethod()
        {
            _myField++;
        }
    }

    class MyThirdClass
    {
        private void MyMethod()
        {
            var myClass = new MyClass();
            myClass._myField++;
            myClass.GetHashCode();
        }
    }