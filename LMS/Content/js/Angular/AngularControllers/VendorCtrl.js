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
                    toaster.pop('error', "error", "Name Is Required!");
                    return;
                }
                var promise = $http.post("/api/VendorApi/AddVendor", Vendor, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Vendor added successfully");
                            //window.open('/Vendor');
                            window.location = '/Vendor';
                            $timeout(function () { window.location.href = '/Vendor'; }, 2000);
                        }

                    });
            }

            $scope.EditInit = function () {

                $scope.Vendor = {}
                var Id = $scope.GetUrlParameter("Id");
                var data = {
                    Id: parseInt(Id)
                }
                console.log(data);
                console.log(data);
                $scope.AjaxGet("/api/VendorApi/GetVendorById", data).then(
                    function (response) {
                        console.log(response);
                        $scope.Vendor = response.data;
                    });

            }

            $scope.EditVendor = function (Vendor) {
                console.log(Vendor);
                if (Vendor.Name == null || Vendor.Name == "") {
                    toaster.pop('error', "error", "Name Is Required!");
                    return;
                }
                var promise = $http.post("/api/VendorApi/EditVendor", Vendor, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        if (response.status == 200) {
                            //alert("Company has been updated Successfully!");
                            toaster.pop('success', "success", "Vendor has been updated successfully");
                            $timeout(function () { window.location.href = '/Vendor'; }, 2000);
                        }

                    });
            }
        }
    ]);