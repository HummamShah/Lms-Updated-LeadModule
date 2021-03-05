
'use strict';
app.controller('baseCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",
        "moment",
        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster, moment) {
            //Url Parameter Start
            $scope.GetUrlParameter = function (param) {
                const queryString = window.location.search;
                const urlParams = new URLSearchParams(queryString);
                return urlParams.get(param);
            }
            //Url Parameter End
            
            //AjaxServices Start
            $scope.IsServiceRunning = false;
            $scope.ServiceClassBinder = "LoaderDeActivate"; // bydefualt loader is deactivated
            $scope.TotalNumberOfServicesRunning = 0;
            $scope.AjaxGet = function (link, data) {
                $scope.ServiceClassBinder = "LoaderActivate"; // Loader class Activated
                $scope.TotalNumberOfServicesRunning = $scope.TotalNumberOfServicesRunning + 1;
                var promise = $http.get(link, { params: data, headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        $scope.TotalNumberOfServicesRunning = $scope.TotalNumberOfServicesRunning - 1;
                        $scope.ServiceClassBinder = "LoaderDeActivate"; // Loader class DeActivated
                        console.log(response);

                    }, function (resp) {
                        console.log(resp);
                        toaster.pop('error', "error", resp.status + "! Internal Error");
                        $scope.TotalNumberOfServicesRunning = $scope.TotalNumberOfServicesRunning - 1;
                        $scope.ServiceClassBinder = "LoaderDeActivate"; // Loader class DeActivated
                    }
                );
                return promise;
            }
            $scope.AjaxGetBackground = function (link, data) {
                var promise = $http.get(link, { params: data, headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        //success

                    }, function (resp) {
                        //error
                    }
                );
                return promise;
            }
            $scope.AjaxPost = function (link, data) {
                $scope.ServiceClassBinder = "LoaderActivate"; // Loader class Activated
                $scope.TotalNumberOfServicesRunning = $scope.TotalNumberOfServicesRunning + 1;
                var promise = $http.post(link, data, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {

                        $scope.TotalNumberOfServicesRunning = $scope.TotalNumberOfServicesRunning - 1;
                        $scope.ServiceClassBinder = "LoaderDeActivate"; // Loader class DeActivated
                        console.log(response);

                    }, function (resp) {
                        console.log(resp);
                        toaster.pop('error', "error", resp.status + "! Internal Error");
                        $scope.TotalNumberOfServicesRunning = $scope.TotalNumberOfServicesRunning - 1;
                        $scope.ServiceClassBinder = "LoaderDeActivate"; // Loader class DeActivated
                    }
                );
                return promise;
            }
            //AjaxServices End




            // Get Notifications Start
            $scope.Notifications = {};
            $scope.Notifications.TotalNotification = 0;
            $timeout(function () {
                getNotificaions();
            }, 30000);
            getNotificaions();
            function getNotificaions() {
                $scope.AjaxGetBackground("/api/NotificationApi/GetTopFiveNotifications", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Notifications = response.data;
                    });
            }
            //Get Notifications End


            //Listing Code Start
            $scope.ListingApiUrl = "";
            $scope.SetApiUrlForListing = function (ListingApiUrl) {
                $scope.ListingApiUrl = ListingApiUrl;
            }
            $scope.ListingOptions = {
                CurrentPage:1,
                PageSize: 100,
                TotalRecords: 20,
                Url: ''
            }
            $scope.NextPage = function () {
                $scope.ListingOptions.CurrentPage = $scope.ListingOptions.CurrentPage + 1;
                $scope.ResetList($scope.ListingOptions);
            }
            $scope.PreviousPage = function () {
                $scope.ListingOptions.CurrentPage = $scope.ListingOptions.CurrentPage - 1;
                $scope.ResetList($scope.ListingOptions);
            }
            $scope.ResetList = function (data) {
                $scope.AjaxGet(data.Url, data).then(
                    function (response) {
                        console.log(response);
                        $scope.ListingData = response.data.Data;
                        $scope.ListingOptions.TotalRecords = response.data.TotalRecords;
                    });
            }

           
            $scope.InitListing = function () {
                $scope.AjaxGet($scope.ListingOptions.Url, $scope.ListingOptions).then(
                    function (response) {
                        console.log(response);
                        $scope.ListingData = response.data.Data;
                        $scope.ListingOptions.TotalRecords = response.data.TotalRecords;
                    });
            }


              //Listing Code End

      
           

            //Dashboard
            $scope.initDashboard = function () {
                $scope.AjaxGet("/api/DashboardApi/GetUserReportData",null).then(
                    function (response) {
                        console.log(response);
                        $scope.UserReportData = response.data;
                        $scope.data = [
                            response.data.MonthlyLeadsData.MonthlyOpenLeadsData, //open
                            response.data.MonthlyLeadsData.MonthlyCompletedLeadsData, //completed
                            response.data.MonthlyLeadsData.MonthlyCancelledLeadsData, //Cancelled

                        ];
                    }
                );

            
                $scope.labels = ['Jan', 'Feb', 'March', 'April', 'May', 'June', 'July', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                $scope.series = ['Open', 'Completed', 'Closed'];
                Chart.defaults.global.colors = ["#00b7a6", "#5cb85c", "#d9534f"];
                //$scope.data = [
                //    [65, 59, 80, 81, 56, 55, 40, 56, 76, 23, 89, 66], //open
                //    [28, 48, 40, 19, 86, 27, 90, 34, 55, 29, 62, 30], //completed
                //    [34, 23, 76, 77, 20, 16, 43, 78, 12, 92, 45, 66], //closed

                //];

               
            }



            //Lead Dropdwons
            $scope.BusinessSegmentationDropDown = [{ id: 0, Name: "ISPs" }, { id: 1, Name: "Distributors" }, { id: 2, Name: "SolutionIntegreator" }, { id: 3, Name: "SoftwareHouse" }, { id: 5, Name: "GOVTSector" }, { id: 6, Name: "FinacialInstitues" }, { id: 7, Name: "FMCG" }, { id: 8, Name: "Telcos" }, { id: 9, Name: "CallCenter_BPO" }, { id: 10, Name: "Pharmaceutical" }, { id: 11, Name: "Textile" }]
            $scope.CityDropDown = [{ id: 0, Name: "Karachi" }, { id: 1, Name: "Lahore" }, { id: 2, Name: "Sialkot" }, { id: 3, Name: "Faisalabad" }, { id: 4, Name: "Rawalpindi" }, { id: 5, Name: "Peshawar" }, { id: 6, Name: "SaiduSharif" }, { id: 7, Name: "Multan" }, { id: 8, Name: "Gujranwala" }, { id: 9, Name: "Islamabad" }, { id: 10, Name: "Quetta " }, { id: 11, Name: "Bahawalpur" }, { id: 12, Name: "Sargodha" }, { id: 13, Name: "Mirpur" }, { id: 14, Name: "Chiniot" }, { id: 15, Name: "Sukkur" }, { id: 16, Name: "Larkana " }, { id: 17, Name: "Shekhupura " }, { id: 18, Name: "Jhang " }, { id: 19, Name: "RahimyarKhan" }, { id: 20, Name: "Gujrat" }]
            $scope.DomainDropDown = [{ Id: 0, Name: "SolutionSet" }, { Id: 1, Name: "Connectivity" }]

            //Ticketing Dropdowns; Open,Inprogress,Resolved,FollowUp,OnHold,ReOpen,
            $scope.TicketStatusList = [{ Id: 0, Name: "Open" }, { Id: 1, Name: "Inprogress" }, { Id: 2, Name: "Resolved" }, { Id: 3, Name: "FollowUp" }, { Id: 4, Name: "OnHold" }, { Id: 5, Name: "ReOpen" }];
            $scope.MediaTypes = ["LTE", "JAZZ_PMP", "Radio", "Fiber", "Zong", "Wimax"];
            $scope.MediumsOfComplaint = ["Hot Line", "E-mail", "Alert", "KAM"];
            $scope.TicketProblems = ["Fiber Damaged", "BTS Flap", "BTS Outage", "CPE Faulty", "Low Stats", "Wi-Fi Issue", "Packet Loss", "Connector Faulty", "IP Issue", "SMTP Issue", "POE/Adaptor Faulty", "Configuration Issue", "Interference Issue", "Coverage Issue", "Slow Browsing", "LAN Cable Unplugged", "Connector Broken", "BTS Congested", "Cable Faulty", "Device Reboot", "Equipment Faulty", "Port Issue", "Latency Issue", "Power Issue", "Bandwidth Choked", "Billing/Limit Issue", "Service Request", "Deployment", "Info Required", "Status Required", "Outgoing blocked", "Link found Operational", "Backend Configuration", "Testing", "Relocation"];
            $scope.TicketStatus = [{ Id: 0, Name: "Pending" }, { Id: 1, Name: "Resolved" }, { Id: 2, Name: "VisitScheduled" }, { Id: 3, Name: "OnHold" }];
            $scope.TicketRegions = ["Karachi", "Lahore", "Islamabad", "Hyderabad", "Sukkur", "Multan", "Peshawar", "Quetta", "Gujranwala", "Gujrat", "Sialkot", "Faisalabad", "Rahim Yar Khan", "Sargodha"];
            $scope.TicketOwnerShips = ["GCS", "Fariya", "Satcom", "Moblink", "Cybernet", "Brain Net", "Supernet", "Cactus", "Wasila", "Nexlinx", "Wateen", "Client", "Nayatel", "Vision Telecom", "ConnectTel", "Multinet", "Telecard", "Galaxy", "FiberCom", "Witribe", "SharpTel", "Connect", "FOP"];
            $scope.VisitCalls = ["On Call", "Faraz KHI", "Najeeb KHI", "Yousuf KHI", "Hasnain KHI", "Dawood PSW", "Jawad LHR", "Humayon LHR", "Ali Haider SLKT/GNW", "Azhar ISB", "Laraib HYD", "Noman MLT", "Waseem QTA", "Ejaz FSD"];

            //Toasters test
            $scope.Pop = function () {
                toaster.pop('success', "success", "Done Successfully");
                toaster.pop('error', "error", "Error in task");
                toaster.pop('warning', "warning", "this is Warning");
                toaster.pop('note', "note", "thhis is note");
            }

            //date moment test
            $scope.date = new moment().format("YYYY/MMM/DD");
            var date = new Date()
            console.log("Connected to lms App base ctrl " + moment(date).format("YYYY-MMM-DD"));

            $scope.GetDatePostFormat = function (date) {
                return moment(date).format("YYYY-MMM-DD")
            }
            $scope.MarkAgentSignOff = function () {
                $scope.AjaxPost("/api/AgentAttendanceApi/SignOffAttendance", {}).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Sign Off Time Has been marked!");
                            
                        } else {
                            //alert("Could Not Assign User");
                            toaster.pop('error', "error", "Could not Sign Off");
                        }
                    });
            }
        }
    ]);
