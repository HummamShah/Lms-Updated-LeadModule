var app = angular.module('LmsApp', ['toaster']);

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
        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to lms App base ctrl");
            $scope.Pop = function () {
                toaster.pop('success', "success", "Done Successfully");
                toaster.pop('error', "error", "Error in task");
                toaster.pop('warning', "warning", "this is Warning");
                toaster.pop('note', "note", "thhis is note");
            }
            $scope.BusinessSegmentationDropDown = [{ id: 0, Name: "ISPs" }, { id: 1, Name: "Distributors" }, { id: 2, Name: "SolutionIntegreator" }, { id: 3, Name: "SoftwareHouse" }, { id: 5, Name: "GOVTSector" }, { id: 6, Name: "FinacialInstitues" }, { id: 7, Name: "FMCG" }, { id: 8, Name: "Telcos" }, { id: 9, Name: "CallCenter_BPO" }, { id: 10, Name: "Pharmaceutical" }, { id: 11, Name: "Textile" }]
            $scope.CityDropDown = [{ id: 0, Name: "Karachi" }, { id: 1, Name: "Lahore" }, { id: 2, Name: "Sialkot" }, { id: 3, Name: "Faisalabad" }, { id: 4, Name: "Rawalpindi" }, { id: 5, Name: "Peshawar" }, { id: 6, Name: "SaiduSharif" }, { id: 7, Name: "Multan" }, { id: 8, Name: "Gujranwala" }, { id: 9, Name: "Islamabad" }, { id: 10, Name: "Quetta " }, { id: 11, Name: "Bahawalpur" }, { id: 12, Name: "Sargodha" }, { id: 13, Name: "Mirpur" }, { id: 14, Name: "Chiniot" }, { id: 15, Name: "Sukkur" }, { id: 16, Name: "Larkana " }, { id: 17, Name: "Shekhupura " }, { id: 18, Name: "Jhang " }, { id: 19, Name: "RahimyarKhan" }, { id: 20, Name: "Gujrat" }]
            $scope.DomainDropDown = [{ Id: 0, Name: "SolutionSet" }, { Id: 1, Name: "Connectivity" }]
            $scope.GetUrlParameter = function (param) {
                const queryString = window.location.search;
                const urlParams = new URLSearchParams(queryString);
                return urlParams.get(param);
            }
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
                      
                    }
                );
                return promise;
            }
            $scope.AjaxGetBackground = function (link, data) {
                var promise = $http.get(link, { params: data, headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                       

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
                        $scope.ServiceClassBinder = "LoaderActivate"; // Loader class DeActivated
                        console.log(response);

                    }
                );
                return promise;
            }
            

        }
    ]);



