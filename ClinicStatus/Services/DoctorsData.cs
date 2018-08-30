
using ClinicStatus.Entities;
using ClinicStatus.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ClinicStatus.Services
{
    public interface IDoctorsData
    {
        IEnumerable<Doctor> GetDoctorByClinic(int id, string floor);
        string GetDoctorName(int spec);
    }

    //public class InMemoryDoctor : IDoctorsData
    //{
    //    private List<Doctor> _Doctors;

    //    public InMemoryDoctor()
    //    {
    //        _Doctors = new List<Doctor>
    //        {
    //            new Doctor{Code = "35", Name = "محسن عبد السميع", DocWait = 2, RecWait = 2, Saw = 2, Seeing = 2, InDuty = true},

    //            new Doctor{Code = "35", Name = "عباس  إبراهيم", DocWait = 6, RecWait = 3, Saw = 2, Seeing = 1, InDuty = true}

    //        };
    //    }


    //    public IEnumerable<Doctor> GetDoctorByClinic(int id, string floor)
    //    {
    //        return _Doctors;
    //    }

    //    public string GetDoctorName(int spec)
    //    {
    //        return _Doctors.Find(r => r.Code == spec.ToString()).Name;
    //    }
    //}

    public class SqlDoctor : IDoctorsData
    {

        IDBConnect connect = new SqlConnect();

        public IEnumerable<Doctor> GetDoctorByClinic(int id, string floor)
        {

            string f = "";

            List<Int32> listInt = new List<int>();

            if (floor == "الدور الأرضي")
            {
                f = "DoctorsVIEW";
            }

            else if (floor == "الدور الأول")
            {

                f = "DoctorsVIEW1";
            }

            List<Doctor> list = new List<Doctor>();

            string CommandText = $"SELECT [Name], [code] FROM [ALDar_Hospital].[dbo].[{f}] Where [Spec] = cast({id} as varchar(50)) ORDER BY [code]";

            string Commandbool = $"SELECT ISNULL([isClinicOpen], 0) as OnDuty FROM [ALDar_Hospital].[dbo].[Doctors] Where [DoctorSpecialtyID] = cast({id} as varchar(50)) ORDER BY [code]";

            DataTable DataTable01 = connect.Command(Commandbool, "Doctors");

            List<DataRow> row01 = new List<DataRow>();

            foreach (DataRow r in DataTable01.Rows)
            {
                listInt.Add(Convert.ToInt32(r["OnDuty"]));
            }

            DataTable myDataTable = connect.Command(CommandText, "DoctorsVIEW");

            List<DataRow> rows = new List<DataRow>();

            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {

                list.Add(new Doctor
                {
                    Name = myDataTable.Rows[i]["Name"].ToString(),
                    Code = (myDataTable.Rows[i]["code"]).ToString(),
                    status = (listInt[i] == 1) ? true : false
                });
            }

            return list;
        }

        public string GetDoctorName(int spec)
        {
            string doctor = "";


            string CommandText = $"SELECT [ArbName] FROM [ALDar_Hospital].[dbo].[Doctors] Where [code] = cast({spec} as varchar(50))";

            DataTable myDataTable = connect.Command(CommandText, "DoctorsSpecialties");

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in myDataTable.Rows)
            {
                doctor = row["ArbName"].ToString();
            }

            return doctor;
        }
    }
}

