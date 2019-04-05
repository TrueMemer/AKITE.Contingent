using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AKITE.Contingent.Models;

namespace AKITE.Contingent.Client.Services
{
    public static class GroupDataService
    {
        private static readonly BindingList<Group> Groups;

        static GroupDataService()
        {
            Groups = new BindingList<Group>
            {
                new Group { SpecialtyId = 0 },
                new Group { GroupID = 17, GroupNum = 1, SpecialtyId = 2 },
                new Group { GroupID = 17, GroupNum = 2, SpecialtyId = 2 },
                new Group { GroupID = 17, GroupNum = 3, SpecialtyId = 2 },
                new Group { GroupID = 19, GroupNum = 1, SpecialtyId = 1 },
                new Group { GroupID = 19, GroupNum = 2, SpecialtyId = 1 },
            };
        }

        public static void AddGroup(Group group)
        {
            if (group != null)
                Groups.Add(group);
        }

        public static IEnumerable<Group> GetGroups()
        {
            return Groups;
        }
    }
}
