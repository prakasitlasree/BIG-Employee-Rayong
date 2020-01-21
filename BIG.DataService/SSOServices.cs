﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIG.Model;
using BIG.DataAccess;

namespace BIG.DataService
{ 
    public partial class SSOServices
    {
        public static bool Add(List<SSO> list)
        {
            try
            {
                var obj = list.FirstOrDefault();
                if (obj != null)
                {
                    DeleteByEmployeeID(obj.EMP_ID);
                }

                using (var ctx = new BIG_DBEntities())
                {
                    foreach (var objAdd in list)
                    {
                        ctx.SSOes.Add(objAdd);
                    }
                    ctx.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<SSO> GetAll()
        {
            var result = new List<SSO>();
            try
            {
                using (var ctx = new BIG_DBEntities())
                {

                    result = ctx.SSOes.ToList();


                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SSO> GetByEmployeeID(string emp)
        {
            var result = new List<SSO>();
            try
            {
                using (var ctx = new BIG_DBEntities())
                {
                    result = ctx.SSOes.Where(x => x.EMP_ID == emp).ToList();

                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void DeleteByEmployeeID(string emp_id)
        {
            try
            {
                using (var ctx = new BIG_DBEntities())
                {

                    var obj = ctx.SSOes.Where(x => x.EMP_ID == emp_id).FirstOrDefault();
                    if (obj != null)
                    {
                        ctx.SSOes.Remove(obj);
                    }
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
