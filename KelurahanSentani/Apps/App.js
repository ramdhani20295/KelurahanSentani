angular.module('app', ['ngRoute'])
.config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            templateUrl:"main.htm",
        })
        .when("/rw", {
            templateUrl: "rw.htm",
        })
        .when("/view", {
            templateUrl: "../Apps/Views/view.html",
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