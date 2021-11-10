using swp_lab2.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace swp_mbvc_test.Models
{
    public class BookDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual List<SageSelectDTO> SagesSelect { get; set; }

        public BookDTO()
        {
            SagesSelect = new List<SageSelectDTO>();
        }

        public override bool Equals(object obj)
        {
            var item = obj as Book;

            if (item == null)
                return false;

            return this.Id == item.Id;
        }
    }

    public class SageSelectDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsAdded { get; set; }
    }
}