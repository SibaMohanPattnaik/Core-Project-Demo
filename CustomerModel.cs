using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Http;

namespace CoreAssement.Models
{
    public class CustomerModel
    {
      
          string stroutMsg = null;
          public int Id;
         [Required(ErrorMessage = "Enter Name")]
            public string Name { get; set; }


        [Required(ErrorMessage = "Enter Mobile")]
        public string Mobile { get; set; }


       // [Required(ErrorMessage = "Upload Image")]
        public string ImageUpload { get; set; }
       

        [Required(ErrorMessage = "Enter Designation")]
           public string Designation { get; set; }

        [Required(ErrorMessage = "Select Country")]
        public string Country { get; set; }


        [Required(ErrorMessage = "Enter Gender")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Enter User Name")]
            public string UserName { get; set; }            

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       


        [Required(ErrorMessage = "please confirm Password")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password")]
            [StringLength(50)]
            public string CPassword { get; set; }



        //public string strCon = "Server=server6; Database=COREDB28;User Id=dbuser28;Password=core@149;";
        public string strCon = "Server=Server15\\SQL17; Database=DOTNET_CORE25;User Id=core25;Password=core#1234;";
       // public string strCon = "User Id=sa;Password=sql;Server=SIBA-PC;Database=ProjectMeeting;";



        public int InsertEmp(CustomerModel em)
            {

                using (IDbConnection con = new SqlConnection(em.strCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@P_Action", "I");
                    param.Add("@P_Name", em.Name);
                    param.Add("@P_Address", em.Designation);
                    param.Add("@P_Mobile", em.Mobile);
                    param.Add("@P_Email", em.UserName);
                    param.Add("@P_Gender", em.Gender);
                    param.Add("@P_Country", em.Country);
                    param.Add("@P_Password", em.Password);
                param.Add("@P_Image", em.ImageUpload);
                
                    param.Add("@P_msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    string res = con.Query<string>("usp_Customer_aed", param, commandType: CommandType.StoredProcedure).ToString();
                    stroutMsg = param.Get<string>("@P_msg");
                }
                return Convert.ToInt32(stroutMsg);

            }

            //Login
            //Login
            public int LoginCheck(CustomerModel login)
            {
                try
                {
                    string strOutuput = null;
                    using (IDbConnection con = new SqlConnection(login.strCon))
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        strOutuput = string.Empty;
                        var param = new DynamicParameters();
                        param.Add("@P_Email", login.UserName);
                        param.Add("@P_Password", login.Password);
                        param.Add("@P_Action", "L");
                        param.Add("@P_msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                        string msgout = con.Query<string>("usp_Customer_aed", param, commandType: CommandType.StoredProcedure).ToString();
                        strOutuput = param.Get<string>("@P_msg");
                        con.Close();
                    }
                    return Convert.ToInt32(strOutuput);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        public IEnumerable<CustomerModel> Getdata()
        {
            CustomerModel objStaticcla = new CustomerModel();
            List<CustomerModel> objStaticclaList = new List<CustomerModel>();

            using (IDbConnection con = new SqlConnection(objStaticcla.strCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@P_Action", "S");
                objStaticclaList = con.Query<CustomerModel>("usp_Customer_aed", parameter, commandType: CommandType.StoredProcedure).ToList();
            }
            return objStaticclaList;
        }


        public  int DeleteCustInfo(int ID)
        {
            CustomerModel em=new CustomerModel();
            int rowAffected = 0;
            using (IDbConnection con = new SqlConnection(em.strCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@P_Action", "D");
                parameters.Add("@P_Id", ID);
                rowAffected = con.Execute("usp_Customer_aed", parameters, commandType: CommandType.StoredProcedure);

            }

            return rowAffected;
        }

    }
}
