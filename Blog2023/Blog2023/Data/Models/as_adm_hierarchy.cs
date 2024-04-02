namespace Blog2023.Data.Models
{
    public class as_adm_hierarchy
    {
        public long id { get; set; }

        public long? objectid { get; set; }

        public long? parentobjid { get; set; }

     
        public long? changeid { get; set; }

     
        public string? regioncode { get; set; }

      
        public string? areacode { get; set; }

      
        public string? citycode { get; set; }

      
        public string? placecode { get; set; }

        public string? plancode { get; set; }

        public string? streetcode { get; set; }

 
        public long? previd { get; set; }


        public long? nextid { get; set; }


        public DateOnly? updatedate { get; set; }


        public DateOnly? startdate { get; set; }

        public DateOnly? enddate { get; set; }

    
        public int? isactive { get; set; }

   
        public string? path { get; set; }
    }
}

