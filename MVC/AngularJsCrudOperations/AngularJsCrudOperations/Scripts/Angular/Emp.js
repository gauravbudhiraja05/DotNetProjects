// <reference path="Emp.js" />
var App = angular.module('MyApp', ['ngRoute']);//already disscused about ngRoute and angular.module
App.controller('EmpController', function ($scope, EmployeeService) {// controller with two parameters $scope and serive Which i have little bit discused about it in ever first angualrjs tutorial
    $scope.isFormValid = false;
    $scope.message = null;
    $scope.employee = null;
    EmployeeService.GetEmployeeList().then(function (d) { //Call the GetEmployeeList() function in service for getting all Employee Record
        alert(d.data);
        $scope.employee = d.data;
    },
        function () {
            alert('Failed');
        });

    // employee Objects
    $scope.Employee = {
        Id: '',
        Name: '',
        FatherName: '',
        EmailID: '',
        PhoneNo: '',
        Address: ''
    };
    //check form validation
    $scope.$watch('CreateForm.$valid', function (newValue) { // form1 is our form name
        $scope.isFormValid = newValue;
    });

    //Add Employee Record function
    $scope.EmployeeRecord = function (data) {
        $scope.message = '';
        $scope.Employee = data;
        if ($scope.isFormValid) {
            EmployeeService.AddEmployee($scope.Employee).then(function (d) { //Calling AddEmployee() from service
                alert(d);
                if (d == 'Success') {
                    //Clear Form
                    ClearForm();
                }
            });
        }
        else {
            $scope.message = 'Please fill the required fields';
        }
    };

    //Delete Employee Record function
    $scope.DelEmployee = function (emp) {
        if (emp.Id != null) {
            if (window.confirm('Are you sure you want to delete this Id = ' + emp.Id + '?'))//Popup window will open to confirm
            {
                EmployeeService.DelEmployeeData(emp.Id).then(function (emp) { //Calling DelEmployeeData() from service with parameter
                    window.location.href = '/Employee/Index';
                }, function () {
                    alert("Error in deleting record");
                });
            }
        }
        else {
            alert("this id is null");
        }
    };

    //Update Employee Data function
    $scope.UpdateEmployee = function () {
        var Employee = {};
        Employee["Id"] = $scope.EmployeeId;
        Employee["Name"] = $scope.EmployeeName;
        Employee["FatherName"] = $scope.EmployeeFatherName;
        Employee["EmailID"] = $scope.EmployeeEmailID;
        Employee["PhoneNo"] = $scope.EmployeePhoneNo;
        Employee["Address"] = $scope.EmployeeAddress;
        EmployeeService.UpdateEmployeeData(Employee);
    }

    //Redrect index form to edit form with parameter
    $scope.RedirectToEdit = function (emp) {
        window.location.href = '/Employee/Edit/' + emp.Id;
    };

    //Redirect to Detail view from index view with parameter
    $scope.RedirectToDetails = function (emp) {
        window.location.href = '/Employee/Detail/' + emp.Id;
    };

    //Calling GetEmployeeByID() from service. This function will fetch record from data base and then paste that record in edit form fields.
    EmployeeService.GetEmployeeByID().success(function (data) {
        $scope.employee = data;
        $scope.EmployeeId = data.Id;
        $scope.EmployeeName = data.Name;
        $scope.EmployeeFatherName = data.FatherName;
        $scope.EmployeeEmailID = data.EmailID;
        $scope.EmployeePhoneNo = data.PhoneNo;
        $scope.EmployeeAddress = data.Address;
    });

    //Clear Form Funciton
    function ClearForm() {
        $scope.Employee = {};
        $scope.CreateForm.$setPristine();
        $scope.UpdateEmp = {};
        $scope.EditForm.$setPristine();
    }
});

App.factory('EmployeeService', function ($http, $q, $window) {//I have disscussed little bit about service in 3rd tutorial of angularjs series. Declare with some dependencies.
    return {
        //Get all Employee List 
        GetEmployeeList: function () {
            return $http.get('/Employee/GetAllEmployee');
        },

        GetEmployeId: function () {//this function will get the last value of current page url.
            var urlPath = $window.location.href;
            var result = String(urlPath).split("/"); //will split the url by forword slash
            if (result != null && result.length > 0) {
                var id = result[result.length - 1]; //will get the last value from url.
                return id;
            }
        },

        GetEmployeeByID: function () { // this function will call the controller function GetEmployeeByID to get record by ID.
            var currentEmployeeID = this.GetEmployeId();//calling GetEmployeId to get the current page url's last value.
            if (currentEmployeeID != null) {
                return $http.get('/Employee/GetEmployeeById', { params: { id: currentEmployeeID } });
            }
        },

        //Add Employee Data. This function will call the Controller's Create action to add a new employee.
        AddEmployee: function (data) {
            var defer = $q.defer();
            $http({
                url: '/Employee/Create',
                method: "POST",
                data: data
            }).success(function (d) {
                //Callback after success
                defer.resolve(d);
            }).error(function (e) {
                //callback after failed
                alert("Error");
                window.location.href = '/Employee/Create';
                defer.reject(e);
            });
            return defer.promise;
        }, //End of Add Employee Data

        //Delete Employee Data. This function will call the controller's Delete action method to delete the employee record.
        DelEmployeeData: function (employeeid) {
            var defer = $q.defer(); // I have disscussed littel bit about $q and defer in 3rd tutorial of angualrjs. 
            $http({
                url: '/Employee/Delete/' + employeeid,
                method: 'POST'
            }).success(function (d) {
                alert("the person deleted successfully");
                defer.resolve(d);
            }).error(function (e) {
                alert("Error");
                defer.reject(e);
            });
            return defer.promise;

        },

        //Update Employee Data. This function will call the controller's Update action method to Update the employee record.
        UpdateEmployeeData: function (employee) {
            var defer = $q.defer();
            employee.Id = this.GetEmployeId();
            $http({
                url: '/Employee/UpdateEmployee',
                method: 'POST',
                data: employee
            }).success(function (d) {
                defer.resolve(d);
                window.location.href = '/Employee/Index';
            }).error(function (e) {
                alert("Error");
                defer.reject(e);
            });
            return defer.promise;
        },
    }
});