app.controller('TicketCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",

        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to TicketCtrl App");
            $scope.initIndex = function () {
                $scope.AjaxGet("/api/TicketApi/GetListData", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Tickets = response.data.Data;
                    });
            }
            $scope.AddInit = function () {
                $scope.Ticket = {};
                var AccountId = $scope.GetUrlParameter("Id");
                if (AccountId == 0 || AccountId == null) {
                    $scope.IsAccountSelected = false;
                } else {
                    $scope.IsAccountSelected = true;
                    $scope.Ticket.AccountId = AccountId;
                }
                getVendors();
                getShiftEngineers();
                GetDepartments();
                GetCompanyAccounts();
            }
          function  getShiftEngineers(){
                $scope.AjaxGet("/api/UserApi/GetAgentsForAssignment", { Type: "SD" }).then(
                    function (response) {
                        console.log(response);
                        $scope.ShiftEngineers = response.data.Data;
                    });
            }
            function GetDepartments() {
                $scope.AjaxGet("/api/DepartmentApi/GetDepartmentsDropdown", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Departments = response.data.Data;
                    });
            }
            function GetCompanyAccounts() {
                $scope.AjaxGet("/api/CompanyAccountsApi/GetCompanyAccountsDropdown", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Accounts = response.data.Data;
                        if ($scope.IsAccountSelected) {
                            var Account = {};
                            angular.forEach($scope.Accounts, function (value, key) {
                                if (value.AccountId == $scope.Ticket.AccountId) {
                                    Account = value;
                                    return;
                                }
                            }); 
                            $scope.AccountId = Account;
                        }
                    });
            }
            $scope.GetShiftEngineerData = function (Engineer) {
                $scope.Ticket.ShiftEngineerAgentId = Engineer.Id;
            }
            $scope.AddTicket= function (Ticket) {
                console.log(Ticket);
                if (Ticket.AccountId == null || Ticket.AccountId == 0) {
                    toaster.pop('error', "error", "Please Select Account!");
                    return;
                }
                $scope.AjaxPost("/api/TicketApi/AddTicket", Ticket).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Ticket Has Been Added Successfully");
                            var link = '/Ticket/';
                            $timeout(function () { window.location.href = link; }, 2000);
                        } else {
                            //alert("Could Not Add new Lead");
                            toaster.pop('error', "error", "Could not add Ticket!");
                        }
                    });

            }
            $scope.EditInit = function () {
                var Id = $scope.GetUrlParameter("Id");
                var data = {
                    Id: parseInt(Id)
                }
                $scope.IsAccountSelected = false;
                GetCompanyAccounts();
                getVendors();
                getShiftEngineers();
                GetDepartments();
              
                console.log(data);
                $scope.Ticket = {};
                $scope.TicketComments = [];
                $scope.AjaxGet("/api/TicketApi/GetTicket", data).then(
                    function (response) {
                        console.log(response);
                        $scope.Ticket = response.data;
                        $scope.TicketComments = response.data.TicketComments;
                        $scope.Ticket.EscalationDateTime = new Date($scope.Ticket.EscalationDateTime);
                        $scope.Ticket.RestorationDateTime = new Date($scope.Ticket.RestorationDateTime);
                        var Account = {};
                        angular.forEach($scope.Accounts, function (value, key) {
                            if (value.AccountId == $scope.Ticket.AccountId) {
                                Account = value;
                                return;
                            }

                        });
                        $scope.AccountId = Account;
                        var ShiftEngineerAgent = {};
                        angular.forEach($scope.ShiftEngineers, function (value, key) {
                            if (value.Id == $scope.Ticket.ShiftEngineerAgentId) {
                                ShiftEngineerAgent = value;
                                return;
                            }

                        });
                        $scope.ShiftEngineer = ShiftEngineerAgent;
                    });
            }
            $scope.GetAccountData = function (Account) {
                console.log(Account);
                $scope.Ticket.AccountId = Account.AccountId;
            }
            $scope.AddComment = function (Comment) {
                var commentReq = {
                    Id: 0,
                    Comment: Comment,
                    TicketId: $scope.Ticket.Id,
                }
                $scope.AjaxPost("/api/TicketApi/AddTicketComments", commentReq).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Comment Has Been Added Successfully");
                            var link = '/Ticket/';
                            $timeout(function () { window.location.reload(); }, 2000);
                        } else {
                            //alert("Could Not Add new Lead");
                            toaster.pop('error', "error", "Could not add Comment!");
                        }
                    });
                
            }
            getVendors = function () {
                $scope.AjaxGet("/api/VendorApi/GetVendors", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Vendors = response.data.Data;
                    });
            }
            $scope.EditTicket = function (Ticket) {
                console.log(Ticket);
                $scope.AjaxPost("/api/TicketApi/EditTicket", Ticket).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Ticket Has Been Updated Successfully");
                            var link = '/Ticket/';
                            $timeout(function () { window.location.href = link; }, 2000);
                        } else {
                            //alert("Could Not Add new Lead");
                            toaster.pop('error', "error", "Could not Update Ticket!");
                        }
                    });
            }
            $scope.ChangeStatus = function (Status, Ticket) {
                console.log(Status + "  " + Ticket.RFO + " " + Ticket.Id);
                var request = {
                    Status: Status,
                    RFO: Ticket.RFO,
                    Id:Ticket.Id
                }
                console.log(request);
                if (Status == 4 || Status == '4') {
                    if (Ticket.RFO == "" || Ticket.RFO == null) {
                        toaster.pop('error', "error", "Please Enter RFO!");
                        return;
                    }
                }
                $scope.AjaxPost("/api/TicketApi/ChangeTicketStatus", request).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Ticket Status Has Been Updated Successfully");
                            $timeout(function () { window.location.reload(); }, 2000);
                        } else {
                            //alert("Could Not Add new Lead");
                            toaster.pop('error', "error", "Could not Update Ticket!");
                        }
                    });
            }

        }
    ]);