app.controller('UserCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",
        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to User App");
            //$scope.TryingAjaxService = function () {
            //    $scope.AjaxGet("/api/UserApi/GetListData", null).then(
            //        function (response) {
            //            console.log(response);
            //        })
            //}
            $scope.initIndex = function () {
                $scope.AjaxGet("/api/UserApi/GetListData", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Users = response.data.Data;
                    });
            }
            $scope.AddUser = function (user) {
                console.log(user);
                if (user.DepartmentId == null || user.DepartmentId == 0) {
                    //alert("Please Select Department");
                    toaster.pop('error', "error", "Please Select Department");
                    return;
                }
                if (user.FirstName == null || user.FirstName == "") {
                    //alert("Please Enter First Name");
                    toaster.pop('error', "error", "Please Enter First Name");
                    return;
                }
                if (user.Email == null || user.Email == "") {
                    //alert("Please Enter Email!");
                    toaster.pop('error', "error", "Please Enter Email!");
                    return;
                }
                if (user.Contact1 == null) {
                    //alert("Please Enter Primary Contact");
                    toaster.pop('error', "error", "Please Enter Primary Contact!");
                    return;
                }
                if (user.Password == null || user.Password == "") {
                    //alert("Enter Password");
                    toaster.pop('error', "error", "Please Enter Password!");
                    return;
                }
                if (user.Password != user.ConfirmPassword) {
                    //alert("Password and Confirm Password shoul match");
                    toaster.pop('error', "error", "Password and Confirm Password should match!");
                    return;
                }
                if (user.HasSupervisor == true) {
                    if (user.SupervisorId == null) {
                        toaster.pop('error', "error", "Please Select Supervisor");
                        return;
                    }
                }
                //Loader need to make it generic so we could use this in a function
                $scope.IsServiceRunning = true;
                //var promise = $http.post("/api/UserApi/RegisterUser", user, {headers: { 'Accept': 'application/json' } });
                //promise.then(
                //    function (response) {
                //        $scope.IsServiceRunning = false;
                //        console.log(response);
                //        if (response.status == 200) {
                //            //alert("User has been Added Successfully!");
                //            toaster.pop('success', "success", "Password and Confirm Password shoul match!");
                //            $timeout(function () { window.location.href = '/User'; }, 2000);
                //        } else {
                //            toaster.pop('error', "error", "Could Not Add User!");
                //        }
                        
                //    });
                $scope.AjaxPost("/api/UserApi/RegisterUser", user).then(
                    function (response) {
                        if (response.status == 200) {
                            //alert("User has been Added Successfully!");
                            toaster.pop('success', "success", "User Has Been Added!");
                            $timeout(function () { window.location.href = '/User'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Add User!");
                        }
                    });

            }
            $scope.GetUsersByDepartmentId = function (DepartmentId) {
                if (DepartmentId != null) {
                    var data = {
                       Id: DepartmentId
                    }
                    $scope.AjaxGet("/api/UserApi/GetUsersByDepartment", data).then(
                        function (response) {
                            console.log(response);
                            $scope.Supervisors = response.data.Data;
                        });
                }
            }
            $scope.CheckForDepartment = function (Id) {
                if (Id == null) {
                    //alert("Please Select Department First");
                    toaster.pop('error', "error", "Please Select Department First");
                    $scope.User.HasSupervisor = false;
                }
            }
            $scope.AddAdmin = function (user) {
                console.log(user);
                //var promise = $http.post("/api/UserApi/RegisterAdmin", user, { headers: { 'Accept': 'application/json' } });
                //promise.then(
                //    function (response) {
                //        console.log(response);
                //        if (response.status == 200) {
                //            alert("Admin has been added Successfully!");
                //            $timeout(function () { window.location.href = '/User'; }, 2000);
                //        } else {
                //            alert("Could Not Add new admin");
                //        }

                //    });

                $scope.AjaxPost("/api/UserApi/RegisterAdmin", user).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Admin Has Been Added Successfully");
                            $timeout(function () { window.location.href = '/User'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Add Admin!");
                        }
                    });

            }
            $scope.AddAdminInit = function () {
                $scope.Admin = {};
            }
            $scope.AddInit = function () {
                $scope.User = {};
                $scope.AjaxGet("/api/DepartmentApi/GetDepartmentsDropdown", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Departments = response.data.Data;
                    });
            }
        }
    ]);

app.controller('VendorCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",
        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to Vendor App");
            $scope.initIndex = function () {
                var promise = $http.get("/api/VendorApi/GetListData", { params: null, headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        $scope.Vendors = response.data.Data;
                    });
                // $scope.Inventories = $http.get("/api/InvenoryApi/GetAllData", { params: data, headers: { 'Accept': 'application/json' } });
            }
            $scope.AddInit = function () {
                $scope.Vendor = {};
            }
            $scope.AddVendor = function (Vendor) {
                console.log(Vendor);
                if (Vendor.Name == null || Vendor.Name == "") {
                    alert("Name Is Required");
                    return;
                }
                var promise = $http.post("/api/VendorApi/AddVendor", Vendor, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        if (response.status == 200) {
                            alert("New Vendor Added Successfully!");
                            //window.open('/Vendor');
                            window.location = '/Vendor';
                            $timeout(function () { window.location.href = '/Vendor';}, 2000);
                        }
                        
                    });
            }
           
        }
    ]);

