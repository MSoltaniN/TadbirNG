
export class EnviromentComponent {



    constructor()
    {

    }

   
    public get FiscalPeriodId(): number {

        var fpId = 0;

        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            fpId = currentContext ? parseInt(currentContext.fpId) : 0;

        }
        return fpId;
    }

    public get BranchId(): number {

        var branchId = 0;

        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            branchId = currentContext ? parseInt(currentContext.branchId) : 0;

        }
        return branchId;
    }

    public get CompanyId(): number {

        var companyId = 0;

        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            companyId = currentContext ? parseInt(currentContext.companyId) : 0;

        }
        return companyId;
    }

    public get Ticket(): string {

        var ticket = '';

        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            ticket = currentContext ? currentContext.ticket.toString() : '';

        }
        return ticket;
    }

    public get UserId(): number {

        var userId = 0;

        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            var jsonContext = atob(currentContext.ticket);
            var context = JSON.parse(jsonContext);
            
            userId = currentContext ? parseInt(context.User.Id) : 0;

        }

        return userId;
    }

    public get UserName(): string {

        var userName = '';

        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            userName = currentContext ? currentContext.userName.toString() : '';

        }
        return userName;
    }

}