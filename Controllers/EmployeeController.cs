using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Practice.Data;
using Practice.Models;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IConfiguration configuration;
        Msdblayer msdb = new Msdblayer();
        [HttpPost("Add Employee_Department_Details")]
        public dynamic AddEmployee([FromBody] Employee_Department ep)
        {
            try
            {
                SqlParameter[] paramArr = new SqlParameter[6];
                paramArr[0] = new SqlParameter("@Id", SqlDbType.BigInt);
                paramArr[0].Value = ep.Id;

                paramArr[1] = new SqlParameter("@Department_Name", SqlDbType.NVarChar, -1);
                paramArr[1].Value = ep.Department_Name;

                paramArr[2] = new SqlParameter("@Employee_Name", SqlDbType.NVarChar, -1);
                paramArr[2].Value = ep.Employee_Name;

                paramArr[3] = new SqlParameter("@Employee_Salary", SqlDbType.NVarChar, -1);
                paramArr[3].Value = ep.Employee_Salary;

                paramArr[4] = new SqlParameter("@MODE", SqlDbType.Char, 2);
                paramArr[4].Value = ep.MODE;

                paramArr[5] = new SqlParameter("@ErrorNo", SqlDbType.Int);
                paramArr[5].Direction = ParameterDirection.Output;

                DataTable dt = new DataTable();
                PracticeResponse RR = new PracticeResponse();
                Response rs = new Response();
                rs.StatusCode = "500";
                rs.Res = "Something Went Wrong";

                if (ep.MODE == 'E')
                {
                    if (msdb.ExecuteSP("sp_Employee_Department_ManagementSystems", paramArr, ref paramArr))
                    {
                        int status = int.Parse(paramArr[5].Value.ToString());
                        if (status == 0)
                        {

                            rs.StatusCode = StatusCodes.Status200OK.ToString();
                            rs.Res = "Saved Successfully";
                        }

                        else
                        {
                            rs.StatusCode = "300";
                            rs.Res = "Data Not Saved";
                        }
                    }
                    else
                    {
                        rs.StatusCode = "500";
                        rs.Res = "Something Went Wrong";
                    }
                }
                if (ep.MODE == 'D')
                {
                    if (msdb.ExecuteSP("sp_Employee_Department_ManagementSystems", paramArr, ref paramArr))
                    {
                        int status = int.Parse(paramArr[5].Value.ToString());
                        if (status == 0)
                        {

                            rs.StatusCode = StatusCodes.Status200OK.ToString();
                            rs.Res = "Saved Successfully";
                        }

                        else
                        {
                            rs.StatusCode = "300";
                            rs.Res = "Data Not Saved";
                        }
                    }
                    else
                    {
                        rs.StatusCode = "500";
                        rs.Res = "Something Went Wrong";
                    }
                }
                RR.Res = rs;
                return JsonConvert.SerializeObject(RR);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error");

            }
        }
        [HttpPut("Update Employee_Department_Details")]
        public dynamic UpdateEmployee([FromBody] Employee_Department ep)
        {
            try
            {
                SqlParameter[] paramArr = new SqlParameter[6];
                paramArr[0] = new SqlParameter("@Id", SqlDbType.BigInt);
                paramArr[0].Value = ep.Id;

                paramArr[1] = new SqlParameter("@Department_Name", SqlDbType.NVarChar, -1);
                paramArr[1].Value = ep.Department_Name;

                paramArr[2] = new SqlParameter("@Employee_Name", SqlDbType.NVarChar, -1);
                paramArr[2].Value = ep.Employee_Name;

                paramArr[3] = new SqlParameter("@Employee_Salary", SqlDbType.NVarChar, -1);
                paramArr[3].Value = ep.Employee_Salary;

                paramArr[4] = new SqlParameter("@MODE", SqlDbType.Char, 2);
                paramArr[4].Value = ep.MODE;

                paramArr[5] = new SqlParameter("@ErrorNo", SqlDbType.Int);
                paramArr[5].Direction = ParameterDirection.Output;

                DataTable dt = new DataTable();
                PracticeResponse RR = new PracticeResponse();
                Response rs = new Response();
                rs.StatusCode = "500";
                rs.Res = "Something Went Wrong";

                if (ep.MODE.ToString() == "UE")
                {
                    if (msdb.ExecuteSP("sp_Employee_Department_ManagementSystems", paramArr, ref paramArr))
                    {
                        int status = int.Parse(paramArr[5].Value.ToString());
                        if (status == 0)
                        {

                            rs.StatusCode = StatusCodes.Status200OK.ToString();
                            rs.Res = "Saved Successfully";
                        }

                        else
                        {
                            rs.StatusCode = "300";
                            rs.Res = "Data Not Saved";
                        }
                    }
                    else
                    {
                        rs.StatusCode = "500";
                        rs.Res = "Something Went Wrong";
                    }
                }
                if (ep.MODE.ToString() == "UD")
                {
                    if (msdb.ExecuteSP("sp_Employee_Department_ManagementSystems", paramArr, ref paramArr))
                    {
                        int status = int.Parse(paramArr[5].Value.ToString());
                        if (status == 0)
                        {

                            rs.StatusCode = StatusCodes.Status200OK.ToString();
                            rs.Res = "Saved Successfully";
                        }

                        else
                        {
                            rs.StatusCode = "300";
                            rs.Res = "Data Not Saved";
                        }
                    }
                    else
                    {
                        rs.StatusCode = "500";
                        rs.Res = "Something Went Wrong";
                    }
                }
                RR.Res = rs;
                return JsonConvert.SerializeObject(RR);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error");

            }
        }

        [HttpGet("View Employee_Department_Details")]
        public dynamic ViewEmployee_Department_Details([FromBody] Employee_Department_View vp)
        {
            try
            {
                SqlParameter[] paramArr = new SqlParameter[8];
                paramArr[0] = new SqlParameter("@Emp_Id", SqlDbType.BigInt);
                paramArr[0].Value = vp.Emp_Id;
                paramArr[1] = new SqlParameter("@Dept_Id", SqlDbType.BigInt);
                paramArr[1].Value = vp.Dept_Id;
                paramArr[2] = new SqlParameter("@JSONOUT", SqlDbType.NVarChar, -1);
                paramArr[2].Direction = ParameterDirection.Output;
                string JSONOUTPUT = "";
                DataSet ds = new DataSet();
                if (msdb.ExecuteSP("sp_Department_Employee_Details", paramArr, ds, ""))
                {
                    JSONOUTPUT = paramArr[2].Value.ToString();

                }
                return JSONOUTPUT;
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
