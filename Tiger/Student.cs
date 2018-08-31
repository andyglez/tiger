namespace Tiger
{
    public class Student
    {
        public string FullName { get; private set; }
        public string Group { get; private set; }
        public Student(string name, string group)
        {
            FullName = name;
            Group = group;
        }
        public override string ToString()
        {
            return FullName;
        }
    }

    
}
