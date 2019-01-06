using System;
using System.Collections.Generic;

namespace LinqMetaTest.Types
{
    public class Employee : IEquatable<Employee>
    {
        public int id;
        public string firstName;
        public string lastName;

        public static List<Employee> GetEmployeesArrayList()
        {
            var al = new List<Employee>
            {
                new Employee {id = 1, firstName = "Joe", lastName = "Rattz"},
                new Employee {id = 2, firstName = "William", lastName = "Gates"},
                new Employee {id = 3, firstName = "Anders", lastName = "Hejlsberg"},
                new Employee {id = 4, firstName = "David", lastName = "Lightman"},
                new Employee {id = 101, firstName = "Kevin", lastName = "Flynn"}
            };

            return al;
        }

        public bool Equals(Employee other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return id == other.id && string.Equals(firstName, other.firstName) && string.Equals(lastName, other.lastName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Employee) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = id;
                hashCode = (hashCode * 397) ^ (firstName != null ? firstName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (lastName != null ? lastName.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}