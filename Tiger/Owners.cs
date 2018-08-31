using System.Collections.Generic;
using System.Linq;

namespace Tiger
{
    public class Owners
    {
        private List<Student> owners;
        public Owners(params Student[] students) { owners = students.ToList(); }
        public override string ToString()
        {
            string result = "";
            foreach (var student in owners)
                result += student.FullName + " " + student.Group + " & ";
            return result.Substring(0, result.Length - 3);
        }
    }
}
