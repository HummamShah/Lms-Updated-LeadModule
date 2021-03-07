app.controller('Company2Ctrl',
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
                $scope.AjaxGet("/api/CompanyApi/GetParentListing", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Companies = response.data.Data;
                    });
            }
            $scope.InitChild = function () {
                $scope.CompanyId = $scope.GetUrlParameter("CompanyId");
                $scope.AjaxGet("/api/CompanyApi/GetBranchesListing", { CompanyId: $scope.CompanyId }).then(
                    function (response) {
                        console.log(response);
                        $scope.Companies = response.data.Data;
                    });
            }

            $scope.InitBilling = function () {
                $scope.AjaxGet("/api/CompanyApi/GetCompaniesBillingListing", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Companies = response.data.Data;
                    });
            }

            $scope.AddParentInit = function () {
                $scope.Company = {};
                $scope.Company.IsBranch = false;
                $scope.Companies = [];
                $scope.MOCS = [{ Name: "Phone", Id: 0 }, { Name: "Email", Id: 1 }, { Name: "Fax", Id: 2 }, { Name: "Visit", Id: 3 }];
            }
            $scope.AddChildInit = function () {
                $scope.Company = {};
                $scope.Company.IsBranch = true;
                $scope.Company.ParentCompanyId = $scope.GetUrlParameter("CompanyId");
                $scope.Companies = [];
                $scope.MOCS = [{ Name: "Phone", Id: 0 }, { Name: "Email", Id: 1 }, { Name: "Fax", Id: 2 }, { Name: "Visit", Id: 3 }];
            }
           


           
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
                            $timeout(function () { window.location.href = '/Company/Parent'; }, 2000);
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
               
                $scope.AjaxGet("/api/CompanyApi/GetCompany", data).then(
                    function (response) {
                        console.log(response);
                        $scope.Company = response.data;
                        $scope.GetDropdownForServies($scope.Company.CUDS);
                        $scope.ParentCompanyId = $scope.Company.ParentCompanyId;
                        console.log($scope.Company);
                    });


            }
            $scope.AddParentCompany = function (Company) {
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
                Company.IsBranch = false;
                Company.IsParent = true;
                console.log(Company);
       
                $scope.AjaxPost("/api/CompanyApi/AddCompany", Company).then(
                    function (response) {
                        if (response.status == 200) {
                            // alert("Lead has been Added Successfully!");
                            toaster.pop('success', "success", "Company Added Successfully");
                            $timeout(function () { window.location.href = '/Company/Parent'; }, 2000);
                        } else {
                            //alert("Could Not Add new Lead");
                            toaster.pop('error', "error", "Could not add company!");
                        }
                    });

            }


            $scope.AddChildCompany = function (Company) {
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
                Company.IsBranch = true;
                Company.IsParent = false;
                Company.ParentCompanyId = $scope.GetUrlParameter("CompanyId");
                console.log(Company);
                $scope.AjaxPost("/api/CompanyApi/AddCompany", Company).then(
                    function (response) {
                        if (response.status == 200) {
                            // alert("Lead has been Added Successfully!");
                            toaster.pop('success', "success", "Company Added Successfully");
                            var link = '/Company/Children?CompanyId=' + $scope.GetUrlParameter("CompanyId");
                            $timeout(function () { window.location.href = link; }, 2000);
                        } else {
                            //alert("Could Not Add new Lead");
                            toaster.pop('error', "error", "Could not add company!");
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
                $scope.AjaxPost("/api/CompanyApi/EditCompany", Company).then(
                    function (response) {
                        if (response.status == 200) {
                            // alert("Lead has been Added Successfully!");
                            toaster.pop('success', "success", "Company has been updated successfully");
                            $timeout(function () { window.location.href = '/Company'; }, 2000);
                        } else {
                            //alert("Could Not Add new Lead");
                            toaster.pop('error', "error", "Could not update company!");
                        }
                    });
            }

        }
    ]);