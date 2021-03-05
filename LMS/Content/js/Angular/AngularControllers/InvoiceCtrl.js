app.controller('InvoiceCtrl',
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
            console.log("Connected to Invoice App");
            $scope.initIndex = function () {
                $scope.Invoices = [];
                $scope.AjaxGet("/api/InvoiceApi/GetListData", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Invoices = response.data.Data;
                    });
            }
            getCompaniesDropdown = function (Id) {
                $scope.AjaxGet("/api/CompanyAccountsApi/GetCompanyAccountsDropdown", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Companies = response.data.Data;
                    });

            }
          
            $scope.AddInit = function () {
                $scope.Invoice = {};
                $scope.Invoice.Date = new Date();
                $scope.Invoice.StartDate = new Date();
                $scope.Invoice.EndDate = new Date();
                $scope.Invoice.DueDate = new Date();
                $scope.Billing = null;
                $scope.Invoice.InvoiceTaxes = [];
                $scope.Invoice.TotalAmount = 0;
                $scope.InvoiceDetails = [];
                $scope.DeletedCharges = [];
                getCompaniesDropdown(0);
            }
            $scope.AddInvoiceDetailRow = function () {
                var row = {
                    Description: "",
                    Amount: 0,
                }
                $scope.InvoiceDetails.push(row);
            }
            $scope.SetChargeModal = function (type) {
                console.log(type);
                $scope.Charge = {};
                $scope.Charge.Type = type;
                $scope.AjaxGet("/api/ChargeApi/GetChargesByType", { Type: type }).then(
                    function (response) {
                        console.log(response);
                        $scope.Charges = response.data.Charges;
                    });
            }
            $scope.SetChargeData = function (ChargeData) {
                console.log(ChargeData);
                $scope.InvoiceCharge = ChargeData;
            }
            $scope.UpdateTotalAmount = function () {
          
                $scope.Invoice.TotalAmount = 0;
                angular.forEach($scope.InvoiceDetails, function (value, key) {
              
                    $scope.Invoice.TotalAmount = $scope.Invoice.TotalAmount + value.Amount;
                });
                var Tax = 0;
                console.log($scope.Invoice.InvoiceTaxes);
                angular.forEach($scope.Invoice.InvoiceTaxes, function (row, key) {
                  
                    var TaxAmount = (row.Value * $scope.Invoice.TotalAmount) / 100;
                    console.log(TaxAmount);
                    row.Amount = TaxAmount;
                    $scope.Invoice.TotalAmount = $scope.Invoice.TotalAmount + TaxAmount;
                });
            }
            $scope.AddCharge = function (SelectedCharge) {
                console.log(SelectedCharge);
                var IsPresent = false; //InvoiceTaxes
                if ($scope.Charge.Type =='Tax' ) {
                    angular.forEach($scope.Invoice.InvoiceTaxes, function (value, key) {
                        if (value.ChargeId == SelectedCharge.ChargeId || value.Name == SelectedCharge.Name ) {
                            IsPresent = true
                            return;
                        }
                    });
                }
                if (!IsPresent) {
                    var data = {
                        Name: SelectedCharge.Name,
                        ChargeId: SelectedCharge.ChargeId,
                        Type: SelectedCharge.Type,
                        Value: SelectedCharge.Value
                    }
                    if ($scope.Charge.Type == 'Tax' ) {
                        $scope.Invoice.InvoiceTaxes.push(data);
                    }
                    $scope.UpdateTotalAmount();
                    console.log($scope.Invoice);
                }
            }
            $scope.GetCompanyData = function (Company) {
                console.log(Company);
                $scope.Billing = null;
                $scope.Invoice.CompanyId = Company.Id;
                $scope.CompanyName = Company.Name;
                $scope.AjaxGet("/api/CompanyApi/GetAccountByCompanyId", { CompanyId: Company.Id }).then(
                    function (response) {
                        console.log(response);
                        $scope.Account = response.data.Account;
                        $scope.Invoice.AccountId = $scope.Account.AccountId;
                        $scope.Invoice.CompanyAddress = $scope.Account.CompanyAddress ;
                        $scope.Invoice.CompanyContact = $scope.Account.CompanyContact ;
                        $scope.Invoice.CompanyEmail = $scope.Account.CompanyEmail;
                        $scope.Invoice.NTN = $scope.Account.NTN;
                    });



                //new request for geeting info from companybillingdetails
                getCompanyBillingDetails(Company);

            }
            function getCompanyBillingDetails(Company) {
                $scope.AjaxGet("/api/BillingApi/GetBillingDetails", { AccountId: Company.AccountId }).then(
                    function (response) {
                        console.log(response);
                        $scope.BillingDetails = response.data.Billings;
                        
                    });
            }
           
            $scope.GetBillingDetailsData = function (billing) {
                console.log(billing);
                $scope.Invoice.AccountId = billing.AccountId;
                $scope.Invoice.CompanyAddress = billing.CompanyAddress;
                $scope.Invoice.CompanyContact = billing.POCContact;
                $scope.Invoice.CompanyEmail = billing.POCEmail;
                $scope.Invoice.AttendantName = billing.POCName;
                $scope.Invoice.NTN = billing.NTN;
                $scope.Invoice.PackageId = billing.PackageId;
            }
            $scope.RemoveRow = function (index) {
                $scope.InvoiceDetails.splice(index, 1);
            }
            $scope.RemoveTaxRow = function (index) {
                if ($scope.Invoice.InvoiceTaxes[index].Id != 0) {
                    $scope.DeletedCharges.push($scope.Invoice.InvoiceTaxes[index]);
                }
                $scope.Invoice.InvoiceTaxes.splice(index, 1);
            }
            $scope.AddInvoice = function (Invoice) {
                Invoice.Date = $scope.GetDatePostFormat(Invoice.Date);
                Invoice.StartDate = $scope.GetDatePostFormat(Invoice.StartDate);
                Invoice.DueDate = $scope.GetDatePostFormat(Invoice.DueDate);
                Invoice.EndDate = $scope.GetDatePostFormat(Invoice.EndDate);
                Invoice.InvoiceDetails = $scope.InvoiceDetails;
                console.log(Invoice);
                if (Invoice.CompanyId == null) {
                    toaster.pop('error', "error", "Please Select Company!");
                    return;
                }
                if (Invoice.AttendantName == null || Invoice.AttendantName == "") {
                    toaster.pop('error', "error", "Please Enter Attendent Name!");
                    return;
                }
                if (Invoice.LinkName == null || Invoice.LinkName == " ") {
                    toaster.pop('error', "error", "Please Enter Link Name!");
                    return;
                }
                if (Invoice.PackageId == null || Invoice.PackageId == " ") {
                    toaster.pop('error', "error", "Please Enter Package!");
                    return;
                }
                if (Invoice.MonthlyCharges == null || Invoice.MonthlyCharges == " ") {
                    toaster.pop('error', "error", "Please Enter Monthly Charges!");
                    return;
                }
                
                $scope.AjaxPost("/api/InvoiceApi/AddInvoice", Invoice).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Invoice Has Been Added Successfully");
                            $timeout(function () { window.location.href = '/Invoice'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not add Invoice!");
                        }
                    });
            }
            $scope.EditInit = function () {
                getCompaniesDropdown(0);
                $scope.DeletedCharges = [];
                var Id = $scope.GetUrlParameter("Id");
                $scope.AjaxGet("/api/InvoiceApi/GetInvoice", {Id:Id}).then(
                    function (response) {
                        console.log(response);
                        $scope.Invoice = response.data;
                        $scope.InvoiceDetails = response.data.InvoiceDetails;
                        $scope.Invoice.StartDate = new Date($scope.Invoice.StartDate);
                        $scope.Invoice.EndDate = new Date($scope.Invoice.EndDate);
                        $scope.Invoice.Date = new Date($scope.Invoice.Date);
                        $scope.Invoice.DueDate = new Date($scope.Invoice.DueDate);

                        var Company = {};
                        angular.forEach($scope.Companies, function (value, key) {
                            if (value.Id == $scope.Invoice.CompanyId) {
                                Company = value;
                                return;
                            }
                        });
                        $scope.Company = Company;
                        $scope.CompanyName = Company.Name;
                    });
            }
            $scope.EditInvoice = function (Invoice) {
                Invoice.DeletedCharges = $scope.DeletedCharges;
                Invoice.InvoiceDetails = $scope.InvoiceDetails;
                Invoice.Date = $scope.GetDatePostFormat(Invoice.Date);
                Invoice.StartDate = $scope.GetDatePostFormat(Invoice.StartDate);
                Invoice.DueDate = $scope.GetDatePostFormat(Invoice.DueDate);
                Invoice.EndDate = $scope.GetDatePostFormat(Invoice.EndDate);
                console.log(Invoice);
                if (Invoice.CompanyId == null) {
                    toaster.pop('error', "error", "Please Select Company!");
                    return;
                }
                if (Invoice.AttendantName == null || Invoice.AttendantName == "") {
                    toaster.pop('error', "error", "Please Enter Attendent Name!");
                    return;
                }
                if (Invoice.LinkName == null || Invoice.LinkName == "") {
                    toaster.pop('error', "error", "Please Enter Link Name!");
                    return;
                }
                if (Invoice.PackageId == null || Invoice.PackageId == "") {
                    toaster.pop('error', "error", "Please Enter Package!");
                    return;
                }
                if (Invoice.MonthlyCharges == null || Invoice.MonthlyCharges == "") {
                    toaster.pop('error', "error", "Please Enter Monthly Charges!");
                    return;
                }
                
                $scope.AjaxPost("/api/InvoiceApi/EditInvoice", Invoice).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Invoice Has Been Updated Successfully");
                            $timeout(function () { window.location.href = '/Invoice'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not Update Invoice!");
                        }
                    });
            }


            $scope.PostInvoice = function (Invoice) {
                $scope.AjaxPost("/api/InvoiceApi/PostInvoice", { Id: Invoice.Id }).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Invoice Has Been Posted Successfully");
                            $timeout(function () { window.location.href = '/Invoice'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not Post Invoice!");
                        }
                    });
            }
        }])