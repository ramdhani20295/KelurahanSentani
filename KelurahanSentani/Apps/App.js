angular.module('app', ['app.service','app.controller','ngRoute'])
.config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            templateUrl:"main.htm",
        })
        .when("/pejabat", {
            templateUrl: "../Apps/Views/pejabat.html",
            controller: "PejabatController"
        })
        .when("/staff", {
            templateUrl: "../Apps/Views/staff.html",
        })
        .when("/rw", {
            templateUrl: "../Apps/Views/rw.html",
            controller:"StrukturKelurahanController"
        })
        .when("/rt", {
            templateUrl: "../Apps/Views/rt.html",
        })
        .when("/kependudukan", {
            templateUrl: "../Apps/Views/kependudukan.html",
        })
        .when("/pmasuk", {
            templateUrl: "../Apps/Views/pmasuk.html",
        })
        .when("/pkeluar", {
            templateUrl: "../Apps/Views/pkeluar.html",
        })
        .when("/kelahiran", {
            templateUrl: "../Apps/Views/kelahiran.html",
        })
        .when("/kematian", {
            templateUrl: "../Apps/Views/kematian.html",
        })
        .when("/kk", {
            templateUrl: "../Apps/Views/kk.html",
        })
    ;
})

.controller("RedController", function ($scope) {

    $scope.RW= [];

    $scope.Init=function()
    {
        var rw ={};
        rw.Nama="I";
        rw.RT=[];
        
        var rt1 = {};
        rt1.Nama="1";

        var rt2 = {};
        rt2.Nama="2";


        rw.RT.push(rt1);
        rw.RT.push(rt2);

        $scope.RW.push(rw);
        $scope.RW.push({ 'Nama': 'II' });

    }

    $scope.ShowName=function(model)
    {
        $scope.RW.push(model);
    }

   


})







    ;