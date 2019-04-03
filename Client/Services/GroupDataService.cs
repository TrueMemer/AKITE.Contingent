using AKITE.Contingent.Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKITE.Contingent.Client.Services
{
    public static class GroupDataService
    {
        private static BindingList<Group> _groups;

        static GroupDataService()
        {
            _groups = new BindingList<Group>
            {
                new Group { SpecialtyID = 0 },
                new Group { GroupID = 17, GroupNum = 1, SpecialtyID = 2 },
                new Group { GroupID = 17, GroupNum = 2, SpecialtyID = 2 },
                new Group { GroupID = 17, GroupNum = 3, SpecialtyID = 2 },
                new Group { GroupID = 19, GroupNum = 1, SpecialtyID = 1 },
                new Group { GroupID = 19, GroupNum = 2, SpecialtyID = 1 },
            };
        }

        public static void AddGroup(Group group)
        {
            if (group != null)
                _groups.Add(group);
        }

        public static IEnumerable<Group> GetGroups()
        {
            return _groups;
        }
    }
}
