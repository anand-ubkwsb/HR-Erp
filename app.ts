/// <reference path="TypeScriptDefinitions/Telerik.Web.UI.d.ts" />
/// <reference path="TypeScriptDefinitions/microsoft.ajax.d.ts" />



/* 
 * This is sample handler that shows how to handle the NavigationCommand 
 * client event of RadScheduler in TypeScript
 */

function OnNavigationCommand(scheduler: Telerik.Web.UI.RadScheduler, e: Sys.EventArgs) {
    var appointment: Telerik.Web.UI.SchedulerAppointment = new Telerik.Web.UI.SchedulerAppointment();
    var start: Date = new Date();
    var end: Date = new Date();

    end.setHours(start.getHours() + 1);

    appointment.set_subject("sample appointment");
    appointment.set_start(start);
    appointment.set_end(end);

    scheduler.get_appointments().add(appointment);
}