app.controller('PaymentCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",

        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to PaymentCtrl");
           
            $scope.PostPayment = function (Payment) {
                $scope.AjaxPost("/api/PaymentApi/PostPayment", { Id: Payment.Id }).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Payment Has Been Posted Successfully");
                            $timeout(function () { window.location.href = '/Payment'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not Post Payment!");
                        }
                    });
            }
            $scope.initIndex = function () {
                $scope.Payments = [];
                $scope.AjaxGet("/api/PaymentApi/GetListData", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Payments = response.data.Data;
                    });
            }
            function GetCompanyAccount() {
                $scope.AjaxGet("/api/ChargeApi/GetCompaniesAccountDropdown", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Companies = response.data.Data;
                    });
            }
            $scope.OnCompanyChange = function (Company) {
                $scope.Payment.AccountId = Company.AccountId;
                $scope.Payment.CompanyId = Company.Id;
                console.log(Company);
            }
            $scope.AddInit = function () {
                $scope.Company = {};
                $scope.Payment = {};
                GetCompanyAccount();
            }
            $scope.EditInit = function () {
                $scope.Payment = {}
                GetCompanyAccount();
                var Id = $scope.GetUrlParameter("Id");
                $scope.AjaxGet("/api/PaymentApi/GetPayment", { Id : Id}).then(
                    function (response) {
                        console.log(response);
                        $scope.Payment = response.data.Payment;
                        $scope.Payment.CheckDate = new Date($scope.Payment.CheckDate);
                        console.log($scope.Payment);
                        console.log($scope.Companies);
                        var Company = {};
                        angular.forEach($scope.Companies, function (value, key) {
                            if (value.Id == $scope.Payment.CompanyId) {
                                Company = value;
                                return;
                            }
                        });
                        $scope.Company = Company;
                        console.log($scope.Company );
                    });
                
            }
            $scope.AddPayment = function (Payment) {
                console.log(Payment);
                Payment.CheckDate = $scope.GetDatePostFormat(Payment.CheckDate);
                if (Payment.CompanyId == null) {
                    toaster.pop('error', "error", "Please Select Company!");
                    return;
                }
                if (Payment.Amount == null || Payment.Amount == " " ) {
                    toaster.pop('error', "error", "please select amount!");
                    return;
                }
                if (Payment.PaymentType == null) {
                    toaster.pop('error', "error", "please select payment type!");
                    return;
                }
                if (Payment.PaymentType == 1)
                {
                    if (Payment.CheckNumber == null || Payment.CheckNumber==" ") {
                        toaster.pop('error', "error", "please select cheque number!");
                        return;
                    }
                    if (Payment.CheckDate == null || Payment.CheckDate == " ") {
                        toaster.pop('error', "error", "please select cheque Date!");
                        return;
                    }
                    if (Payment.BankName == null || Payment.BankName == " ") {
                        toaster.pop('error', "error", "please select Bank Name!");
                        return;
                    }
                    if (Payment.DepositedBank == null || Payment.DepositedBank == " ") {
                        toaster.pop('error', "error", "please select Deposited Bank Name!");
                        return;
                    }
                }
            
                $scope.AjaxPost("/api/PaymentApi/AddPayment", Payment).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Payment Has Been Added Successfully");
                            $timeout(function () { window.location.href = '/Payment'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not add Payment!");
                        }
                    });
            }

            $scope.EditPayment = function (Payment) {
                console.log(Payment);
                if (Payment.CompanyId == null) {
                    toaster.pop('error', "error", "Please Select Company!");
                    return;
                }
                if (Payment.Amount == null || Payment.Amount == " ") {
                    toaster.pop('error', "error", "please select amount!");
                    return;
                }
                if (Payment.PaymentType == null) {
                    toaster.pop('error', "error", "please select payment type!");
                    return;
                }
                if (Payment.PaymentType == 1) {
                    if (Payment.CheckNumber == null || Payment.CheckNumber == " ") {
                        toaster.pop('error', "error", "please select cheque number!");
                        return;
                    }
                    if (Payment.CheckDate == null || Payment.CheckDate == " ") {
                        toaster.pop('error', "error", "please select cheque Date!");
                        return;
                    }
                    if (Payment.BankName == null || Payment.BankName == " ") {
                        toaster.pop('error', "error", "please select Bank Name!");
                        return;
                    }
                    if (Payment.DepositedBank == null || Payment.DepositedBank == " ") {
                        toaster.pop('error', "error", "please select Deposited Bank Name!");
                        return;
                    }
                }
                
                $scope.AjaxPost("/api/PaymentApi/EditPayment", Payment).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Payment Has Been Updated Successfully");
                            $timeout(function () { window.location.href = '/Payment'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could not update Payment!");
                        }
                    });
            }
        }]);