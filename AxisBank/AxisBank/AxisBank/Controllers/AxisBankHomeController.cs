using AxisBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AxisBank.Controllers
{
    public class AxisBankHomeController : Controller
    {

        /*
        Summary
        author: S Rudra Prasad Choudhury
        date of creation: 05/10/2019    
        description : This is a Project of banking system. This is the home controller of the project.
         */


        // GET: AxisBankHome
        public ActionResult Landing()
        {
            return View();
        }


        //Login Form Action

        public ActionResult Login()
        {
            return View();
        }

        //Login form post action
        [HttpPost]
        public ActionResult Login(AxisBank_tblAllAccount axisAta)
                    {
            if (ModelState.IsValid)
            {
                try
                {
                    using (AxisBankDBEntities db = new AxisBankDBEntities())
                    {
                        //first check the  isActive cplumn is true or False then proced to next step
                        if (db.AxisBank_tblAllAccount.Any(x => x.CustomerId == axisAta.CustomerId && x.IsActive == false))
                        {
                            ViewBag.Message = "Your Account is Deactivated";
                            return View("Login");
                        }
                        else
                        {
                            //check this customerid and password is matching in axisbanl_allaccount and role id should be 1004 in a if block
                            if (db.AxisBank_tblAllAccount.Any(x => x.CustomerId == axisAta.CustomerId && x.Password == axisAta.Password && x.RoleId == 1005))
                            {

                                //Get the firstname firstLetter and Role name from axisbank_tblallaccount and axisbank_tblrole
                                var firstName = db.AxisBank_tblAllAccount.Where(x => x.CustomerId == axisAta.CustomerId).Select(x => x.FirstName).FirstOrDefault();
                                var firstLetter = firstName.Substring(0, 1);
                                var roleName = db.AxisBank_tblRole.Where(x => x.Id == 1005).Select(x => x.Role).FirstOrDefault();


                                Session["firstName"] = firstName;
                                Session["roleName"] = roleName;
                                Session["firstLetter"] = firstLetter;


                                //if true then redirect to user panel
                                return RedirectToAction("AdminDashboard", "Admin");
                            }
                            else if (db.AxisBank_tblAllAccount.Any(x => x.CustomerId == axisAta.CustomerId && x.Password == axisAta.Password && x.RoleId == 1006))
                            {
                                var firstName = db.AxisBank_tblAllAccount.Where(x => x.CustomerId == axisAta.CustomerId).Select(x => x.FirstName).FirstOrDefault();
                                var firstLetter = firstName.Substring(0, 1);
                                var roleName = db.AxisBank_tblRole.Where(x => x.Id == 1006).Select(x => x.Role).FirstOrDefault();

                                Session["firstName"] = firstName;
                                Session["roleName"] = roleName;
                                Session["firstLetter"] = firstLetter;
                                return RedirectToAction("CashierPanel", "Cashier");
                            }
                            else if (db.AxisBank_tblAllAccount.Any(x => x.CustomerId == axisAta.CustomerId && x.Password == axisAta.Password && x.RoleId == 1007))
                            {
                                var firstName = db.AxisBank_tblAllAccount.Where(x => x.CustomerId == axisAta.CustomerId).Select(x => x.FirstName).FirstOrDefault();
                                var firstLetter = firstName.Substring(0, 1);
                                var roleName = db.AxisBank_tblRole.Where(x => x.Id == 1007).Select(x => x.Role).FirstOrDefault();


                                Session["firstName"] = firstName;
                                Session["roleName"] = roleName;
                                Session["firstLetter"] = firstLetter;

                                return RedirectToAction("UserPanel", "User");
                            }
                            else if (db.AxisBank_tblAllAccount.Any(x => x.CustomerId == axisAta.CustomerId && x.Password == axisAta.Password && x.RoleId == 1008))
                            {
                                var firstName = db.AxisBank_tblAllAccount.Where(x => x.CustomerId == axisAta.CustomerId).Select(x => x.FirstName).FirstOrDefault();
                                var firstLetter = firstName.Substring(0, 1);
                                var roleName = db.AxisBank_tblRole.Where(x => x.Id == 1008).Select(x => x.Role).FirstOrDefault();

                                Session["firstName"] = firstName;
                                Session["roleName"] = roleName;
                                Session["firstLetter"] = firstLetter;


                                return RedirectToAction("WorkerPanel", "Worker");

                            }
                            else
                            {
                                ViewBag.Message = "Invalid User";
                            }

                        }




                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }

            return View("Login");
        }

        //for singup form
        public ActionResult SignUp()
        {
            return View();
        }

        // signup form post action
        [HttpPost]
        public ActionResult SignUp(AxisBank_tblLogin axisBank_Signup)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (AxisBankDBEntities db = new AxisBankDBEntities())
                    {
                        if (db.AxisBank_tblLogin.Any(x => x.UserName == axisBank_Signup.UserName))
                        {
                            ViewBag.Message = "User Already Exist";

                        }
                        else
                        {
                            axisBank_Signup.CreatedOn = DateTime.Now;
                            axisBank_Signup.CreatedBy = axisBank_Signup.UserName;
                            axisBank_Signup.IsActive = true;

                            db.AxisBank_tblLogin.Add(axisBank_Signup);
                            db.SaveChanges();
                            ViewBag.Message = "User Added Successfully";
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }




            return View("Login");
        }



    }
}