app.controller('CompanyCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",
        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to Company App");
            $scope.initIndex = function () {
                //var promise = $http.get("/api/CompanyApi/GetListData", { params: null, headers: { 'Accept': 'application/json' } });
                //promise.then(
                //    function (response) {
                //        console.log(response);
                //        $scope.Companies = response.data.Data;
                //    });
                $scope.AjaxGet("/api/CompanyApi/GetListData", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Companies = response.data.Data;
                    });
            }

          

            $scope.AddInit = function () {
                $scope.Company = {};
                $scope.Companies = [];
                $scope.MOCS = [{ Name: "Phone", Id: 0 }, { Name: "Email", Id: 1 }, { Name: "Fax", Id: 2 }, { Name: "Visit", Id: 3 }];
                getCompaniesDropdown();
            }
            getCompaniesDropdown =function (){
                //var promise = $http.get("/api/CompanyApi/GetCompaniesDropdown", { params: null, headers: { 'Accept': 'application/json' } });
                //promise.then(
                //    function (response) {
                //        console.log(response);
                //        $scope.Companies = response.data.Data;
                //    });
                $scope.AjaxGet("/api/CompanyApi/GetCompaniesDropdown", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Companies = response.data.Data;
                    });
            }


            $scope.WiWax = [{ Name: "WiWax1", Id: 0 }, { Name: "WiWax2", Id: 1 }, { Name: "WiWax3", Id: 2 }];
            $scope.DSL = [{ Name: "DSL1", Id: 0 }, { Name: "DSL2", Id: 1 }, { Name: "DSL3", Id: 2 }];
            $scope.VSAT = [{ Name: "VSAT1", Id: 0 }, { Name: "VSAT2", Id: 1 }, { Name: "VSAT3", Id: 3 }];
            $scope.Fiber = [{ Name: "Fiber", Id: 0 }, { Name: "Fiber1", Id: 1 }, { Name: "Fiber2", Id: 2 }];
            $scope.GetDropdownForServies = function (param) {
                //0-dsl,1=Vsat,2=Wiwax,3=Fiber,4-other
                if (param == 0 || param == '0') {
                    $scope.CurrentServices = $scope.DSL;
                }
                if (param == 1 || param == '1') {
                    $scope.CurrentServices = $scope.VSAT;
                }
                if (param == 2 || param == '2') {
                    $scope.CurrentServices = $scope.WiWax;
                }
                if (param == 3 || param == '3') {
                    $scope.CurrentServices = $scope.Fiber;
                }

            }



            $scope.EditInit = function () {
                $scope.Company = {};
                $scope.Companies = [];
                
                getCompaniesDropdown();
                $scope.MOCS = [{ Name: "Phone", Id: 0 }, { Name: "Email", Id: 1 }, { Name: "Fax", Id: 2 }, { Name: "Visit", Id: 3 }];
                var Id = $scope.GetUrlParameter("Id");
                var data = {
                    Id: parseInt(Id)
                }
                console.log(data);
                //var promise = $http.get("/api/CompanyApi/GetCompany", { params:data} ,{  headers: { 'Accept': 'application/json' } });
                //promise.then(
                //    function (response) {
                //        console.log(response);
                //        $scope.Company = response.data;
                //        $scope.GetDropdownForServies($scope.Company.CUDS);
                //        $scope.ParentCompanyId = $scope.Company.ParentCompanyId;
                //        console.log($scope.Company);
                //    });
                $scope.AjaxGet("/api/CompanyApi/GetCompany", data).then(
                    function (response) {
                        console.log(response);
                        $scope.Company = response.data;
                        $scope.GetDropdownForServies($scope.Company.CUDS);
                        $scope.ParentCompanyId = $scope.Company.ParentCompanyId;
                        console.log($scope.Company);
                    });


            }
            $scope.GetThisConsole = function (id) {
                console.log(id);
                $scope.Company.ParentCompanyId = id;
            }
            $scope.AddCompany = function (Company) {
                console.log(Company);

 
                if (Company.Name == null || Company.Name == "") {
                    //alert("Name Is Required");
                    toaster.pop('error', "error", "Name Is Required!");
                    return;
                }
                if (Company.Address == null || Company.Address == "") {
                    //alert("Address Is Required");
                    toaster.pop('error', "error", "Address Is Required!");
                    return;
                }
                if (Company.Contact == null || Company.Contact == "") {
                    //alert("Contact Is Required");
                    toaster.pop('error', "error", "Contact Is Required!");
                    return;
                }
                if (Company.Latitude == null)
                {
                    toaster.pop('error', "error", "Latitide is required");
                    return;
                }
                if (Company.Longitude == null) {
                    toaster.pop('error', "error", "Longitude is required");
                    return;
                }

                console.log(Company);
        //TODOPOST
                var promise = $http.post("/api/CompanyApi/AddCompany", Company, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        if (response.status == 200) {
                            //alert("New Company Added Successfully!");
                            toaster.pop('success', "success", "Company Added Successfully");
                            $timeout(function () { window.location.href = '/Company'; }, 2000);
                        }

                    });
            }

            $scope.EditCompany = function (Company) {
                console.log(Company);
                //return;
                if (Company.Name == null || Company.Name == "") {
                    //alert("Name Is Required");
                    toaster.pop('error', "error", "Name Is Required!");
                    return;
                }
                if (Company.Address == null || Company.Address == "") {
                    //alert("Address Is Required");
                    toaster.pop('error', "error", "Address Is Required!");
                    return;
                }
                if (Company.Contact == null || Company.Contact == "") {
                    //alert("Contact Is Required");
                    toaster.pop('error', "error", "Contact Is Required!");
                    return;
                }
                if (Company.Latitude == null) {
                    toaster.pop('error', "error", "Latitide is required");
                    return;
                }
                if (Company.Longitude == null) {
                    toaster.pop('error', "error", "Longitude is required");
                    return;
                }
                var promise = $http.post("/api/CompanyApi/EditCompany", Company, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        if (response.status == 200) {
                            //alert("Company has been updated Successfully!");
                            toaster.pop('success', "success", "Company has been updated successfully");
                            $timeout(function () { window.location.href = '/Company'; }, 2000);
                        }

                    });
            }

        }
    ]);

