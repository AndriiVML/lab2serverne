using swp_lab2.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace swp_mbvc_test.Models
{
    public class Cart
    {
        public List<long> BookIds { get; set; }

        public string UserId { get; set; }

        public Cart()
        {
            BookIds = new List<long>();
        }
    }
}