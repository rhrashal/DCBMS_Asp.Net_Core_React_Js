using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCBMS_API.Models
{
    public class Constant
    {
        public static string SYNCED { get { return "Sync completed successfully !"; } }
        public static string SUCCESS { get { return "Request Successful."; } }
        public static string SAVED { get { return "Save Successfully !"; } }
        public static string NOT_FOUND { get { return "No Data Found !"; } }
        public static string SAVED_ERROR { get { return "Data Not Save."; } }
        public static string UPDATED_ERROR { get { return "Data Not Updated."; } }
        public static string FAILED { get { return "Request Failed."; } }
        public static string UPDATED { get { return "Updated Successfully !"; } }
        public static string DELETED { get { return "Deleted Successfully !"; } }
        public static string DELETE_ERROR { get { return "Data not Deleted"; } }
        public static string INVAILD_DATA { get { return "Invaild Data !"; } }
        public static string DUPLICATE { get { return "Data is Duplicated!"; } }
        public static string DATA_EXISTS { get { return "Data is Exist."; } }
    }
}