app.controller('DepartmentCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",
        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to Department App");
            $scope.initIndex = function () {
                var promise = $http.get("/api/DepartmentApi/GetListData", { params: null, headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        $scope.Departments = response.data.Data;
                    });
            }
            $scope.AddInit = function () {
                $scope.Department = {};
            }
            $scope.EditInit = function () {
                $scope.Company = {};
                var Id = $scope.GetUrlParameter("Id");
                var data = {
                    Id: parseInt(Id)
                }
                console.log(data);
                var promise = $http.get("/api/DepartmentApi/GetDepartment", { params: data }, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        $scope.Department = response.data;
                    });

            }
            $scope.AddDepartment = function (Department) {
                console.log(Department);
                if (Department.Name == null || Department.Name == "") {
                    //alert("Name Is Required");
                    toaster.pop('error', "error", "Name Is Required!");
                    return;
                }
               
                var promise = $http.post("/api/DepartmentApi/AddDepartment", Department, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        if (response.status == 200) {
                            //alert("New Department Added Successfully!");
                            toaster.pop('success', "success", "New Department added Successfully!");
                            $timeout(function () { window.location.href = '/Department'; }, 2000);
                        }
                        else {
                            //alert("Could not add new department");
                            toaster.pop('error', "error", "Could not add successfully!");
                        }

                    });
            }

            $scope.EditDepartment = function (Department) {
                console.log(Department);
                if (Department.Name == null || Department.Name == "") {
                    //alert("Name Is Required");
                    toaster.pop('error', "error", "Name Is Required!");
                    return;
                }
               
                var promise = $http.post("/api/DepartmentApi/EditDepartment", Department, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        if (response.status == 200) {
                            //alert("Department has been updated Successfully!");
                            toaster.pop('success', "success", "Department has been added succesfully");
                            $timeout(function () { window.location.href = '/Department'; }, 2000);
                        } else {
                            //alert("Could Not Update Department");
                            toaster.pop('error', "error", "Could not add department!");
                        }

                    });
            }

        }
    ]);

