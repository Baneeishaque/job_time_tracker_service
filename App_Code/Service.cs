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
    public string InsertJob(string equipment, string employeeID, string jobNumber, string itemNumber, string coreNumber, string coreTotal, string phase)
    {
        DBhandler db = new DBhandler();

        string dbresult = db.SingleInsertUpdate("INSERT INTO ScrumTable(Job, Item, CoreTotal, ToDo, Doing, Done)VALUES('" + jobNumber + "','" + itemNumber + "'," + coreTotal + ",0,0,0)");
        if (dbresult == "1")
        {
            //TODO : Handle Department table

            dbresult = db.SingleInsertUpdate("INSERT INTO Equipment_Scrum([Equipment No_], Job, Item, Core, CoreTotal, Phases, Status) VALUES('" + equipment + "', '" + jobNumber + "', '" + itemNumber + "', " + coreNumber + ", " + coreTotal + ", '" + phase + "', 'Start')");
            if (dbresult == "1")
            {
                dbresult = db.SingleInsertUpdate("INSERT INTO WorkInProgress(No_, Job, Item, [Customer No_], [Customer Description], [Core Description], Core, CoreTotal, Location, Operation, [Badge Start], [Date Start], [Badge End], [Date End], [No Operator], [Weight for Unit kg], [Thickness mm], [Static mm], [Column 1 Measure mm], [Central Measure mm], [Column 2 Measure mm], [Actual Phases],[Department Code])VALUES('','" + jobNumber + "','" + itemNumber + "','','',''," + coreNumber + "," + coreTotal + ",'','','','" + DateTime.Now.ToString("yyyy-MM-dd") + "','','',0,0,0,0,0,0,0,'','')");
                if (dbresult == "1")
                {
                    dbresult = db.SingleInsertUpdate("INSERT INTO Time_Employee(PK_Time_Employee, FK_WorkInProgress_No_, ID_employee, Time_Start, Time_End)VALUES('','','" + employeeID + "','" + DateTime.Now.ToString("HH:mm:ss") + "','')");
                }
            }
        }
        return dbresult;
    }

    [WebMethod]
    public string UpdateJob(string equipment, string employeeID, string jobNumber)
    {
        DBhandler db = new DBhandler();

        //TODO : Handle Department table

        string dbresult = db.SingleInsertUpdate("UPDATE Equipment_Scrum SET Status = 'End' WHERE([Equipment No_] = '" + equipment + "')");
        if (dbresult == "1")
        {
            dbresult = db.SingleInsertUpdate("UPDATE WorkInProgress SET [Date End] = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' WHERE(Job = '" + jobNumber + "')");
            if (dbresult == "1")
            {
                dbresult = db.SingleInsertUpdate("UPDATE Time_Employee SET Time_End = '" + DateTime.Now.ToString("HH:mm:ss") + "' WHERE(ID_employee = '" + employeeID + "')");
            }
        }

        return dbresult;
    }
}
