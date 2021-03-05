app.controller('LeadCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",
        "moment",
        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster,moment) {
            console.log("Connected to Lead App");
            $scope.NextPage = function () {
                $scope.ListingOptions.CurrentPage = $scope.ListingOptions.CurrentPage + 1;
                var ListingOptions = {
                    CurrentPage: $scope.ListingOptions.CurrentPage,
                    PageSize: $scope.ListingOptions.PageSize
                }
                $scope.ResetList(ListingOptions);
            }
            $scope.PreviousPage = function () {
                $scope.ListingOptions.CurrentPage = $scope.ListingOptions.CurrentPage - 1;
                var ListingOptions = {
                    CurrentPage: $scope.ListingOptions.CurrentPage,
                    PageSize: $scope.ListingOptions.PageSize
                }
                $scope.ResetList(ListingOptions);
            }
            $scope.initIndex = function () {
                $scope.Assignment = {};
                $scope.AssignmentTypes = ["Lead", "PMD", "PreSale"];
                var ListingOptions = {
                    CurrentPage: 1,
                    PageSize: 20
                }
               
                $scope.ListingOptions.Url = '/api/LeadApi/GetListData';
                //$scope.InitListing();
                $scope.AjaxGet("/api/LeadApi/GetListData", $scope.ListingOptions).then(
                    function (response) {
                        console.log(response);
                        $scope.Leads = response.data.Data;
                        $scope.ListingOptions.TotalRecords = response.data.TotalRecords;
                    });


            }
            $scope.ResetList = function (data) {
                $scope.AjaxGet("/api/LeadApi/GetListData", data).then(
                    function (response) {
                        console.log(response);
                        $scope.Leads = response.data.Data;
                        $scope.ListingOptions.TotalRecords = response.data.TotalRecords;
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
                $scope.Assignment.Domain = Lead.Domain;

            }
            $scope.AddQuotation = function (Quot,Id) {
                var temp = {
                    LeadId : Id,
                    Quotation: Quot.Quotation,
                    MRC: Quot.QuotationMrc,
                    OTC: Quot.QuotationOtc,
                    QuotationStatus: Quot.QuotationStatus,
                    QuotationRemarks: Quot.QuotationRemarks
                }
                console.log(temp);
                $scope.AjaxPost("/api/LeadApi/AddQuotation", temp).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Quotation Updated!");
                            $timeout(function () { window.location.href = '/Lead/List'; }, 2000);
                        } else {
                            //alert("Could Not Assign User");
                            toaster.pop('error', "error", "Could not Assign Commercial");
                        }
                    });
                console.log(temp);
            }
            $scope.SetCommercialModal = function (Lead) {
                $scope.Commercial = {};
                $scope.Commercial.LeadId = Lead.Id;
                $scope.Commercial.QuotationStatus = 2;
            }
            $scope.SetAssigningType = function (Type) {
                $scope.AjaxGet("/api/UserApi/GetAgentsForAssignment", { Type: Type }).then(
                    function (response) {
                        console.log(response);
                        $scope.AssignmentAgents = response.data.Data;
                    });
            }
            $scope.AssignUser = function (Assignment,IsPMD) {
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
                            if (IsPMD) {
                                $timeout(function () { window.location.href = '/Lead/List'; }, 2000);
                            }
                            if (!IsPMD) {
                                $timeout(function () { window.location.href = '/Lead'; }, 2000);
                            }
                            
                        } else {
                            //alert("Could Not Assign User");
                            toaster.pop('error', "error", "Could not Assign User");
                        }
                    });
            }

            $scope.ChangeLeadStatus = function (Id) {
                var Request = {
                    Id: Id,
                    Status:3
                }
                $scope.AjaxPost("/api/LeadApi/ChangeLeadStatus", Request).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Lead Status has been changed to Closed");
                            $timeout(function () { window.location.href = '/Lead'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not Close Lead");
                        }
                    });
            }
        
            $scope.EditLeadStatus = function (IsApproved,Id )
            {
                var data = { LeadId: Id, IsApproved: IsApproved };
                console.log(data);
                
                $scope.AjaxPost("/api/LeadApi/EditLeadStatusRequest", data).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Lead Has Been Marked Completed");
                            $timeout(function () { window.location.href = '/Lead'; }, 2000);
                        } else {
                            
                            toaster.pop('error', "error", "Could Not Mark Completed Lead");
                        }
                    });
            }

            $scope.AddApproval = function (IsApproved, Id, AdminRemarks)
            {
                var temp = {
                    IsApproved :IsApproved,
                    LeadId: Id,
                    AdminRemarks: AdminRemarks
                }
                console.log(temp);
                
                $scope.AjaxPost("/api/LeadApi/AddApproval", temp).then(
                    function (response) {
                        if (response.status == 200) {
                            
                            toaster.pop('success', "success", "Lead Approval has been Updated Successfully");
                            $timeout(function () { window.location.href = '/Lead'; }, 2000);
                        } else {
                            
                            toaster.pop('error', "error", "Could not Approved Lead");
                        }
                    });

            }
            $scope.AddLead = function (Lead) {
                console.log(Lead);
                Lead.EstimatedClosingDate = $scope.GetDatePostFormat(Lead.EstimatedClosingDate);
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
                if (Lead.Domain == 1) {
                    if (Lead.ConnectivityDetails.CurrentlyUsedMedium == null || Lead.ConnectivityDetails.CurrentlyUsedMedium == "") {
                        toaster.pop('error', "error", "Please Enter Currently Used Medium");
                        return;
                    }
                    if (Lead.ConnectivityDetails.RequiredMedium == null || Lead.ConnectivityDetails.RequiredMedium == "") {
                        toaster.pop('error', "error", "Please Enter Required Medium");
                        return;
                    }
                    if (Lead.ConnectivityDetails.ConnectivityType == null || Lead.ConnectivityDetails.ConnectivityType == "") {
                        toaster.pop('error', "error", "Please Enter Connectivity Type");
                        return;
                    }
                    //if (Lead.ConnectivityDetails.Budget == null || Lead.ConnectivityDetails.Budget == "") {
                    //    toaster.pop('error', "error", "Please Enter Target Price");
                    //    return;
                    //}
                    //if (Lead.ConnectivityDetails.EstimatedClosingDate == null || Lead.EstimatedClosingDate == "") {
                    //    toaster.pop('error', "error", "Please Enter Estimated Closing Date");
                    //    return;
                    //}
                    if (Lead.ConnectivityDetails.Bandwidth == null || Lead.ConnectivityDetails.Bandwidth == "") {
                        toaster.pop('error', "error", "Please Enter Bandwidth");
                        return;
                    }
                }
                if (Lead.Domain == 0)
                {
                    if (Lead.SolutionDetails.SolutionType == null || Lead.SolutionDetails.SolutionType == "") {
                        toaster.pop('error', "error", "Please Select Solution Type");
                        return;
                    }
                    if (Lead.SolutionDetails.SolutionType == 0 || Lead.SolutionDetails.SolutionType == 3 || Lead.SolutionDetails.SolutionType == 4)
                    {

                        if (Lead.SolutionDetails.SolutionSubType == null || Lead.SolutionDetails.SolutionSubType == "") {
                            toaster.pop('error', "error", "Please Select Solution Sub Type");
                            return;
                        }
                    }

                    if (Lead.SolutionDetails.SolutionType == 1 || Lead.SolutionDetails.SolutionType == 2 || Lead.SolutionDetails.SolutionType == 3 || Lead.SolutionDetails.SolutionType == 5)
                    {
                        if (Lead.SolutionDetails.SolutionServiceProvider == null || Lead.SolutionDetails.SolutionServiceProvider == "") {
                        toaster.pop('error', "error", "Please Select Solution Service Provider");
                        return;
                        }
                    }

                    if (Lead.SolutionDetails.SolutionType == 0 || Lead.SolutionDetails.SolutionType == 1 || Lead.SolutionDetails.SolutionType == 2 || Lead.SolutionDetails.SolutionType == 5)
                    {
                        if (Lead.SolutionDetails.SolutionServiceProduct == null || Lead.SolutionDetails.SolutionServiceProduct == "") {
                            toaster.pop('error', "error", "Please Select Solution Service Product");
                            return;
                            }
                    }

                    if (Lead.SolutionDetails.SolutionType == 0 && (Lead.SolutionDetails.SolutionSubType != 3 && Lead.SolutionDetails.IsNew == false))
                    {

                        if (Lead.SolutionDetails.CurrentServiceInfo == null || Lead.SolutionDetails.CurrentServiceInfo == "") {
                            toaster.pop('error', "error", "Please Select Existing SKU / Serial No");
                            return;
                            }
                    }

                    if (Lead.SolutionDetails.SolutionType == 0 && (Lead.SolutionDetails.SolutionSubType == 0 || Lead.SolutionDetails.SolutionSubType == 2))
                    {
                        if (Lead.SolutionDetails.Duration == null || Lead.SolutionDetails.Duration == "") {
                            toaster.pop('error', "error", "Please Select Duration");
                            return;
                            }
                    }

                    if ((Lead.SolutionDetails.SolutionType == 0 && Lead.SolutionDetails.SolutionSubType == 0) || Lead.SolutionDetails.SolutionType == 1 || Lead.SolutionDetails.SolutionType == 2 || Lead.SolutionDetails.SolutionType == 4 || Lead.SolutionDetails.SolutionType == 5) {
                        if (Lead.SolutionDetails.Quantity == null || Lead.SolutionDetails.Quantity == "") {
                            toaster.pop('error', "error", "Please Select Quantity/No of Users");
                            return;
                            }
                    }

                    if (Lead.SolutionDetails.SolutionType == 5 && Lead.SolutionDetails.SolutionServiceProduct == 0) {
                        if (Lead.SolutionDetails.OtherMeasurements == null || Lead.SolutionDetails.OtherMeasurements == "") {
                            toaster.pop('error', "error", "Please Select No of Co-Lines");
                            return;
                        }
                    }
                }
                
                //AddLead now AddnewLead changes need to verify
                $scope.AjaxPost("/api/LeadApi/AddLeadNew", Lead).then(
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
            getCompaniesDropdown = function (Id) {
                $scope.AjaxGet("/api/CompanyApi/GetCompaniesDropdown", { LeadId: Id }).then(
                    function (response) {
                        console.log(response);
                        $scope.Companies = response.data.Data;
                    });

            }
            $scope.AddInit = function () {
                $scope.Company = {};
                $scope.Companies = [];
                $scope.Lead = {};
                $scope.Lead.SolutionDetails = {};
                $scope.Lead.SolutionDetails.IsNew = false;
                $scope.ShowContactInformation = false;
                $scope.ShowBusinessInformation = false;
                //$scope.Lead.Domain = 0;
                $scope.MOCS = [{ Name: "Phone", Id: 0 }, { Name: "Email", Id: 1 }, { Name: "Fax", Id: 2 }, { Name: "Visit", Id: 3 }];
                getCompaniesDropdown(0);
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
                if (Company.Latitude == null ) {
                    toaster.pop('error', "error", "Latitude Is Required!");
                    return;
                }
                if (Company.Longitude == null) {
                    toaster.pop('error', "error", "Longitude Is Required!");
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
                        getCompaniesDropdown(0);
                    }
                });

            }
            $scope.GetCompanyData = function (Company) {
                console.log(Company.Id);
                $scope.Lead.CompanyId = Company.Id;
                var data = {
                    Id: Company.Id
                }
                var promise = $http.get("/api/CompanyApi/GetCompany", { params: data }, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        $scope.Lead.CompanyDetails= response.data;
                        $scope.Lead.CompanyId = Company.Id;
                        $scope.GetDropdownForServies($scope.Lead.CUDS);

                    });
            }


            $scope.Editinit = function () {
                $scope.Company = {};
                $scope.Companies = [];
                $scope.Lead = {};
                $scope.ShowContactInformation = false;
                $scope.ShowBusinessInformation = false;
                $scope.ShowFeasibilityInformation = false;
                // $scope.Lead.Domain = 0;
                var Id = $scope.GetUrlParameter("Id");
                getCompaniesDropdown(Id);
                var data = {
                    Id: parseInt(Id)
                }
                console.log(data);
                //GetLead Before testing new request
                $scope.AjaxGet("/api/LeadApi/GetNewLead", data).then(
                    function (response) {
                        console.log(response);
                        $scope.Lead = response.data;
                        $scope.Lead.ConnectivityDetails.EstimatedClosingDate = new Date($scope.Lead.ConnectivityDetails.EstimatedClosingDate)
                        console.log($scope.Lead.ConnectivityDetails.EstimatedClosingDate);
                        var Company = {};
                        angular.forEach($scope.Companies, function (value, key) {
                            if (value.Id == $scope.Lead.CompanyId) {
                                Company = value;
                                return;
                            }
                        }); 
                        $scope.CompanyId = Company;
                        $scope.GetDropdownForServies($scope.Lead.CUDS);
                    });

                $scope.MOCS = [{ Name: "Phone", Id: 0 }, { Name: "Email", Id: 1 }, { Name: "Fax", Id: 2 }, { Name: "Visit", Id: 3 }];
                
            }
            $scope.EditLead = function (Lead) {
                console.log(Lead);
                if (Lead.CompanyId == null) {
                    toaster.pop('error', "error", "Please Select Company!");
                    return;
                }
                if (Lead.Domain == null) {
                    toaster.pop('error', "error", "Please Select Domain!!");
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
                if (Lead.Domain == 1) {
                    if (Lead.ConnectivityDetails.CurrentlyUsedMedium == null || Lead.ConnectivityDetails.CurrentlyUsedMedium == "") {
                        toaster.pop('error', "error", "Please Enter Currently Used Medium");
                        return;
                    }
                    if (Lead.ConnectivityDetails.RequiredMedium == null || Lead.ConnectivityDetails.RequiredMedium == "") {
                        toaster.pop('error', "error", "Please Enter Required Medium");
                        return;
                    }
                    if (Lead.ConnectivityDetails.ConnectivityType == null || Lead.ConnectivityDetails.ConnectivityType == "") {
                        toaster.pop('error', "error", "Please Enter Connectivity Type!");
                        return;
                    }
                    //if (Lead.ConnectivityDetails.Budget == null || Lead.ConnectivityDetails.Budget == "") {
                    //    toaster.pop('error', "error", "Please Enter Target Price");
                    //    return;
                    //}
                    //if (Lead.ConnectivityDetails.EstimatedClosingDate == null || Lead.EstimatedClosingDate == "") {
                    //    toaster.pop('error', "error", "Please Enter Estimated Closing Date");
                    //    return;
                    //}
                    if (Lead.ConnectivityDetails.Bandwidth == null || Lead.ConnectivityDetails.Bandwidth == "") {
                        toaster.pop('error', "error", "Please Enter Bandwidth");
                        return;
                    }
                }
                if (Lead.Domain == 0) {
                    if (Lead.SolutionDetails.SolutionType == null || Lead.SolutionDetails.SolutionType == "") {
                        toaster.pop('error', "error", "Please Select Solution Type");
                        return;
                    }
                    if (Lead.SolutionDetails.SolutionType == 0 || Lead.SolutionDetails.SolutionType == 3 || Lead.SolutionDetails.SolutionType == 4) {

                        if (Lead.SolutionDetails.SolutionSubType == null || Lead.SolutionDetails.SolutionSubType == "") {
                            toaster.pop('error', "error", "Please Select Solution Sub Type");
                            return;
                        }
                    }

                    if (Lead.SolutionDetails.SolutionType == 1 || Lead.SolutionDetails.SolutionType == 2 || Lead.SolutionDetails.SolutionType == 3 || Lead.SolutionDetails.SolutionType == 5) {
                        if (Lead.SolutionDetails.SolutionServiceProvider == null || Lead.SolutionDetails.SolutionServiceProvider == "") {
                            toaster.pop('error', "error", "Please Select Solution Service Provider");
                            return;
                        }
                    }

                    if (Lead.SolutionDetails.SolutionType == 0 || Lead.SolutionDetails.SolutionType == 1 || Lead.SolutionDetails.SolutionType == 2 || Lead.SolutionDetails.SolutionType == 5) {
                        if (Lead.SolutionDetails.SolutionServiceProduct == null || Lead.SolutionDetails.SolutionServiceProduct == "") {
                            toaster.pop('error', "error", "Please Select Solution Service Product");
                            return;
                        }
                    }

                    if (Lead.SolutionDetails.SolutionType == 0 && (Lead.SolutionDetails.SolutionSubType != 3 && Lead.SolutionDetails.IsNew == false)) {

                        if (Lead.SolutionDetails.CurrentServiceInfo == null || Lead.SolutionDetails.CurrentServiceInfo == "") {
                            toaster.pop('error', "error", "Please Select Existing SKU / Serial No");
                            return;
                        }
                    }

                    if (Lead.SolutionDetails.SolutionType == 0 && (Lead.SolutionDetails.SolutionSubType == 0 || Lead.SolutionDetails.SolutionSubType == 2)) {
                        if (Lead.SolutionDetails.Duration == null || Lead.SolutionDetails.Duration == "") {
                            toaster.pop('error', "error", "Please Select Duration");
                            return;
                        }
                    }

                    if ((Lead.SolutionDetails.SolutionType == 0 && Lead.SolutionDetails.SolutionSubType == 0) || Lead.SolutionDetails.SolutionType == 1 || Lead.SolutionDetails.SolutionType == 2 || Lead.SolutionDetails.SolutionType == 4 || Lead.SolutionDetails.SolutionType == 5) {
                        if (Lead.SolutionDetails.Quantity == null || Lead.SolutionDetails.Quantity == "") {
                            toaster.pop('error', "error", "Please Select Quantity/No of Users");
                            return;
                        }
                    }

                    if (Lead.SolutionDetails.SolutionType == 5 && Lead.SolutionDetails.SolutionServiceProduct == 0) {
                        if (Lead.SolutionDetails.OtherMeasurements == null || Lead.SolutionDetails.OtherMeasurements == "") {
                            toaster.pop('error', "error", "Please Select No of Co-Lines");
                            return;
                        }
                    }
                }
                
                //$scope.AjaxPost("/api/LeadApi/EditLead", Lead).then(
                $scope.AjaxPost("/api/LeadApi/EditNewLead", Lead).then( //New Request
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
            $scope.FeasibiltyAddInit = function () {
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
                        $scope.Lead.EstimatedClosingDate = new Date($scope.Lead.EstimatedClosingDate);
                    });
                getVendors();
            }
            $scope.FeasibiltyEditInit = function () {
                $scope.Lead = {};
                $scope.LeadFeasibility = [];
                $scope.Vendors = [];
                $scope.DeletedRows = [];
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
                        $scope.Lead.EstimatedClosingDate = new Date($scope.Lead.EstimatedClosingDate);
                      //$scope.Lead.PmdStatus 
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
                    Id:0,
                    VendorId: null,
                    LeadId: $scope.Lead.Id,
                    OTC: 0,
                    MRC: 0,
                    BandWidth: "",
                    Remarks: "",
                    ConnectivityType:0,
                }
                $scope.LeadFeasibility.push(temp);
                console.log($scope.LeadFeasibility);
            }
            $scope.AddRowInEditForm = function () {
                var temp = {
                    Id:0,
                    VendorId: null,
                    LeadId: $scope.Lead.Id,
                    OTC: 0,
                    MRC: 0,
                    BandWidth: "",
                    Remarks: "",
                    ConnectivityType:0,
                }
                $scope.Lead.FeasibilityDetails.push(temp);
                console.log($scope.Lead.FeasibilityDetails);
            }
            $scope.RemoveRow = function (index) {
                console.log(index);
                $scope.LeadFeasibility.splice(index, 1);
            }
            $scope.RemoveEditRow = function (index) {
                console.log(index);
                if ($scope.Lead.FeasibilityDetails[index].Id != 0) {
                    $scope.DeletedRows.push($scope.Lead.FeasibilityDetails[index]);
                }
                $scope.Lead.FeasibilityDetails.splice(index, 1);
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
            $scope.EditFeasibility = function (LeadFeasibility, FeasibilityStatus) {
                console.log(LeadFeasibility);
                console.log($scope.DeletedRows);
                console.log(FeasibilityStatus);
                var temp = {
                    Feasibility: LeadFeasibility,
                    Status: FeasibilityStatus,
                    DeletedRows: $scope.DeletedRows
                }
                console.log(temp);
                $scope.AjaxPost("/api/LeadApi/EditFeasibility", temp).then(
                    function (response) {
                        if (response.status == 200) {
                            //alert("Lead has been Updated Successfully!");
                            toaster.pop('success', "success", "Pmd Details Has Been Updated Successfully!");
                            $timeout(function () { window.location.href = '/Lead/List'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Unable to update Feasibility!");
                        }
                    });
            }
            $scope.SetAssignmentModal = function (Lead) {
                $scope.Assignment.Id = Lead.Id;
                $scope.Assignment.Header = "Assign A User";
                $scope.Assignment.Type = "PreSale";
                $scope.SetAssigningType($scope.Assignment.Type);
            }
            $scope.ChangeFeasibilityStatus = function (status, id) {
                var request = {
                    Id: id,
                    Status: status
                }
                $scope.AjaxPost("/api/LeadApi/ChangeFeasibilityStatus", request).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Feasibility Status Has Been Updated Successfully!");
                            $timeout(function () { window.location.reload(); }, 2000);
                        } else {
                            toaster.pop('error', "error", "Unable To Update Feasibility Status!");
                        }
                    });
            }
        }
        
    ]);