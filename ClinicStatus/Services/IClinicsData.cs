
using ClinicStatus.Entities;
using ClinicStatus.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicStatus.Services
{
    public interface IClinicsData
    {
        IEnumerable<Clinic> GetCliniByFloor(int id);
        Clinic Get(int spec);
    }

    public class InMemoryclinic : IClinicsData
    {
        private List<Clinic> _clinics;

        public InMemoryclinic()
        {
            _clinics = new List<Clinic>
            {
                new Clinic{Speciality = 160, Name =  "عيادة الأسنان", Floor = 1},
                new Clinic{Speciality = 148, Name = "عيادة العظام", Floor = 0},
                new Clinic{Speciality = 171, Name = "عيادة جراحة العيون", Floor = 1},
                new Clinic{Speciality = 162, Name = "عيادة الأطفال", Floor = 1},
                new Clinic{Speciality = 163, Name = "عيادة الأنف والأذن والحنجرة", Floor = 1},
                new Clinic{Speciality = 177, Name = "عيادة الجراحة التجميلية", Floor = 1},
                new Clinic{Speciality = 167, Name = "طوارئ النساء الأطفال", Floor = 1},
                new Clinic{Speciality = 169, Name = "عيادة السمعيات", Floor = 1},
                new Clinic{Speciality = 161, Name = "عيادة الجلدية", Floor = 1},
                new Clinic{Speciality = 173, Name = "عيادة التخاطب", Floor = 1},
                new Clinic{Speciality = 178, Name = "عيادة الجراحة العامة", Floor = 0},
                new Clinic{Speciality = 151, Name = "العيادة النفسية", Floor = 0},
                new Clinic{Speciality = 141, Name = "عيادة المعالجة الفيزيائية", Floor = 0},
                new Clinic{Speciality = 174, Name = "عيادة الأمراض العصبية", Floor = 0},
                new Clinic{Speciality = 176, Name = "عيادة المخ والأعصاب", Floor = 0},
                new Clinic{Speciality = 168, Name = "عيادة الأمراض العصبية", Floor = 0},
                new Clinic{Speciality = 172, Name = "عيادة الأمراض الهضمية", Floor = 0},
                new Clinic{Speciality = 167, Name = "عيادة النساء والولادة", Floor = 0}
            };
        }

        public Clinic Get(int spec)
        {
            return _clinics.FirstOrDefault(c => c.Speciality == spec);
        }

        public IEnumerable<Clinic> GetCliniByFloor(int floor)
        {
            return _clinics.FindAll(c => c.Floor == floor);
        }
    }

    public class SqlClinic : IClinicsData
    {

        IDBConnect connect = new SqlConnect();

        public Clinic Get(int spec)
        {
            Clinic clinic = new Clinic();

            string CommandText = "SELECT [ID], [ArbName],[Remarks] FROM [ALDar_Hospital].[dbo].[DoctorsSpecialties] where [REMARKS] IS NOT NULL AND [ID] = " + spec;

            DataTable myDataTable = connect.Command(CommandText, "DoctorsSpecialties");

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in myDataTable.Rows)
            {
                clinic.Speciality = Convert.ToInt32(row["ID"]);
                clinic.Name = row["ArbName"].ToString();
                clinic.Floor = Convert.ToInt32(row["Remarks"]);                
            }

            return clinic;
        }

        public IEnumerable<Clinic> GetCliniByFloor(int id)
        {
            List<Clinic> list = new List<Clinic>();

            string CommandText = "SELECT [ID], [ArbName],[Remarks] FROM [ALDar_Hospital].[dbo].[DoctorsSpecialties] where [REMARKS] IS NOT NULL AND [Remarks] = " + id;

            DataTable myDataTable = connect.Command(CommandText, "DoctorsSpecialties");

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in myDataTable.Rows)
            {
                list.Add(new Clinic
                {
                   
                    Speciality = Convert.ToInt32(row["ID"]),
                    Name = row["ArbName"].ToString(),
                    Floor = Convert.ToInt32(row["Remarks"])
                }); ;
            }

            return list;
        }

    }
}
