app.controller('CustomerCtrl', ['$scope', 'CrudService',
    function ($scope, CrudService) {
        // Base Url   
        var baseUrl = '/api/Customers/';
        $scope.btnText = "Save";
        $scope.customerID = 0;
        $scope.SaveUpdate = function () {
            var customer = {
                FirstName: $scope.firstName,
                LastName: $scope.lasttName,
                Email: $scope.email,
                Phone_Number: $scope.phoneNumber,
                Country_code: $scope.countrycode,
                Gender: $scope.gender,
                Balance: $scope.balance
            }
            if ($scope.btnText == "Save") {
                var apiRoute = baseUrl + 'SaveCustomer/';
                var saveCustomer = CrudService.post(apiRoute, customer);
                saveCustomer.then(function (response) {
                    if (response.data != "") {
                        alert("Data Save Successfully");
                        $scope.Clear();
                    } else {
                        alert("Some error");
                    }
                }, function (error) {
                    console.log("Error: " + error);
                });
            }
            else {
                var apiRoute = baseUrl + 'UpdateCustomers/';
                var UpdateCustomer = CrudService.put(apiRoute, customer);
                UpdateCustomer.then(function (response) {
                    if (response.data != "") {
                        alert("Data Update Successfully");
                        $scope.GetCustomers();
                        $scope.Clear();
                    } else {
                        alert("Some error");
                    }
                }, function (error) {
                    console.log("Error: " + error);
                });
            }
        }
        $scope.Clear = function () {
            $scope.customerID = 0;
            $scope.firstName = "";
            $scope.lasttName = "";
            $scope.email = "";
            $scope.phoneNumber = "",
            $scope.countrycode = "";
            $scope.gender = "";
            $scope.balance = "";
        }
        $scope.GetCustomers = function () {
            var apiRoute = baseUrl + 'GetCustomers/';
            var customer = CrudService.getAll(apiRoute);
            customer.then(function (response) {
                debugger
                $scope.customers = response.data;
            }, function (error) {
                console.log("Error: " + error);
            });
        }
        $scope.GetCustomers();

        $scope.GetCustomerByID = function (dataModel) {
            debugger
            var apiRoute = baseUrl + 'GetCustomersById';
            var customer = CrudService.getbyID(apiRoute, dataModel.CustomerID);
            customer.then(function (response) {
                $scope.customerID = response.data.CustomerID;
                $scope.firstName = response.data.FirstName;
                $scope.lasttName = response.data.LastName;
                $scope.email = response.data.Email;
                $scope.phoneNumber = response.data.Phone_Number,
                    $scope.countrycode = response.data.Country_code;
                $scope.gender = response.data.Gender;
                $scope.balance = response.data.Balance;
                $scope.btnText = "Update";
            }, function (error) {
                console.log("Error: " + error);
            });
        }
        $scope.DeleteCustomer = function (dataModel) {
            debugger
            var apiRoute = baseUrl + 'DeleteCustomersById/' + dataModel.CustomerID;
            var deleteCustomer = CrudService.delete(apiRoute);
            deleteCustomer.then(function (response) {
                if (response.data != "") {
                    alert("Data Delete Successfully");
                    $scope.GetCustomers();
                    $scope.Clear();
                } else {
                    alert("Some error");
                }
            }, function (error) {
                console.log("Error: " + error);
            });
        }
    }
]);
