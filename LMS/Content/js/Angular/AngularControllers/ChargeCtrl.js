app.controller('ChargeCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",

        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to ChargeCtrl");
            $scope.initIndex = function () {
                $scope.Charges = [];
                $scope.TypeOfCharge = $scope.GetUrlParameter("Type");
                var Type = 0;
                if ($scope.TypeOfCharge == 'Tax') {
                    Type = 0;
                }
                $scope.AjaxGet("/api/ChargeApi/GetListData", { Type: Type }).then(
                    function (response) {
                        console.log(response);
                        $scope.Charges = response.data.Data;
                    });
            } // send type from js 
            $scope.AddInit = function () {
                $scope.TypeOfCharge = $scope.GetUrlParameter("Type");
                $scope.Charge = {};
                $scope.File;
                
            }
            $scope.AddCharge = function (Charge) {
                if ($scope.TypeOfCharge == 'Tax') {
                    Charge.Type = 0;
                }
                if (Charge == null || Charge.Name == "") {
                    toaster.pop('error', "error", "Please Add Name Of the Charge");
                }
                console.log(Charge);
                $scope.AjaxPost("/api/ChargeApi/AddCharge", Charge).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Charge Has Been Added Successfully");
                            var link = '/Charge/Index?Type=' + $scope.TypeOfCharge;
                            $timeout(function () { window.location.href = link; }, 2000);
                        } else {
                            //alert("Could Not Add new Lead");
                            toaster.pop('error', "error", "Could not add Charge!");
                        }
                    });

            }
            $scope.EditInit = function () {
                $scope.TypeOfCharge = $scope.GetUrlParameter("Type");
                var Id = $scope.GetUrlParameter("Id");
                var data = {
                    Id: parseInt(Id)
                }
                console.log(data);
                $scope.Charge = {};
                $scope.AjaxGet("/api/ChargeApi/GetCharge", data).then(
                    function (response) {
                        console.log(response);
                        $scope.Charge = response.data.Charge;
                        
                    });
            }

            $scope.EditCharge = function (Charge) {
                if (Charge == null || Charge.Name == "") {
                    toaster.pop('error', "error", "Please Add Name Of the Charge");
                }
                console.log(Charge);
                $scope.AjaxPost("/api/ChargeApi/EditCharge", Charge).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Charge Has Been Updated Successfully");
                            var link = '/Charge/Index?Type=' + $scope.TypeOfCharge;
                            $timeout(function () { window.location.href = link; }, 2000);
                        } else {
                            //alert("Could Not Add new Lead");
                            toaster.pop('error', "error", "Could not Update Charge!");
                        }
                    });
            }
            //$scope.UploadFile = function () {
            //    //if ($('#myfile').val() == '') {
            //    //    alert('Please select file');
            //    //    return;
            //    //}
            //    console.log($('#myfile').val());
            //    var formData = new FormData();
            //    //var file = $('#myfile')[0];
            //    var file = angular.element('#myfile')[0];
            //    formData.append('file', file.files[0]);
            //    console.log(formData);
            //    var result = $scope.FileUpload(formData);
            //    console.log(result);
            //}

        }])