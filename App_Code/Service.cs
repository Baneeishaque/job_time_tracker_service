using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Service : System.Web.Services.WebService
{
    public Service()
    {
    }

    [WebMethod]
    public string InsertJob(string equipment, string jobNumber, string itemNumber, string coreNumber, string coreTotal, string phase, string status)
    {
        DBhandler db = new DBhandler();
        return db.SingleInsert("INSERT INTO equipment_scrum(equipment, job, item, core, core_total, phase, status) VALUES('" + equipment + "', " + jobNumber + ", " + itemNumber + ", " + coreNumber + ", " + coreTotal + ", '" + phase + "', '" + status + "')");
    }
}
