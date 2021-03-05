
app.controller('PurchaseOrderCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",
        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to PurchaseORder App");
            $scope.initIndex = function () {
                $scope.AjaxGet("/api/PurchaseOrderApi/GetListData", null).then(
                    function (response) {
                        console.log(response);
                        $scope.PurchaseOrders = response.data.Data;
                    });
            }
           
            getCompaniesDropdown = function (Id) {
                $scope.AjaxGet("/api/CompanyApi/GetParentCompaniesDropdown", { LeadId: Id }).then(
                    function (response) {
                        console.log(response);
                        $scope.Companies = response.data.Data;
                    });

            }
            $scope.GetLeadData = function (LeadId) {
                console.log(LeadId);
                var IsLeadValidated = false;
                $scope.AjaxGet("/api/LeadApi/GetLeadById", { Id: LeadId }).then(
                    function (response) {
                        console.log(response);
                        if (!response.data.IsLeadPresent) {
                            toaster.pop('error', "error", "Please Enter Valid Lead Id!");
                            IsLeadValidated = false;
                        } else {
                            $scope.PurchaseOrder.CompanyId = response.data.CompanyId;
                            IsLeadValidated = true;
                        }
                       
                    });
                return IsLeadValidated;
            }
           
            $scope.AddInit = function () {
                $scope.PurchaseOrder = {};
                $scope.IsReferal = false;
                getCompaniesDropdown(0);
            }
            //$scope.SetReferal = function (IsReferal) {
            //    $scope.IsReferal = !IsReferal;
            //}
            $scope.EditInit = function () {
                $scope.PurchaseOrders = {};
                $scope.PresalesApproval = 4;
                $scope.IsSuggestionGiven = false;
                $scope.IsCoreHasFilledInput = false;
                $scope.IsReferal = false;
                $scope.IsCustomerVerificationFilled = false;
                getCompaniesDropdown(0);
                var Id = $scope.GetUrlParameter("Id");
                $scope.AjaxGet("/api/PurchaseOrderApi/GetPurchaseOrder", { Id: Id }).then(
                    function (response) {
                        if (response.status == 200) {
                            $scope.PurchaseOrder = response.data;
                            $scope.PresalesApproval = $scope.PurchaseOrder.PresalesApproval;
                            if ($scope.PurchaseOrder.SDSuggestedVendorId1 != null) {
                                $scope.IsSuggestionGiven = true;
                            }
                            if (($scope.PurchaseOrder.CoreIp != null || $scope.PurchaseOrder.CoreIp == "") && ($scope.PurchaseOrder.CoreVlanId_ != null || $scope.PurchaseOrder.CoreVlanId_ == "")) {
                                $scope.IsCoreHasFilledInput = true;
                            }
                           
                            if ($scope.PurchaseOrder.LeadId != 0 || $scope.PurchaseOrder.LeadId != null) {
                                $scope.IsReferal = true;
                            }
                            if ($scope.PurchaseOrder.SDClientVerification != null) {
                                $scope.IsCustomerVerificationFilled = true;
                            }
                        } else {
                            toaster.pop('error', "error", "Could not get PurchaseOrder!");
                        }
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
            $scope.ValidatePurchaseOrder = function (PurchaseOrder) {
                if ($scope.IsReferal && (PurchaseOrder.LeadId == 0 || PurchaseOrder.LeadId == null)) {
                    toaster.pop('error', "error", "Plese Enter Lead Id!");
                    return;
                }
                if (!$scope.IsReferal && (PurchaseOrder.CompanyId == null || PurchaseOrder.CompanyId == 0)) {
                    toaster.pop('error', "error", "Please Company!");
                    return;
                }
                if (($scope.IsReferal && (PurchaseOrder.LeadId != null || PurchaseOrder.LeadId != 0))) {
                    $scope.AjaxGet("/api/LeadApi/GetLeadById", { Id: PurchaseOrder.LeadId }).then(
                        function (response) {
                            console.log(response);
                            if (!response.data.IsLeadPresent) {
                                toaster.pop('error', "error", "Please Enter Valid Lead Id!");
                                return;
                            } else {
                                $scope.PurchaseOrder.CompanyId = response.data.CompanyId;
                                $scope.AddPurchaseOrder($scope.PurchaseOrder);
                            }

                        });
                }
                if (!$scope.IsReferal && (PurchaseOrder.CompanyId != null || PurchaseOrder.CompanyId != 0)) {
                    $scope.PurchaseOrder.LeadId = null;
                    $scope.AddPurchaseOrder($scope.PurchaseOrder);
                }
               
                console.log(PurchaseOrder);
               
            }
            $scope.AddPurchaseOrder = function (PurchaseOrder) {
                $scope.AjaxPost("/api/PurchaseOrderApi/AddPurchaseOrder", PurchaseOrder).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "PurchaseOrder Has Been Added Successfully");
                            $timeout(function () { window.location.href = '/PurchaseOrder'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not add PurchaseOrder!");
                        }
                    });
            }
            $scope.ValidatePurchaseOrderEdit = function (PurchaseOrder) {
                if ($scope.IsReferal && (PurchaseOrder.LeadId == 0 || PurchaseOrder.LeadId == null)) {
                    toaster.pop('error', "error", "Plese Enter Lead Id!");
                    return;
                }
                if (!$scope.IsReferal && (PurchaseOrder.CompanyId == null || PurchaseOrder.CompanyId == 0)) {
                    toaster.pop('error', "error", "Please Company!");
                    return;
                }
                if (($scope.IsReferal && (PurchaseOrder.LeadId != null || PurchaseOrder.LeadId != 0))) {
                    $scope.AjaxGet("/api/LeadApi/GetLeadById", { Id: PurchaseOrder.LeadId }).then(
                        function (response) {
                            console.log(response);
                            if (!response.data.IsLeadPresent) {
                                toaster.pop('error', "error", "Please Enter Valid Lead Id!");
                                return;
                            } else {
                                $scope.PurchaseOrder.CompanyId = response.data.CompanyId;
                                $scope.EditPurchaseOrder($scope.PurchaseOrder);
                            }

                        });
                }
                if (!$scope.IsReferal && (PurchaseOrder.CompanyId != null || PurchaseOrder.CompanyId != 0)) {
                    $scope.PurchaseOrder.LeadId = null;
                    $scope.EditPurchaseOrder($scope.PurchaseOrder);
                }

                console.log(PurchaseOrder);
               
            }
            $scope.EditPurchaseOrder = function (PurchaseOrder) {
                $scope.AjaxPost("/api/PurchaseOrderApi/EditPurchaseOrder", PurchaseOrder).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "PurchaseOrder Has Been Updated Successfully");
                            $timeout(function () { window.location.href = '/PurchaseOrder'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not update PurchaseOrder!");
                        }
                    });
            }
            $scope.SetAssigningDataToModal = function (PurchaseOrder) {
                GetDepartments();
                $scope.Assignment = { };
                $scope.Assignment.Id = PurchaseOrder.Id;
            }
            function GetDepartments() {
                $scope.AjaxGet("/api/DepartmentApi/GetDepartmentsDropdownForPO", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Departments = response.data.Data;
                    });
            }
            $scope.AssignDepartment = function (Assignment) {
                console.log(Assignment);
                $scope.AjaxPost("/api/PurchaseOrderApi/AssignDepartment", Assignment).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "PurchaseOrder Has Been Assigned Successfully");
                            $timeout(function () { window.location.href = '/PurchaseOrder'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not Assign PurchaseOrder!");
                        }
                    });
            }
            $scope.ChangeApprovalStatus = function (Id,ApprovalStatus) {
                var Reuest = {
                    Id: Id,
                    ApprovalId: ApprovalStatus
                }
                console.log(Reuest);
                $scope.AjaxPost("/api/PurchaseOrderApi/ApprovePurchaseOrder", Reuest).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "PurchaseOrder Has Been Approved Successfully");
                            $timeout(function () { window.location.href = '/PurchaseOrder'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not Approve PurchaseOrder!");
                        }
                    });
            }
            $scope.AddApproval = function (PO) {
                var Reuest = {
                    Id: PO.Id,
                    ApprovalId: PO.PresalesApproval
                }
                console.log(Reuest);
                $scope.AjaxPost("/api/PurchaseOrderApi/ApprovePurchaseOrder", Reuest).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Approval Status  Has Been Marked Successfully");
                            $timeout(function () { window.location.href = '/PurchaseOrder'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Update Status!");
                        }
                    });
            }
            $scope.AddRecommendation = function (PO) {
                var Reuest = {
                    Id: PO.Id,
                    SDSuggestedVendorId1: PO.SDSuggestedVendorId1,
                    SDSuggestedVendorId2: PO.SDSuggestedVendorId2,
                    SDSuggestedVendorId3: PO.SDSuggestedVendorId3
                }
                console.log(Reuest);
           
                $scope.AjaxPost("/api/PurchaseOrderApi/AddVendorSuggestionPurchaseOrder", Reuest).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Suggestion Has Been Added Successfully");
                            $timeout(function () { window.location.href = '/PurchaseOrder'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not Update PurchaseOrder!");
                        }
                    });
            }
            $scope.AddCoreInput = function (PO) {
                var Reuest = {
                    Id: PO.Id,
                    CoreIp: PO.CoreIp,
                    CoreVlanId_: PO.CoreVlanId_,
                 
                }
                console.log(Reuest);
       
                $scope.AjaxPost("/api/PurchaseOrderApi/AddCoreIpPurchaseOrder", Reuest).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Suggestion Has Been Added Successfully");
                            $timeout(function () { window.location.href = '/PurchaseOrder'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not Update PurchaseOrder!");
                        }
                    });
            }
            $scope.AddClientVerification = function (PO) {
                var Reuest = {
                    Id: PO.Id,
                    SDClientVerification: true
                }
                console.log(Reuest);
 
                $scope.AjaxPost("/api/PurchaseOrderApi/AddClientVerification", Reuest).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Client Approval Has Been Added Successfully");
                            $timeout(function () { window.location.href = '/PurchaseOrder'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Add Client Approval!");
                        }
                    });
            }
            $scope.UnassignPurchaseOrder = function (PO) {
                $scope.AjaxPost("/api/PurchaseOrderApi/UnAssignPurchaseOrder", { Id: PO.Id }).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Your response is inserted Successfully");
                            $timeout(function () { window.location.href = '/PurchaseOrder'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not insert your response to PurchaseOrder!");
                        }
                    });
            }

            $scope.MarkPurchaseOrderComplete = function (PO) {
                var Reuest = {
                    Id: PO.Id,
                    Status : 1 //1=Complete,0=inProgress
                }
                console.log(Reuest);
                $scope.AjaxPost("/api/PurchaseOrderApi/ChangePurchaseOrderStatus", Reuest).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Status Changed Successfully");
                            $timeout(function () { window.location.href = '/PurchaseOrder'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Change Status!");
                        }
                    });
            }
            $scope.EnableBilling = function (PO) {
                var Request = {
                    Id: PO.Id,
                    BillingStatus: 1 //1=Complete,0=inProgress
                }
                console.log(Request);
                $scope.AjaxPost("/api/PurchaseOrderApi/ChangePurchaseOrderBillingStatus", Request).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Status Changed Successfully");
                            $timeout(function () { window.location.href = '/PurchaseOrder'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Change Status!");
                        }
                    });
            }
           
        }])