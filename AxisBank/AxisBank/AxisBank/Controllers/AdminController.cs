﻿using AxisBank.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AxisBank.Controllers
{
    public class AdminController : Controller
    {


        //for admin landing page 
        public ActionResult AdminDashboard()
        {

            return View();
        }

        public ActionResult ShowAllUsers()
        {
            try
            {
                using (AxisBankDBEntities db = new AxisBankDBEntities())
                {
                    var result = db.AxisBank_tblAllAccount.ToList();
                    return View(result);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        //for create new Account
        public ActionResult CreateNewAccount()
        {
            ViewData["ROLEselectListItems"] = AppModels.RoleSelectListItemsByRoleId();
            return View();

        }


        [HttpPost]
        public ActionResult CreateNewAccount(AxisBank_tblAllAccount data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (AxisBankDBEntities db = new AxisBankDBEntities())
                    {
                        // Check if the data was successfully saved
                        data.CreatedOn = DateTime.Now;
                        string FirstName = Session["FirstName"] as string;
                        data.CreatedBy = FirstName;
                        data.IsActive = true;

                        //create a autogenerated 5-digit customerId 
                        Random random = new Random();
                        var customerId = random.Next(10000, 99999);
                        data.CustomerId = customerId.ToString();

                        var accountNo = random.Next(100000000, 999999999);
                        data.AccountNo = accountNo.ToString();

                        db.AxisBank_tblAllAccount.Add(data);
                        var i = db.SaveChanges();
                        if (data.Id > 0)
                        {

                            //insert id to all tables
                            db.AxisBank_tblBalance.Add(new AxisBank_tblBalance
                            {
                                AccountId = data.Id,
                                Balance = 0,
                                CreatedOn = DateTime.Now,
                                CreatedBy = FirstName,
                                IsActive = true
                            });

                            db.AxisBank_tblLogin.Add(new AxisBank_tblLogin
                            {
                                UserName = customerId.ToString(),
                                Password = data.Password,
                                RoleId = data.RoleId,
                                CreatedOn = DateTime.Now,
                                CreatedBy = FirstName,
                                IsActive = true
                            });
                            var j = db.SaveChanges();

                            TempData["SuccessMessage"] = "Successfully registered!";
                            return RedirectToAction("ShowAllUsers");
                        }
                        else
                        {

                            TempData["ErrorMessage"] = "Registration failed. Please try again.";
                            return RedirectToAction("CreateNewAccount");
                        }


                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the data.");
                }
            }
            // Registration failed, set an error message
            TempData["ErrorMessage"] = "Registration failed. Please try again.";
            return RedirectToAction("CreateNewAccount");
        }


        public ActionResult EditAccountDetails(int id)
        {
            ViewData["ROLEselectListItems"] = AppModels.RoleSelectListItemsByRoleId();
            try
            {
                using (AxisBankDBEntities db = new AxisBankDBEntities())
                {
                    var result = db.AxisBank_tblAllAccount.Where(x => x.Id == id).FirstOrDefault();
                    return View(result);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpPost]
        public ActionResult EditAccount(AxisBank_tblAllAccount data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (AxisBankDBEntities db = new AxisBankDBEntities())
                    {
                        //var result = db.AxisBank_tblAllAccount.Where(D => D.IsActive == true && D.Id == data.Id).FirstOrDefault();

                        //get the details of this id from AxisBank_tblAllAccount

                        var result = db.AxisBank_tblAllAccount.Where(x => x.Id == data.Id && x.IsActive == true).FirstOrDefault();

                        if (result != null)
                        {
                            result.UpdatedOn = DateTime.Now;
                            string FirstName = Session["FirstName"] as string;
                            result.UpdatedBy = FirstName;

                            result.FirstName = data.FirstName;
                            result.LastName = data.LastName;
                            result.Password = data.Password;
                            result.Gender = data.Gender;
                            result.MobileNo = data.MobileNo;
                            result.Email = data.Email;
                            result.MaritalStatus = data.MaritalStatus;
                            result.DateOfBirth = data.DateOfBirth;
                            result.IdCardType = data.IdCardType;
                            result.IdCardNumber = data.IdCardNumber;
                            result.Address = data.Address;
                            result.RoleId = data.RoleId;

                            var i = db.SaveChanges();
                            if (data.Id > 0)
                            {


                                var result2 = db.AxisBank_tblLogin.Where(x => x.UserName == data.CustomerId && x.IsActive == true).FirstOrDefault();
                                if (result2 != null)
                                {
                                    result2.RoleId = data.RoleId;
                                    result2.Password = data.Password;
                                    result2.UpdatedOn = DateTime.Now;
                                    result2.UpdatedBy = FirstName;
                                }
                                var j = db.SaveChanges();

                                TempData["SMessage"] = "Successfully Updated";
                                return RedirectToAction("ShowAllUsers");
                            }
                        }
                        else
                        {
                            TempData["EMessage"] = "Updation failed. Please try again.";
                            return RedirectToAction("ShowAllUsers");
                        }

                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the data.");
                }
            }
            // Registration failed, set an error message
            TempData["EMessage"] = "Updation failed. Please try again.";
            return RedirectToAction("ShowAllUsers");
        }
        //For deposite Operation
        public ActionResult DepositeOperation()
        {
            return View();
        }

        public ActionResult CheckAccountForDepositeOperation(AxisBank_tblAllAccount accNo)
        {
            if (ModelState.IsValid)
            {
                ForDeposite fd = new ForDeposite();
                try
                {
                    using (AxisBankDBEntities db = new AxisBankDBEntities())
                    {
                        var result = db.AxisBank_tblAllAccount.Where(x => x.AccountNo == accNo.AccountNo && x.IsActive == true).FirstOrDefault();


                        if (result != null)
                        {
                            // how to get accountNo, firstName, balance from AxisBank_tblAllAccount and AxisBank_tblBalance and it is of DepositeFor model type
                            fd.AccountNo = result.AccountNo;
                            fd.FirstName = result.FirstName;
                            return View(fd);
                        }
                        else
                        {
                            return View();
                        }

                    }

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return View();
        }


        public ActionResult DepositeAmountInAccount(ForDeposite de)
        {
            try
            {

                using (AxisBankDBEntities db = new AxisBankDBEntities())
                {
                    var result = db.AxisBank_tblAllAccount.Where(x => x.AccountNo == de.AccountNo && x.IsActive == true).FirstOrDefault();


                    if (result != null)
                    {
                        var data = db.AxisBank_tblBalance
                      .Where(x => x.AccountId == result.Id && x.IsActive == true)
                      .FirstOrDefault();
                        if (de.Balance <= 0)
                        {
                            TempData["NotDeposite"] = "Balance Cant not Be Negative Or Zero";
                            return View("DepositeOperation");
                        }
                        else
                        {
                            if (data != null)
                            {

                                // Add the deposited amount to the current balance
                                data.Balance += de.Balance;
                                // Update the balance in the database
                                db.Entry(data).State = EntityState.Modified;

                                data.UpdatedOn = DateTime.Now;
                                data.UpdatedBy = Session["FirstName"] as string;
                                db.SaveChanges();
                                TempData["Deposite"] = "Balance Deposited Successfully";
                                db.tblAccountStatement.Add(new tblAccountStatement
                                {

                                    Date = DateTime.Now,
                                    Description = "Deposite",
                                    Credit = de.Balance,
                                    Balance = db.AxisBank_tblBalance.Where(x => x.AccountId == result.Id).Select(x => x.Balance).FirstOrDefault(),
                                    AccountId = result.Id
                                });
                                var k = db.SaveChanges();

                                List<tblAccountStatement> model = new List<tblAccountStatement>();
                                model = db.tblAccountStatement.Where(x => x.AccountId == result.Id).ToList();


                                return View(model);

                            }

                        }
                    }


                    return View("DepositeAmountInAccount");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public ActionResult WithdrawOperation()
        {
            return View();
        }

        public ActionResult CheckAccountForWithdrawOperation(AxisBank_tblAllAccount accNo)
        {
            if (ModelState.IsValid)
            {
                ForDeposite fd = new ForDeposite();
                try
                {
                    using (AxisBankDBEntities db = new AxisBankDBEntities())
                    {
                        var result = db.AxisBank_tblAllAccount.Where(x => x.AccountNo == accNo.AccountNo && x.IsActive == true).FirstOrDefault();
                        if (result != null)
                        {
                            // how to get accountNo, firstName, balance from AxisBank_tblAllAccount and AxisBank_tblBalance and it is of DepositeFor model type
                            fd.AccountNo = result.AccountNo;
                            fd.FirstName = result.FirstName;
                            return View(fd);
                        }
                        else
                        {
                            return View();
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return View();
        }


        public ActionResult WithdrawAmountInAccount(ForDeposite de)
        {
            try
            {

                using (AxisBankDBEntities db = new AxisBankDBEntities())
                {
                    var result = db.AxisBank_tblAllAccount.Where(x => x.AccountNo == de.AccountNo && x.IsActive == true).FirstOrDefault();


                    if (result != null)
                    {
                        var data = db.AxisBank_tblBalance
                      .Where(x => x.AccountId == result.Id && x.IsActive == true)
                      .FirstOrDefault();
                        if (de.Balance <= 0)
                        {
                            TempData["Zero"] = "You Can not Withdraw Zero Balance";
                            return View("WithdrawOperation");
                        }
                        else if (de.Balance > data.Balance)
                        {
                            TempData["NotWithdraw"] = "Balance is Less";
                            return View("WithdrawOperation");
                        }
                        else
                        {
                            if (data != null)
                            {
                                data.Balance -= de.Balance;
                                // Update the balance in the database
                                db.Entry(data).State = EntityState.Modified;

                                data.UpdatedOn = DateTime.Now;
                                data.UpdatedBy = Session["FirstName"] as string;
                                db.SaveChanges();

                                db.tblAccountStatement.Add(new tblAccountStatement
                                {
                                    Date = DateTime.Now,
                                    Description = "Withdraw",
                                    Debit = de.Balance,
                                    Balance = db.AxisBank_tblBalance.Where(x => x.AccountId == result.Id).Select(x => x.Balance).FirstOrDefault(),
                                    AccountId = result.Id
                                });
                                var k = db.SaveChanges();

                                List<tblAccountStatement> model = new List<tblAccountStatement>();
                                model = db.tblAccountStatement.Where(x => x.AccountId == result.Id).ToList();

                                TempData["Withdraw"] = "Balance Withdrawl Successfully";
                                return View(model);

                            }

                        }
                    }


                    return View("WithdrawAmountInAccount");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public ActionResult TotalDeposite()
        {
            try
            {
                using (AxisBankDBEntities db = new AxisBankDBEntities())
                {
                    var result = db.tblAccountStatement.Where(x => x.Credit != null).ToList();
                    return View(result);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        public ActionResult TotalWithdraw()
        {
            try
            {
                using (AxisBankDBEntities db = new AxisBankDBEntities())
                {
                    var result = db.tblAccountStatement.Where(x => x.Debit != null).ToList();
                    return View(result);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }       
    }
}