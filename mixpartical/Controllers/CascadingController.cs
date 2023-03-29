using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using static mixpartical.Controllers.repo;

namespace mixpartical.Controllers
{
    public class CascadingController : Controller
    {
        // GET: Cascading
         private string[] persion = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
        private string[] english = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        public string convertDigits(string input, string srcCulture, string targetCulture)
        {
            var nfCurrentDigits = GetNumberSymbols(srcCulture).ToList();
            var nfTargetDigits = GetNumberSymbols(targetCulture).ToArray();
            var ret = new Regex("[" + string.Join("", nfCurrentDigits) + "]").Replace(input, m => nfTargetDigits[nfCurrentDigits.IndexOf(m.Value)]);
            return ret;
        }

        public string[] GetNumberSymbols(string srcCulture)
        {
            var nfCurrent = new CultureInfo(srcCulture).NumberFormat;
            var list = new List<string>();
            list.AddRange(nfCurrent.NativeDigits);
            list.Add(nfCurrent.CurrencyDecimalSeparator);
            list.Add(nfCurrent.CurrencyGroupSeparator);
            list.Add(nfCurrent.CurrencySymbol);
            return list.ToArray();
        }
        public ActionResult create()
        {

            string chash = "١٢٣٤٥٦٧٨٩";
            for (int i = 0; i < 10; i++)
                chash = chash.Replace(persion[i], english[i]);

          //  short da = Convert.ToInt16(chash);
            var sa = chash;

            var str = convertDigits("١٢٣٤٥٦٧٨٩", "en-US", "fa-IR");
            var str1 = convertDigits(str, "fa-IR", "en-US");
          var saa=  convertDigits(str, "en-US", "fa-IR");
             
            return View();
        }
        public JsonResult getallcountry()
        {
            return Json(new repo().getalldata(),    JsonRequestBehavior.AllowGet);
        }
        public JsonResult getallstate(int id)
        {
            return Json(new repo().getallstate(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getallcity(int id)
        {
            return Json(new repo().getallcity(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult save(coustomerinfo sa,string[] platfromname)
        {
            return Json(new repo().save(sa), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getdata()
        {
            return Json(new repo().getalltabledata(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult delete(int id)
        {
            return Json(new repo().deletedata(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult edit(int id)
        {
            var dat = new repo().editedata(id);
           
            return Json(new repo().editedata(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult home()
        {
            return View();
        }

    }
}