app.controller('BillingCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",
        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to Billing App");
            $scope.initIndex = function () {
                $scope.AjaxGet("/api/BillingApi/GetListData", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Companies = response.data.Data;
                    });
            }
            $scope.AddInit = function () {
                $scope.BillingInformationsList = [];
                $scope.Billing = {
                    Id: 0,
                    Address1: "",
                    Address2: "",
                    City: "",
                    Country:""
                };
                $scope.CompanyId = $scope.GetUrlParameter("Id");
                data = {
                    Id: $scope.CompanyId 
                }
                $scope.AjaxGet("/api/BillingApi/GetCompanyBillingInformation", data).then(
                    function (response) {
                        $scope.Billing = response.data.BillingInformation;
                        $scope.BillingInformationsList = response.data.BillingDetails;
                    });
            }
            $scope.AddBilling = function (Billing) {
                console.log(Billing); // billing onformation will not be added if there is no single billing details added
                console.log($scope.BillingInformationsList);
                angular.forEach($scope.BillingInformationsList, function (value, key) {
                    if (value.BillingName == null || value.BillingName == "") {
                        toaster.pop('error', "error", "Please Enter Billing Details Name!");
                        return;
                    }
                }); 
                if ($scope.BillingInformationsList.length == 0) {
                    toaster.pop('error', "error", "Please Enter Atleast One Billing Detail!");
                    return;
                }
                $scope.BillingInformationsList
                var BillingData = {
                    CompanyId: $scope.CompanyId,
                    BillingInformation: Billing,
                    BillingDetails: $scope.BillingInformationsList
                }
                console.log(BillingData);
                $scope.AjaxPost("/api/BillingApi/UpdateBillingInformation", { BillingData }).then(
                    function (response) {
                        if (response.status == 200) {
                            // alert("Lead has been Added Successfully!");
                            toaster.pop('success', "success", "Billing Information Has Been Updated Successfully");
                            $timeout(function () { window.location.href = '/Company/Billing'; }, 2000);
                        } else {
                            //alert("Could Not Add new Lead");
                            toaster.pop('error', "error", "Could not update Billing!");
                        }
                    });
            }
            $scope.AddRow = function () {
                var BillingDetailRow = {
                    Id: 0,
                    BillingInformationId:0,
                    SalesAgentId: 0,
                    IsPaperBillRequired: false,
                    BirthDate: new Date(),
                    InstallationDate: new Date(),
                    OTC: 0,
                    MRC: 0,
                    Medium:"",
                    Package: "",
                    SalesRepId:null
                }
                $scope.BillingInformationsList.push(BillingDetailRow);
            }
            $scope.RemoveRow = function (index) {
                //if ($scope.Lead.FeasibilityDetails[index].Id != 0) {
                //    $scope.DeletedRows.push($scope.Lead.FeasibilityDetails[index]);
                //}
                $scope.BillingInformationsList.splice(index, 1);
            }
        }])