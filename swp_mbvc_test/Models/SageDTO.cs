using swp_lab2.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace swp_mbvc_test.Models
{
    public class SageDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public HttpPostedFileBase Photo { get; set; }

        public string City { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as Sage;

            if (item == null)
                return false;

            return this.Id == item.Id;
        }
    }
}