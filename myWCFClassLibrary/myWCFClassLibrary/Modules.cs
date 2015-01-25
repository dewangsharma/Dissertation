using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data.Entity;

namespace myWCFClassLibrary
{
    public class Modules: IModules
    {
        public IEnumerable<DepartmentModule> GetAllModulesByProfessor(int ProfessorId)
        {
            using(myProjectEntities context = new myProjectEntities())
            {
                List<tblDepartmentModule> pro = new List<tblDepartmentModule>();
              //First get List of modules 
              List<tblProfessorModule> moduleId =  context.tblProfessorModules.Where(q => q.FK_Professor_id == ProfessorId).ToList();
              foreach (tblProfessorModule mId in moduleId)
              {
                  tblDepartmentModule d = context.tblDepartmentModules.Where(a => a.PK_Module_id == mId.FK_Module_id).FirstOrDefault();
                  pro.Add(d);
              }
              Mapper.CreateMap<tblDepartmentModule, DepartmentModule>();
              IEnumerable<DepartmentModule> result = Mapper.Map<IEnumerable<tblDepartmentModule>, IEnumerable<DepartmentModule>>(pro);
                return result;
            }
        }
        // Get all the modules of the Student 
        public IEnumerable<DepartmentModule> GetAllModulesByStudent(int StudentId)
        {
            using(myProjectEntities context = new myProjectEntities())
            {
                List<tblDepartmentModule> pro = new List<tblDepartmentModule>();
                //First get List of modules 
                List<tblStudentModule> moduleId = context.tblStudentModules.Where(q => q.FK_Student_id == StudentId).ToList();
                foreach (tblStudentModule mId in moduleId)
                {
                    tblDepartmentModule d = context.tblDepartmentModules.Where(a => a.PK_Module_id == mId.FK_Module_id).FirstOrDefault();
                    pro.Add(d);
                }
                Mapper.CreateMap<tblDepartmentModule, DepartmentModule>();
                IEnumerable<DepartmentModule> result = Mapper.Map<IEnumerable<tblDepartmentModule>, IEnumerable<DepartmentModule>>(pro);
                return result;
            }
        }
    }
}
