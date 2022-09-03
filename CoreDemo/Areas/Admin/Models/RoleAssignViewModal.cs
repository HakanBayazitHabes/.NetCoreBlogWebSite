using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.Models
{
    public class RoleAssignViewModal
    {
        public int RoleID { get; set; }
        public string Name { get; set; }
        public bool Exists { get; set; }
    }
}