app.controller('LeadCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",
        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to Lead App");
            $scope.initIndex = function () {
                $scope.Assignment = {};
                $scope.AssignmentTypes = ["Lead","PMD","PreSale"];
                $scope.AjaxGet("/api/LeadApi/GetListData", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Leads = response.data.Data;
                    });

            }
            $scope.initPmdIndex = function () {
                $scope.Assignment = {};
                $scope.AssignmentTypes = ["Lead", "PMD", "PreSale"];
                $scope.AjaxGet("/api/LeadApi/GetListForPmd", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Leads = response.data.Data;
                    });

            }
            $scope.SetAssigningDataToModal = function (Lead) {
                console.log(Lead);
                $scope.Assignment.Id = Lead.Id;
                $scope.Assignment.Header = "Assign A User";
               
            }
            $scope.SetAssigningType = function (Type) {
                $scope.AjaxGet("/api/UserApi/GetAgentsForAssignment", { Type: Type }).then(
                    function (response) {
                        console.log(response);
                        $scope.AssignmentAgents = response.data.Data;
                    });
            }
            $scope.AssignUser = function (Assignment) {
                console.log(Assignment);
                if (Assignment.Type == null) {
                    toaster.pop('error', "error", "Select Type Of User");
                    return;
                }
                if (Assignment.AssignedUserId == null) {
                    toaster.pop('error', "error", "Select User To Assign");
                    return;
                }
                $scope.AjaxPost("/api/LeadApi/AssignLead", Assignment).then(
                    function (response) {
                        if (response.status == 200) {
                            //alert("User Has Been Assigned Successfully!");
                            toaster.pop('success', "success", "User Has Been Assigned Successfully!");
                            $timeout(function () { window.location.href = '/Lead'; }, 2000);
                        } else {
                            //alert("Could Not Assign User");
                            toaster.pop('error', "error", "Could not Assign User");
                        }
                    });
            }
            $scope.AddLead = function (Lead) {
                console.log(Lead);

                if (Lead.CompanyId == null) {
                    toaster.pop('error', "error", "Please Select Company!");
                    return;
                }
                if (Lead.Domain == null) {
                    toaster.pop('error', "error", "Please Select Domain!");
                    return;
                }
                if (Lead.ContactPersonName == null || Lead.ContactPersonName == "") {
                    toaster.pop('error', "error", "Please Enter Contact Person Name");
                    return;
                }
                if (Lead.ContactPersonNumber == null || Lead.ContactPersonNumber == "") {
                    toaster.pop('error', "error", "Please Enter Contact Person Number");
                    return;
                }
                if (Lead.ContactPersonEmail == null || Lead.ContactPersonEmail == "") {
                    toaster.pop('error', "error", "Please Enter Contact Person Email");
                    return;
                }
                if (Lead.ContactPersonTitle == null || Lead.ContactPersonTitle == "") {
                    toaster.pop('error', "error", "Please Enter Contact Person Title");
                    return;
                }
                if (Lead.ContactPersonDepartment == null || Lead.ContactPersonDepartment == "") {
                    toaster.pop('error', "error", "Please Enter Contact Person Department");
                    return;
                }
              
                $scope.AjaxPost("/api/LeadApi/AddLead", Lead).then(
                    function (response) {
                    if (response.status == 200) {
                       // alert("Lead has been Added Successfully!");
                        toaster.pop('success', "success", "Lead Has Been Added Successfully");
                        $timeout(function () { window.location.href = '/Lead'; }, 2000);
                    } else {
                        //alert("Could Not Add new Lead");
                        toaster.pop('error', "error", "Could not add lead!");
                    }
                });
            

            }
            getParentCompaniesDropdown = function () {
                var promise = $http.get("/api/CompanyApi/GetParentCompaniesDropdown", { params: null, headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        $scope.Companies = response.data.Data;
                    });
            }
            getCompaniesDropdown = function () {
                $scope.AjaxGet("/api/CompanyApi/GetCompaniesDropdown", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Companies = response.data.Data;
                    });

            }
            $scope.AddInit = function () {
                $scope.Company = {};
                $scope.Companies = [];
                $scope.Lead = {};
                //$scope.Lead.Domain = 0;
                $scope.MOCS = [{ Name: "Phone", Id: 0 }, { Name: "Email", Id: 1 }, { Name: "Fax", Id: 2 }, { Name: "Visit", Id: 3 }];
                getCompaniesDropdown();
            }
           
            $scope.WiWax = [{ Name: "WiWax1", Id: 0 }, { Name: "WiWax2", Id: 1 }, { Name: "WiWax3", Id: 2 }];
            $scope.DSL = [{ Name: "DSL1", Id: 0 }, { Name: "DSL2", Id: 1 }, { Name: "DSL3", Id: 2 }];
            $scope.VSAT = [{ Name: "VSAT1", Id: 0 }, { Name: "VSAT2", Id: 1 }, { Name: "VSAT3", Id: 3 }];
            $scope.Fiber = [{ Name: "Fiber", Id: 0 }, { Name: "Fiber1", Id: 1 }, { Name: "Fiber2", Id: 2 }];
            $scope.GetDropdownForServies = function (param) {
                //0-dsl,1=Vsat,2=Wiwax,3=Fiber,4-other
                if (param == 0) {
                    $scope.CurrentServices = $scope.DSL;
                }
                if (param == 1) {
                    $scope.CurrentServices = $scope.VSAT;
                }
                if (param == 2) {
                    $scope.CurrentServices = $scope.WiWax;
                }
                if (param == 3) {
                    $scope.CurrentServices = $scope.Fiber;
                }
               
            }
            $scope.AddNewCompany = function (Company) {
                //Create a request to add a new company and then refresh the list of company
                if (Company.Name == null || Company.Name == "") {
                    //alert("Name Is Required");
                    toaster.pop('error', "error", "Name Is Required!");
                    return;
                }
                if (Company.Address == null || Company.Address == "") {
                    //alert("Address Is Required");
                    toaster.pop('error', "error", "Address Is Required!");
                    return;
                }
                if (Company.Contact == null || Company.Contact == "") {
                    //alert("Contact Is Required");
                    toaster.pop('error', "error", "Contact Is Required!");
                    return;
                }
                console.log(Company);
                $scope.AjaxPost("/api/CompanyApi/AddCompany", Company).then(function (response) {
                    console.log(response);
                    if (response.status == 200) {
                        toaster.pop('success', "success", "Company Added Successfully");
                        //$scope.AjaxGetBackground().then(function (resp) {

                        //})
                        getCompaniesDropdown();
                    }
                });
                
            }
            $scope.GetCompanyData = function (Id) {
               
                var data = {
                    Id: Id
                }
                var promise = $http.get("/api/CompanyApi/GetCompany", { params: data }, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        $scope.Lead = response.data;
                        $scope.Lead.CompanyId = Id;
                        $scope.GetDropdownForServies($scope.Lead.CUDS);
                       
                    });
            }


            $scope.Editinit = function () {
                $scope.Company = {};
                $scope.Companies = [];
                $scope.Lead = {};
               // $scope.Lead.Domain = 0;
                var Id = $scope.GetUrlParameter("Id");
                var data = {
                    Id: parseInt(Id)
                }
                console.log(data);
                $scope.AjaxGet("/api/LeadApi/GetLead",  data ).then(
                    function (response) {
                        console.log(response);
                        $scope.Lead = response.data;
                        $scope.GetDropdownForServies($scope.Lead.CUDS);
                    });
             
                $scope.MOCS = [{ Name: "Phone", Id: 0 }, { Name: "Email", Id: 1 }, { Name: "Fax", Id: 2 }, { Name: "Visit", Id: 3 }];
                getCompaniesDropdown();
            }
            $scope.EditLead = function (Lead) {
                console.log(Lead);
                if (Lead.CompanyId == null) {
                    toaster.pop('error', "error", "Please Select Company!");
                    return;
                }
                if (Lead.Domain == null) {
                    toaster.pop('error', "error", "Please Select Domain!");
                    return;
                }
                if (Lead.ContactPersonName == null || Lead.ContactPersonName == "") {
                    toaster.pop('error', "error", "Please Enter Contact Person Name");
                    return;
                }
                if (Lead.ContactPersonNumber == null || Lead.ContactPersonNumber == "") {
                    toaster.pop('error', "error", "Please Enter Contact Person Number");
                    return;
                }
                if (Lead.ContactPersonEmail == null || Lead.ContactPersonEmail == "") {
                    toaster.pop('error', "error", "Please Enter Contact Person Email");
                    return;
                }
                if (Lead.ContactPersonTitle == null || Lead.ContactPersonTitle == "") {
                    toaster.pop('error', "error", "Please Enter Contact Person Title");
                    return;
                }
                if (Lead.ContactPersonDepartment == null || Lead.ContactPersonDepartment == "") {
                    toaster.pop('error', "error", "Please Enter Contact Person Department");
                    return;
                }

                $scope.AjaxPost("/api/LeadApi/EditLead", Lead).then(
                    function (response) {
                        if (response.status == 200) {
                            //alert("Lead has been Updated Successfully!");
                            toaster.pop('success', "success", "Lead Has Been Updated Successfully!");
                            $timeout(function () { window.location.href = '/Lead'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Unable to update lead!");
                        }
                    });
            }
            $scope.FeasibiltyInit = function () {
                $scope.Lead = {};
                $scope.LeadFeasibility = [];
                $scope.Vendors = [];
                $scope.FeasibilityStatus = 2;
                $scope.ShowContactInformation = false;
                $scope.ShowBusinessInformation = false;
                $scope.ShowFeasibilityInformation = false;
                var Id = $scope.GetUrlParameter("Id");
                var data = {
                    Id: parseInt(Id)
                }
                console.log(data);
                $scope.AjaxGet("/api/LeadApi/GetLead", data).then(
                    function (response) {
                        console.log(response);
                        $scope.Lead = response.data;
                    });
                getVendors();
            }
            getVendors = function () {
                $scope.AjaxGet("/api/VendorApi/GetVendors", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Vendors = response.data.Data;
                    });
            }
            $scope.ExpandCollapse = function (PortionName) {
                if (PortionName == "BI") {
                    $scope.ShowBusinessInformation = !$scope.ShowBusinessInformation;
                }
                if (PortionName == "CI") {
                    $scope.ShowContactInformation = !$scope.ShowContactInformation;
                }
                if ("FF") {
                    $scope.ShowFeasibilityInformation = !$scope.ShowFeasibilityInformation;
                }
            }
            $scope.AddRow = function () {
                var temp = {
                    VendorId: null,
                    LeadId: $scope.Lead.Id,
                    OTC: 0,
                    MRC: 0,
                    BandWidth: "",
                    Remarks:""
                }
                $scope.LeadFeasibility.push(temp);
                console.log($scope.LeadFeasibility);
            }
            $scope.RemoveRow = function (index) {
                console.log(index);
                $scope.LeadFeasibility.splice(index, 1);
            }
            $scope.AddFeasibility = function (LeadFeasibility) {
                console.log(LeadFeasibility);
                var temp = {
                    Feasibility: LeadFeasibility,
                    Status: $scope.FeasibilityStatus
                }
                console.log(temp);
                $scope.AjaxPost("/api/LeadApi/AddFeasibility", temp).then(
                    function (response) {
                        if (response.status == 200) {
                            //alert("Lead has been Updated Successfully!");
                            toaster.pop('success', "success", "Pmd Details Has Been Added Successfully!");
                            $timeout(function () { window.location.href = '/Lead/List'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Unable to update lead!");
                        }
                    });
            }
        }
    ]);