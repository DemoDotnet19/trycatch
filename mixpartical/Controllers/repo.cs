using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace mixpartical.Controllers
{
    public class repo
    {
        string conect = ConfigurationManager.ConnectionStrings["connectionst"].ToString();
        public repo()
        {
        }

        public class data
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        public class coustomerinfo
        {
            public int id { get; set; }
            public int country_id { get; set; }
            public int state_id { get; set; }
            public int city_id { get; set; }
            public string platfromname { get; set; }
        }
        internal List<data> getalldata()
        {
            using (var dc=new SqlConnection(conect))
            {
                return dc.Query<data>("Usp_get_countrys", commandType: CommandType.StoredProcedure).AsList();

            }
        }

        internal List<data> getallcity(int id)
        {
            using (var dc = new SqlConnection(conect))
            {
                return dc.Query<data>("uap_Get_cityes", new { id}, commandType: CommandType.StoredProcedure).AsList();

            }
        }

        internal bool deletedata(int id)
        {
            using (var dc = new SqlConnection(conect))
            {
                return dc.Execute("usp_del_csc", new { id}, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        internal coustomerinfo editedata(int id)
        {
            using (var dc = new SqlConnection(conect))
            {
                return dc.QueryFirstOrDefault<coustomerinfo>("usp_get_csc",new { id}, commandType: CommandType.StoredProcedure);

            }
        }

        public class alltabledata: coustomerinfo
        {
            public string countryname { get; set; }
            public string statename { get; set; }
            public string cityname { get; set; }
        }

        internal List<alltabledata> getalltabledata()
        {
            using (var dc = new SqlConnection(conect))
            {
                return dc.Query<alltabledata>("usp_get_cscs",  commandType: CommandType.StoredProcedure).AsList();

            }
        }

        internal bool save(coustomerinfo save)
        {
            using (var dc = new SqlConnection(conect))
            {
                return dc.Execute("usp_save_csc", save, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        internal List<data> getallstate(int id)
        {
            using (var dc = new SqlConnection(conect))
            {
                return dc.Query<data>("uap_Get_states", new { id }, commandType: CommandType.StoredProcedure).AsList();

            }
        }
    }
}