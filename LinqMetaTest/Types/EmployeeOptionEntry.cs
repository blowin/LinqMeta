using System;
using System.Collections.Generic;

namespace LinqMetaTest
{
    public class EmployeeOptionEntry : IEquatable<EmployeeOptionEntry>
    {
        public int id;
        public long optionsCount;
        public DateTime dateAwarded;

        public static EmployeeOptionEntry[] GetEmployeeOptionEntries()
        {
            EmployeeOptionEntry[] empOptions = {
                new EmployeeOptionEntry { 
                    id = 1, 
                    optionsCount = 2, 
                    dateAwarded = DateTime.Parse("1999/12/31") },
                new EmployeeOptionEntry { 
                    id = 2, 
                    optionsCount = 10000, 
                    dateAwarded = DateTime.Parse("1992/06/30")  },
                new EmployeeOptionEntry { 
                    id = 2, 
                    optionsCount = 10000, 
                    dateAwarded = DateTime.Parse("1994/01/01")  },
                new EmployeeOptionEntry { 
                    id = 3, 
                    optionsCount = 5000, 
                    dateAwarded = DateTime.Parse("1997/09/30") },
                new EmployeeOptionEntry { 
                    id = 2, 
                    optionsCount = 10000, 
                    dateAwarded = DateTime.Parse("2003/04/01")  },
                new EmployeeOptionEntry { 
                    id = 3, 
                    optionsCount = 7500, 
                    dateAwarded = DateTime.Parse("1998/09/30") },
                new EmployeeOptionEntry { 
                    id = 3, 
                    optionsCount = 7500, 
                    dateAwarded = DateTime.Parse("1998/09/30") },
                new EmployeeOptionEntry { 
                    id = 4, 
                    optionsCount = 1500, 
                    dateAwarded = DateTime.Parse("1997/12/31") },
                new EmployeeOptionEntry { 
                    id = 101, 
                    optionsCount = 2, 
                    dateAwarded = DateTime.Parse("1998/12/31") }
            };

            return empOptions;
        }

        public static bool Compare(List<List<EmployeeOptionEntry>> first, List<List<EmployeeOptionEntry>> second)
        {
            if (first.Count != second.Count)
                return false;
                
            var count = first.Count;
            for (var i = 0; i < count; i++)
            {
                var linqI = first[i];
                var metaI = second[i];
                if (linqI.Count != metaI.Count)
                    return false;

                var iterCount = linqI.Count;
                for (var j = 0; j < iterCount; j++)
                {
                    if (!metaI.Contains(linqI[j]))
                        return false;
                }
            }

            return true;
        }
            
        public bool Equals(EmployeeOptionEntry other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return id == other.id && optionsCount == other.optionsCount && dateAwarded.Equals(other.dateAwarded);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EmployeeOptionEntry) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = id;
                hashCode = (hashCode * 397) ^ optionsCount.GetHashCode();
                hashCode = (hashCode * 397) ^ dateAwarded.GetHashCode();
                return hashCode;
            }
        }
    }
}