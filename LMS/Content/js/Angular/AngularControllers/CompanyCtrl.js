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
                $scope.Company.IsBranch = false;
                $scope.Companies = [];
                $scope.MOCS = [{ Name: "Phone", Id: 0 }, { Name: "Email", Id: 1 }, { Name: "Fax", Id: 2 }, { Name: "Visit", Id: 3 }];
                getCompaniesDropdown(0);
                getParentCompanies();
                $scope.BillingInformationsList = [];
            }
            $scope.AddRow = function () {

            }
            getParentCompanies = function (Id) {
                $scope.AjaxGet("/api/CompanyApi/GetHeadParentCompaniesDropdown", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Companies = response.data.Data;
                    });
            }
            getCompaniesDropdown = function (Id) {
                $scope.AjaxGet("/api/CompanyApi/GetCompaniesDropdown", { LeadId: Id }).then(
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
            $scope.SetTagModal = function (Id) {
                $scope.Tag = {};
                $scope.Tag.CompanyId = Id;
                $scope.SetAssigningType('Lead');
            }
            $scope.TagUser = function (Tag) {
                console.log(Tag);
                $scope.AjaxPost("/api/CompanyApi/TagAgent", Tag).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "User Has Been Tagged!");
                            $timeout(function () { window.location.href = '/Company/'; }, 2000);
                        } else {
                            //alert("Could Not Assign User");
                            toaster.pop('error', "error", "Could Not Tag User");
                        }
                    });
            }
            $scope.SetAssigningType = function (Type) {
                $scope.AjaxGet("/api/UserApi/GetAgentsForAssignment", { Type: Type }).then(
                    function (response) {
                        console.log(response);
                        $scope.AssignmentAgents = response.data.Data;
                    });
            }

            $scope.EditInit = function () {
                $scope.Company = {};
                $scope.Companies = [];

                getCompaniesDropdown(0);
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
                if (Company.Latitude == null) {
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